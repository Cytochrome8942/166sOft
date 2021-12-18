using UnityEngine;
using UnityEngine.UI;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using UdpKit.Platform.Photon;
using UdpKit;
using System.Collections.Generic;

public class BoltLobbyCallbacks : GlobalEventListener
{
    public GameObject playerNodePrefab;
    public BoltEntity entity;
    public GameObject blue, red;
    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        entity = BoltNetwork.Instantiate(playerNodePrefab);
        entity.TakeControl();
    }
    public override void OnEvent(LoadSceneRequest evnt)
    {
        /*
        BoltTokens.StartToken token = new BoltTokens.StartToken();

        var blueTeam = blue.GetComponentsInChildren<BoltEntity>();
        var redTeam = red.GetComponentsInChildren<BoltEntity>();
        
        token.name = new List<string>();
        token.isBlueTeam = new List<bool>();

        Debug.Log(blueTeam.Length);
        Debug.Log(redTeam.Length);

        for(int i = 0; i < blueTeam.Length; i++){
            token.name.Add(blueTeam[i].GetState<IPlayerTeamState>().name);
            token.isBlueTeam.Add(blueTeam[i].GetState<IPlayerTeamState>().isBlueTeam);
        }
        for(int i = 0; i < redTeam.Length; i++){
            token.name.Add(redTeam[i].GetState<IPlayerTeamState>().name);
            token.isBlueTeam.Add(redTeam[i].GetState<IPlayerTeamState>().isBlueTeam);
        }
        token.playerAmount = token.name.Count;
        */
        BoltGameInfo.isBlueTeam = entity.GetState<IPlayerTeamState>().isBlueTeam;
        BoltNetwork.LoadScene(evnt.SceneName); 
    }
    public void OnBlueClicked(){
        entity.gameObject.GetComponent<BoltSelectTeam>().OnBlueClicked();
    }
    public void OnRedClicked(){
        entity.gameObject.GetComponent<BoltSelectTeam>().OnRedClicked();
    }
    public void OnStartBtnClicked(){
        if(BoltNetwork.IsServer){
            var loadSceneEvent = LoadSceneRequest.Create(GlobalTargets.Everyone);
            loadSceneEvent.SceneName = "GameNet";
            loadSceneEvent.Load = true;

            loadSceneEvent.Send();
        }
    }
}
