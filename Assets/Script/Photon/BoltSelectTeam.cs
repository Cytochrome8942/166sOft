using UnityEngine;
using UnityEngine.UI;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using UdpKit.Platform.Photon;
using UdpKit;

public class BoltSelectTeam : EntityBehaviour<IPlayerTeamState>
{
    bool isWantChangeTeam;
    public Text nameText;
    GameObject blue, red;
    public override void Attached()
    {
        blue = GameObject.Find("Blue");
        red = GameObject.Find("Red");

        state.AddCallback("name", OnNameChanged);
        state.AddCallback("isBlueTeam", OnTeamChanged);
        if(entity.IsOwner){
            state.name = PlayFabClient.instance.displayName;
        }

        if(blue.GetComponentsInChildren<Transform>().Length <= red.GetComponentsInChildren<Transform>().Length){
            state.isBlueTeam = true;
        }
        else{
            state.isBlueTeam = false;
        }

        OnTeamChanged();
    }
    public override void SimulateOwner()
    {
        if(state.isRequestChangeTeam){
            state.isBlueTeam = !state.isBlueTeam;
            state.isRequestChangeTeam = false;
        }
        
    }
    public override void SimulateController()
    {
        if(isWantChangeTeam){
            state.isRequestChangeTeam = true;
            isWantChangeTeam = false;
        }
    }
    public void OnNameChanged(){
        nameText.text = state.name;
    }
    public void OnTeamChanged(){
        if(state.isBlueTeam){
            gameObject.transform.SetParent(blue.transform);
        }
        else{
            gameObject.transform.SetParent(red.transform);
        }
    }
    public void OnBlueClicked(){
        if(!state.isBlueTeam){
            isWantChangeTeam = true;
        }
    }
    public void OnRedClicked(){
        if(state.isBlueTeam){
            isWantChangeTeam = true;
        }
    }
}
