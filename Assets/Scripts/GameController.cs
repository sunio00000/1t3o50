using UnityEngine;

public class GameController : MonoBehaviour{
    public static GameController instance;
    public GameObject OptionWindow, QuitRequestWindow;
    public bool sound =true;
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
    public void Option(){
        if(OptionWindow == null) OptionWindow = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject;
        OptionWindow.SetActive(true);
    }
    public void OptionCancel(){
        if(OptionWindow == null) OptionWindow = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject;
        OptionWindow.SetActive(false);
    }
    public void QuitRequest(){
        if(QuitRequestWindow == null) QuitRequestWindow =GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).gameObject;
        QuitRequestWindow.SetActive(true);
        GameMgr.GameState = State.QUIT;
    }
    public void Quit(){
        Application.Quit();
    }
    public void QuitCancel(){
        if(QuitRequestWindow == null) QuitRequestWindow =GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).gameObject;
        QuitRequestWindow.SetActive(false);
        GameMgr.GameState = State.NONE;
    }

    public void SoundSetState(){
        if(sound){
            AudioListener.pause = true;
            sound =false; 

        }
        else{
            AudioListener.pause = false;
            sound =true;
        }

    }

    private void OnApplicationPause(bool pauseStatus) {
        
    }
}