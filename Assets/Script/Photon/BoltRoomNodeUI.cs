using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using UnityEngine.UI;
using UdpKit.Platform.Photon;
using UdpKit;

public class BoltRoomNodeUI : GlobalEventListener
{
    private BoltLobbyScene _netManager;
    public PhotonSession _data;
    public Text roomNumText, roomNameText, roomPlayersText, roomHideText;
    public void SetRoomNodeData(PhotonSession data, BoltLobbyScene netManager)
    {
        _netManager = netManager;
        _data = data;
        roomNameText.text = data.HostName;
        roomPlayersText.text = data.ConnectionsCurrent + " / " + data.ConnectionsMax;
        roomHideText.text = data.IsVisible ? "X" : "O";
    }
    public override void SessionListUpdated(Map<System.Guid, UdpSession> sessionList)
    {
        Destroy(gameObject);
    }
    public void btnClicked(){
        _netManager.NodeBtnClicked(_data.HostName, "");
    }
}
