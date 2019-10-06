using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class NumberMgr : MonoBehaviour
{
    
    public int myNum;
    protected void Initialize(){
        var b = GetComponent<Button>();
        b.onClick.AddListener(Disappear);
    }
    protected abstract void Appear();
    abstract protected void Disappear();
    abstract protected void FadeIn();
    abstract protected void FadeOut();
    abstract protected void NewNumber();
    abstract protected bool IsCorrect();

    protected void Awake(){
        Initialize();
    }
}
