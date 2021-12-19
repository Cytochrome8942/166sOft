using UnityEngine;
using UnityEngine.UI;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using UdpKit.Platform.Photon;
using UdpKit;
using System.Collections.Generic;

public class BoltInPlayCallback : GlobalEventListener
{
    public GameObject playerMale;
    public GameObject playerFemale;

    public BoltEntity entity;
    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        if(BoltGameInfo.isBlueTeam){
            entity = BoltNetwork.Instantiate(playerMale, new Vector3(107, 0, 0), Quaternion.identity);
        }
        else{
            entity = BoltNetwork.Instantiate(playerFemale, new Vector3(-107, 0, 0), Quaternion.identity);
        }
    }
}
