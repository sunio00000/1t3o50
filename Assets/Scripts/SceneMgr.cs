using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneMgr : MonoBehaviour{
    public Button startBtn;
    private void Awake(){
        startBtn.onClick.AddListener(StartGame);
    }

    public void StartGame(){
        //GameMgr.inst.OnClick();
        SceneManager.LoadScene("MainScene");
    }
}