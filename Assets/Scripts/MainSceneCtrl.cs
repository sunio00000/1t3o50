using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MainSceneCtrl : MonoBehaviour{
    public GameObject PausePanel;

    private void OnEnable(){
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Home) || Input.GetKeyDown(KeyCode.Menu)){
            PausePanel.SetActive(true);
            GameMgr.GameState= State.PAUSE;
            Time.timeScale  = 0.0f;
        }
        else{
            if(Input.GetKeyDown(KeyCode.Escape)){
                if(GameMgr.GameState == State.NONE){
        // back     
                    SceneManager.LoadScene("StartScene");
                }
                else if(GameMgr.GameState == State.PLAY){
        // pause
                    Time.timeScale = 0.0f;
                    PausePanel.SetActive(true);
                    GameMgr.GameState = State.PAUSE;
                }
                else if(GameMgr.GameState == State.PAUSE){
                    Time.timeScale = 1.0f;
                    PausePanel.SetActive(false);
                    GameMgr.GameState = State.PLAY;
                }
            }
        }
    }
}