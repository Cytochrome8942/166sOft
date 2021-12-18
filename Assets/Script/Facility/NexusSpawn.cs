using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class NexusSpawn : MonoBehaviour
{
    public GameObject nexus;
    void Start(){
        if(BoltNetwork.IsServer){
            var newTower = BoltNetwork.Instantiate(nexus, transform.position, transform.rotation);
        }
    }
}