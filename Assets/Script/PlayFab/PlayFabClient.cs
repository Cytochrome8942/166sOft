using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;

public class PlayFabClient : MonoBehaviour
{
    public static PlayFabClient instance;
    private System.Action<PlayFabError> _LogInCallback;
    private System.Action<PlayFabError> _RenewCallback;
    private string _userID;
    private LoginResult _login;
    public string userID
    {
        get
        {
            return _userID;
        }
        set
        {
            _userID = userID;
        }
    }
    private string _displayName;
    public string displayName
    {
        get
        {
            return _displayName;
        }
    }

    //////////////////////////////////////////////////
    /*    ON SIGN IN                               */
    //////////////////////////////////////////////////
    public static void initPlayerData(System.Action<PlayFabError> callback, LoginResult login){
        if(instance == null){
            instance = new GameObject().AddComponent<PlayFabClient>();
            instance.name = "PlayFabClient";
            DontDestroyOnLoad(instance);
        }
        instance._login = login;
        instance._LogInCallback = callback;
        instance.DuplicateCheck();
    }
    private void DuplicateCheck(){
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest(){
            FunctionName = "onSignIn",
            FunctionParameter = new { sessionTicket = _login.SessionTicket}
        }, GetProfiles, LoginErrorCtrl);
    }
    private void GetProfiles(ExecuteCloudScriptResult result){
        JsonObject jsonResult = (JsonObject)result.FunctionResult;
        jsonResult.TryGetValue("Status", out object Status);
        
        if(((string)Status).Equals("success")){
            _userID = _login.PlayFabId;
            var request = new GetPlayerProfileRequest { PlayFabId = _userID, ProfileConstraints = new PlayerProfileViewConstraints { ShowDisplayName = true } };
            PlayFabClientAPI.GetPlayerProfile(request, SetProfileData, LoginErrorCtrl);
        }
        else{
            LoginErrorCtrl(new PlayFabError(){CustomData = true});
        }
    }
    private void SetProfileData(GetPlayerProfileResult response)
    {
        _displayName = response.PlayerProfile.DisplayName;
        _LogInCallback(null);
    }

    private void LoginErrorCtrl(PlayFabError error)
    {
        _LogInCallback(error);
    }

    //////////////////////////////////////////////////
    /*    Duplicate ExpireTime renew                */
    //////////////////////////////////////////////////
    public void renewExpireTime(System.Action<PlayFabError> callback){
        _RenewCallback = callback;
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest(){
            FunctionName = "renewExpireTime",
            FunctionParameter = new { sessionTicket = _login.SessionTicket}
        }, renewExpireSuccess, renewErrorCtrl);
    }
    private void renewExpireSuccess(ExecuteCloudScriptResult result){
        JsonObject jsonResult = (JsonObject)result.FunctionResult;
        jsonResult.TryGetValue("Status", out object Status);
        
        if(((string)Status).Equals("success")){
            _RenewCallback(null);
        }
        else{
            renewErrorCtrl(new PlayFabError(){CustomData = true});
        }
    }
    private void renewErrorCtrl(PlayFabError error){
        _RenewCallback(error);
    }
    public void StartRenewExpireTime(){
        StartCoroutine("renewExpireTimeCorutine");
    }
    private void RenewCallback(PlayFabError error){
        if(error == null)
            StartRenewExpireTime();
        else{
            string text = "갱신 실패 : " + error.ErrorMessage;
            if(error.CustomData != null && (bool)error.CustomData){
                text = "갱신 실패 : 갱신 시간 만료";
            }
            Debug.Log(text);
        }
    }

    IEnumerator renewExpireTimeCorutine(){
        yield return new WaitForSecondsRealtime(10f);
        Debug.Log("갱신 성공");
        renewExpireTime(RenewCallback);
    }
}
