using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneMgr : MonoBehaviour{
    public Button startBtn, OptionBtn, QuitBtn, leaderBtn, bQuit, bCancel;

    public GameObject OptionWindow, QuitRequestWindow;
    public Text warn;
    private void Awake(){
        startBtn.onClick.AddListener(StartGame);
        //OptionBtn.onClick.AddListener(Option);
        QuitBtn.onClick.AddListener(QuitRequest);
        bQuit.onClick.AddListener(Quit);
        bCancel.onClick.AddListener(QuitCancel);
        leaderBtn.onClick.AddListener(GooglePlay.ShowLeaderBoard);
    }

    public void StartGame(){
        //GameMgr.inst.OnClick();
        if(Social.localUser.authenticated){
            SceneManager.LoadScene("MainScene");
        }
        else{
            StartCoroutine(Warn("You need to login.",Color.red));
        }
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
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GameMgr.GameState == State.QUIT) GameController.instance.Quit();
            else    GameController.instance.QuitRequest();
        }
    }

    public void Option(){
        OptionWindow.SetActive(true);
    }
    public void OptionCancel(){
        OptionWindow.SetActive(false);
    }
    public void QuitRequest(){
        QuitRequestWindow.SetActive(true);
        GameMgr.GameState = State.QUIT;
    }
    public void Quit(){
        Application.Quit();
    }
    public void QuitCancel(){
        QuitRequestWindow.SetActive(false);
        GameMgr.GameState = State.NONE;
    }

}