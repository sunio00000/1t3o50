using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneMgr : MonoBehaviour{
    public Button startBtn;
    public Text warn;
    private void Awake(){
        startBtn.onClick.AddListener(StartGame);
    }

    public void StartGame(){
        //GameMgr.inst.OnClick();
        if(Social.localUser.authenticated){
            SceneManager.LoadScene("MainScene");
        }
        else{
            StartCoroutine(Warn());
        }
    }
    IEnumerator Warn(){ 
        warn.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        warn.gameObject.SetActive(false);
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GameMgr.GameState == State.QUIT) GameController.instance.Quit();
            else    GameController.instance.QuitRequest();
        }
    }
}