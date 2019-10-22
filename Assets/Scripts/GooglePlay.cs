using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
public class GooglePlay : MonoBehaviour
{
    public Text scoreText;
    public Text myLog;
    public RawImage myImage;
    public Button LoginBtn;

    private bool WaitingForAuth= false;

    void Awake()
    {
        myLog.text = "Ready...";
        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }
    void Start()
    {
        LoginBtn.onClick.AddListener(OnBtnLoginClicked);
        doAutoLogin();
    }

    public void doAutoLogin(){
        myLog.text = "...";
        if(WaitingForAuth) return;

        if(!Social.localUser.authenticated){
            myLog.text = "Authenticating...";
            WaitingForAuth = true;

            Social.localUser.Authenticate(authenticateCallback);
        }
        else{
            myLog.text = "Login Fail\n";
        }
    }

    public void OnBtnLoginClicked(){
        myLog.text = "......";
        StartCoroutine(AuthLoad());
        
    }
    public IEnumerator AuthLoad(){
        Social.localUser.Authenticate((bool success) =>{
            if(success){
                Debug.Log(Social.localUser.userName);
                myLog.text = "name: "+ Social.localUser.userName +"\n";
            }
            else
            {
                Debug.Log("Login Fail");
                myLog.text=  "Login Fail\n";
            }
        });
        yield return null;
    }
    public void OnBtnLogoutClicked(){
        ((PlayGamesPlatform)Social.Active).SignOut();
        myLog.text = "LogOut...";
    }

    void authenticateCallback(bool success){
        myLog.text= "Loading";
        if(success){
            myLog.text = "Welcome"+ Social.localUser.userName+"\n";
            StartCoroutine(UserPictureLoad());
        }
        else{
            myLog.text = "Login Fail\n";
        }
    }
    IEnumerator UserPictureLoad(){
        myLog.text = "image Loading...";
        Texture2D pic = Social.localUser.image;

        while(pic == null){
            pic = Social.localUser.image;
            yield return null;
        }

        myImage.texture = pic;
        myLog.text = "image Create";
    }    
    void Update()
    {
        
    }
}
