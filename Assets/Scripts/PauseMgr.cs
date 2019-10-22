using UnityEngine;
using UnityEngine.UI;

public class PauseMgr : MonoBehaviour{
    public static PauseMgr instance;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if(instance == null ) instance = this;
        else if(instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    public void Cancel(){

    }

}