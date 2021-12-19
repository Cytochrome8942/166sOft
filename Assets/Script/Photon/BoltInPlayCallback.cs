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

    public GameObject[] skills;

    public BoltEntity _entity;
    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        if(BoltGameInfo.isBlueTeam){
            _entity = BoltNetwork.Instantiate(playerMale, new Vector3(107, 0, 0), Quaternion.identity);
        }
        else{
            _entity = BoltNetwork.Instantiate(playerFemale, new Vector3(-107, 0, 0), Quaternion.identity);
        }
    }
    public override void OnEvent(SkillEvent evnt)
    {
        Debug.LogWarning("wow2");
        if(BoltNetwork.IsServer){
            Debug.LogWarning("wow");
            var obj = Instantiate(skills[evnt.SkillNum], evnt.Pos, Quaternion.identity);
            obj.GetComponent<CharacterSkill>().Initialize(evnt.Team, evnt.Damage);
        }
        else{
            var obj = Instantiate(skills[evnt.SkillNum], evnt.Pos, Quaternion.identity);
            obj.GetComponent<CharacterSkill>().Initialize(0, 0);
        }
    }
}
