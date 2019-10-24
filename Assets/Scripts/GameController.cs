using UnityEngine;

public class GameController : MonoBehaviour{
    public static GameController instance;
    public GameObject OptionWindow, QuitRequestWindow;

    void Awake()
    {
        if(instance == null) instance = this;
        else if(instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GameMgr.GameState == State.NONE){
                // options
            }
            else if(GameMgr.GameState == State.PLAY){
                // pause
            }
        }
    }
    
    public void LeaderBoard(){
        Social.ShowLeaderboardUI();
    }
    public void Option(){
        OptionWindow.SetActive(true);
    }
    public void QuitRequest(){
        QuitRequestWindow.SetActive(true);
    }
    public void Quit(){
        Application.Quit();
    }
    public void QuitCancel(){
        QuitRequestWindow.SetActive(false);
    }
}