using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class TowerSpawner : MonoBehaviour
{
    public GameObject tower;
    public bool isBlueTeam;
    public TowerInfo towerInfo;
    void Start(){
        if(BoltNetwork.IsServer){
            var newTower = BoltNetwork.Instantiate(tower, transform.position, transform.rotation);
            
            if(!isBlueTeam){
                towerInfo.team = 0;
                newTower.GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfo), null);
            }
            else{
                towerInfo.team = 1;
                newTower.GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfo), null);
            }
        }
    }
}
