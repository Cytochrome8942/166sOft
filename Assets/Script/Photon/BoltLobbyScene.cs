using UnityEngine;
using UnityEngine.UI;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using UdpKit.Platform.Photon;
using UdpKit;
using System;

public class BoltLobbyScene : GlobalEventListener
{
    public Text statusText;
    public GameObject roomNodePrefeb;
    public GameObject roomNodeList;
    public void RefreshStatusText()
    {
        statusText.text = "안녕하세요 " + PlayFabClient.instance.displayName + "님";
    }
    public override void BoltStartDone()
    {
        GetComponent<PlayFabSignInUI>().playFabUI.SetActive(false);
    }
    public override void SessionListUpdated(Map<System.Guid, UdpSession> sessionList)
    {
        foreach (var session in sessionList)
        {
            var tempSession = session.Value;
            if (tempSession.Source == UdpSessionSource.Photon)
            {
                var photonSession = tempSession as PhotonSession;
                var newNode = Instantiate(roomNodePrefeb, roomNodeList.transform);
                newNode.GetComponent<BoltRoomNodeUI>().SetRoomNodeData(photonSession, this);
            }
        }
    }
    public void StartServerBtnClicked(){
        BoltCustomServer.StartServer();
    }
    public void NodeBtnClicked(string hostName, string password){
        var roomAccessToken = new BoltTokens.RoomAccessToken();
        roomAccessToken.userName = PlayFabClient.instance.userID;
        roomAccessToken.password = password;
        BoltMatchmaking.JoinSession("test", roomAccessToken);
    }
}
