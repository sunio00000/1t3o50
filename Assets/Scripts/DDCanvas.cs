using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDCanvas : MonoBehaviour
{
    public static DDCanvas instance;
    // Start is called before the first frame update
    private void Awake(){
        if(instance==null) instance = this;
        else if(instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }        
}
