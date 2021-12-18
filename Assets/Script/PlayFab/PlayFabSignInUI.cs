using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class PlayFabSignInUI : MonoBehaviour
{
    public GameObject playFabUI;
    public Text logText;
    [Header("Sign In")]
    public GameObject signIn;
    public Button signInBtn;
    public InputField emailInput, passInput;
    [Header("Sign Up")]
    public GameObject signUp;
    public Button signUpBtn;
    public InputField signUpEmailInput, signUpPassInput, signUpConfirmPassInput, signUpNNInput;

    public void Start()
    {
        signUpConfirmPassInput.onEndEdit.AddListener(ConfirmInput);
        signUpPassInput.onEndEdit.AddListener(ConfirmInput);
    }
    public void LoginBtn()
    {
        var request = new LoginWithEmailAddressRequest { Email = emailInput.text, Password = passInput.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, SignInCallBack);
        logText.text = "로그인 중...";
        signInBtn.interactable = false;
    }
    public void RegisterBtn()
    {
        if (signUpPassInput.text.Equals(signUpConfirmPassInput.text))
        {
            var request = new RegisterPlayFabUserRequest { Email = signUpEmailInput.text, Password = signUpPassInput.text, Username = signUpNNInput.text, DisplayName = signUpNNInput.text };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
            logText.text = "회원 가입 중...";
            signUpBtn.interactable = false;
        }
    }
    public void ConfirmInput(string a)
    {
        var cb = signUpConfirmPassInput.colors;
        if (signUpPassInput.text.Length < 6)
        {
            cb.normalColor = new Color(1, 0.5f, 0.5f, 1);
            signUpPassInput.colors = cb;
            cb.normalColor = Color.white;
            signUpConfirmPassInput.colors = cb;
        }
        else
        {
            cb.normalColor = new Color(0.5f, 1f, 0.5f, 1);
            signUpPassInput.colors = cb;
            if (!signUpPassInput.text.Equals(signUpConfirmPassInput.text))
            {
                cb.normalColor = new Color(1, 0.5f, 0.5f, 1);
                signUpConfirmPassInput.colors = cb;
            }
            else
            {
                cb.normalColor = new Color(0.5f, 1f, 0.5f, 1);
                signUpConfirmPassInput.colors = cb;
            }
        }

    }
    private void OnLoginSuccess(LoginResult result)
    {
        print("로그인 1단계 성공");

        PlayFabClient.initPlayerData(SignInCallBack, result);

        logText.text = "데이터 가져오는 중...";
    }
    private void SignInCallBack(PlayFabError error)
    {
        if (error == null)
        { // success
            PlayFabClient.instance.StartRenewExpireTime();
            GetComponent<BoltLobbyScene>().RefreshStatusText();
            BoltCustomClient.StartClient();
            logText.text = "인증 성공! : 서버에 연결하는 중...";
        }
        else
        {
            logText.text = "로그인 실패 : " + error.ErrorMessage;
            if (error.CustomData != null && (bool)error.CustomData)
            {
                logText.text = "로그인 실패 : 이미 로그인된 클라이언트가 있습니다.";
            }
        }
        signInBtn.interactable = true;
    }
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        logText.text = "회원가입 성공!";
        signIn.SetActive(true);
        signUp.SetActive(false);

        signUpBtn.interactable = true;
    }
    private void OnRegisterFailure(PlayFabError error)
    {
        logText.text = "회원가입 실패 : ";
        switch (error.Error)
        {
            case PlayFabErrorCode.EmailAddressNotAvailable:
                logText.text = logText.text + "중복된 이메일 입니다.";
                break;
            case PlayFabErrorCode.InvalidParams:
                logText.text = logText.text + "이메일 형식이 잘못되었습니다.";
                break;
            case PlayFabErrorCode.UsernameNotAvailable:
                logText.text = logText.text + "중복된 Nick Name 입니다.";
                break;
            default:
                logText.text = logText.text + "알 수 없는 에러 " + error.Error;
                break;
        }
        signUpBtn.interactable = true;
    }

}