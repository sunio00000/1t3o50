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
        OptionWindow.SetActive(true);
    }
    public void OptionCancel(){
        OptionWindow.SetActive(false);
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
}