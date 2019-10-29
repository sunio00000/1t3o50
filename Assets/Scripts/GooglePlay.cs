using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
public class GooglePlay : MonoBehaviour
{
    public static GooglePlay instance;

    public Text scoreText;
    public Text myLog;
    public RawImage myImage;
    public Button LoginBtn;

    private bool WaitingForAuth= false, IsSigned= false;

    void Awake()
    {
        if(instance == null) instance =this;
        else if(instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        if(myLog != null) myLog.text = "Ready...";
        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }
    void Start()
    {
        //doAutoLogin();
    }

    public void doAutoLogin(){
        myLog.text = "...";
        if(WaitingForAuth) return;

        if(!Social.localUser.authenticated){
            myLog.text = "Authenticating...";
            WaitingForAuth = true;

            Social.localUser.Authenticate(authenticateCallback);
            IsSigned = true;
        }
        else{
            myLog.text = "Login Fail\n";
        }
    }

    public void OnBtnLoginClicked(){
        myLog.text = "......";
        if(!IsSigned) StartCoroutine(AuthLoad());
        else OnBtnLogoutClicked();
        
    }
    public IEnumerator AuthLoad(){
        Social.localUser.Authenticate((bool success) =>{
            if(success){
                Debug.Log(Social.localUser.userName);
                myLog.text = "name: "+ Social.localUser.userName +"\n";
                StartCoroutine(UserPictureLoad());
                IsSigned = true;
            }
            else
            {
                Debug.Log("Login Fail");
                myLog.text=  "Login Fail\n";
            }
        });
        yield return new WaitForSeconds(0.1f);
    }
    public void OnBtnLogoutClicked(){
        ((PlayGamesPlatform)Social.Active).SignOut();
        myLog.text = "LogOut...";
        IsSigned= false;
    }

    void authenticateCallback(bool success){
        if(success){
            myLog.text = "Welcome"+ Social.localUser.userName+"\n";
            StartCoroutine(UserPictureLoad());
        }
        else{
            myLog.text = "Login Fail\n";
        }
    }
    IEnumerator UserPictureLoad(){
        Texture2D pic = Social.localUser.image;

        while(pic == null){
            pic = Social.localUser.image;
            yield return null;
        }

        myImage.texture = pic;
    }    
    public void ReportToBoard(long score){
        PlayGamesPlatform.Instance.ReportScore(score, GPGSIds.leaderboard_1to50ver369, (bool success)=>{
            if(success){

            }
            else{

            }
        });
    }
    public void ShowLeaderBoard(){
        if(Social.localUser.authenticated == false){
            return;
            Social.localUser.Authenticate((bool success)=>{
                if(success){
                    Social.ShowLeaderboardUI();
                    return;
                }
                else{
                    // auth identification needed.
                    return;
                }
            });
        }
        PlayGamesPlatform.Instance.ShowLeaderboardUI();
    }

    public void UnlockAchievment(int score){
        if(score >=100){
            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.leaderboard_1to50ver369, 100f, null);
        }
    }
    void Update()
    {
        
    }
}
