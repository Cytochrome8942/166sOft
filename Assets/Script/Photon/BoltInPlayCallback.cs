using UnityEngine;
using UnityEngine.UI;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using UdpKit.Platform.Photon;
using UdpKit;
using System.Collections.Generic;

public class BoltInPlayCallback : GlobalEventListener
{
    public GameObject player;
    public BoltEntity entity;
    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        Vector3 pos = new Vector3(80,0,0);
        
        if(BoltGameInfo.isBlueTeam){
            pos = new Vector3(-80,0,0);
            entity = BoltNetwork.Instantiate(player, pos, Quaternion.identity);
        }
        else{
            pos = new Vector3(80,0,0);
            entity = BoltNetwork.Instantiate(player, pos, Quaternion.identity);
        }

    }
}
