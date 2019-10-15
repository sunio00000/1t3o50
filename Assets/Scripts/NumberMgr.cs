using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

abstract public class NumberMgr : MonoBehaviour
{
    public bool isTSN = false;
    public static float missTime;
    public int myNum;
    protected void Initialize(){
        missTime = 0.0f;
        var b = GetComponent<Button>();
        b.onClick.AddListener(Disappear);
    }
    protected abstract void Appear();
    abstract protected void Disappear();
    abstract protected void FadeIn();
    abstract protected void FadeOut();
    abstract protected void NewNumber();
    abstract protected bool IsCorrect();

    public static TimeSpan GetMissTime(){
        return TimeSpan.FromSeconds(missTime);
    }
    protected void Awake(){
        Initialize();
    }
}
