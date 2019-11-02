using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneMgr : MonoBehaviour{
    public AudioSource btn;
    public Button startBtn, OptionBtn, QuitBtn, leaderBtn, bQuit, bCancel, LoginBtn;
    public Text gooScoreText , gooMyLog;
    public RawImage gooMyImage;

    public GameObject OptionWindow, QuitRequestWindow;
    public Text warn;
    private void Awake(){
        startBtn.onClick.AddListener(StartGame);
        //OptionBtn.onClick.AddListener(Option);
        QuitBtn.onClick.AddListener(QuitRequest);
        bQuit.onClick.AddListener(Quit);
        bCancel.onClick.AddListener(QuitCancel);
        leaderBtn.onClick.AddListener(GooglePlay.ShowLeaderBoard);
        leaderBtn.onClick.AddListener(delegate(){btn.Play();});
        GooglePlay.instance.tmp = this;
        GooglePlay.instance.myImage = gooMyImage;
        GooglePlay.instance.myLog = gooMyLog;
        GooglePlay.instance.scoreText = gooScoreText;
        LoginBtn.onClick.AddListener(GooglePlay.instance.OnBtnLoginClicked);
        LoginBtn.onClick.AddListener(delegate(){btn.Play();});
    }

    public void StartGame(){
        if(Social.localUser.authenticated){
            SceneManager.LoadScene("MainScene");
        }
        else{
            StartCoroutine(Warn("You need to login.",Color.red));
        }
        btn.Play();
    }
    public void Warnings(string msg, Color c){
        StartCoroutine(Warn(msg,c));
    }
    IEnumerator Warn(string msg, Color c){ 
        warn.gameObject.SetActive(true);
        warn.text = msg; warn.color = c;
        yield return new WaitForSeconds(1.5f);
        warn.gameObject.SetActive(false);
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Home) || Input.GetKeyDown(KeyCode.Menu)){

        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GameMgr.GameState == State.QUIT) GameController.instance.Quit();
            else    GameController.instance.QuitRequest();
        }
    }

    public void Option(){
        OptionWindow.SetActive(true);btn.Play();
    }
    public void OptionCancel(){
        OptionWindow.SetActive(false);btn.Play();
    }
    public void QuitRequest(){
        QuitRequestWindow.SetActive(true);
        GameMgr.GameState = State.QUIT;btn.Play();
    }
    public void Quit(){
        Application.Quit();btn.Play();
    }
    public void QuitCancel(){
        QuitRequestWindow.SetActive(false);
        GameMgr.GameState = State.NONE;btn.Play();
    }

}