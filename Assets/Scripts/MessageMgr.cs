using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MessageMgr : MonoBehaviour
{
    private void Awake()
    {
        if(inst == null) inst = this;
        else if(inst != this) Destroy(gameObject);           
    }
    public static MessageMgr inst;
    public Text msg; public GameObject go;
    public static bool isOn = false;
    public void CountDown(){
        if(isOn) return;
        isOn= true;
        StartCoroutine(countDown());
    }
    private IEnumerator countDown(){
        float start =3.99f;
        while(start >1.0f){
            msg.text = ((int)start).ToString();
            start -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        GameMgr.GameState = State.PLAY;
        isOn = false;
        go.SetActive(false);    
        GameMgr.dateTime = DateTime.Now;
        yield return null;
    }

    public void Stop(){
        go.SetActive(true);
        StartCoroutine(Clear());
    }

    private IEnumerator Clear(){
        float end = 0.0f;
        msg.text = GameMgr.inst.time + " Clear.";
        while(end<1.5f){
            end+=0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        msg.text = "START";
        GameMgr.inst.time = "00:00.0000";
    }
}
