using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Normal : NumberMgr
{
    public bool isBreaking = false;
    private const int BrokeEvent=0, MissEvent=1;
    protected override void Appear(){}
    protected override void Disappear(){
        if(GameMgr.GameState == State.PLAY){
            if(isBreaking) return;
            if(myNum == GameMgr.currNum) NewNumber();
            else if(GameMgr.currNum < 10){
                if(isTSN && GameMgr.IsTSN((char)(GameMgr.currNum+48))) NewNumber();
                else MissNumber();
            }
            else {
                if(isTSN && GameMgr.IsTSN((char)((GameMgr.currNum%10)+48))) NewNumber();
                else if(isTSN && GameMgr.IsTSN((char)((GameMgr.currNum/10)+48))) NewNumber();
                else MissNumber();
            } 
        }
    }
    private IEnumerator Term(){
        yield return new WaitForSeconds(0.5f);
        //?��?��메이?��?���? ???�?
        transform.GetComponent<Image>().color = Color.white;
        transform.GetChild(0).GetComponent<Text>().color = Color.black;
    }
    protected override  void FadeIn(){}
    protected override  void FadeOut(){}
    protected override  void NewNumber(){
        GameMgr.inst.NormalBreakSound();
        if(GameMgr.currNum == GameMgr.inst.endValue) {
            GameMgr.inst.Clear();  
        }
        else{
            GameMgr.currNum++;
            GameMgr.inst.SetViewNumb(GameMgr.currNum, GameMgr.currNum-1);
            BrokeNumber();
            // transform.GetComponent<Image>().color = Color.clear;
            // transform.GetChild(0).GetComponent<Text>().color = Color.clear;
            //gameObject.SetActive(false);
            // 코루?�� -> Appear();
            // StartCoroutine(Term());
        }
    }
    protected override  bool IsCorrect(){return true;}
    public void BrokeNumber(){
        gameObject.GetComponent<Animation>().Play("BreakNumber");
    }
    public void SetNumber(){
        GameMgr.SetNumber(transform,26,51);
    }
    public void MissNumber(){
        GameMgr.inst.MissSound();
        gameObject.GetComponent<Animation>().Play("MissNumber");
        GameMgr.inst.TimeView.GetComponent<Animation>().Play("addTimeCuzMiss");
        missTime += 0.2f;
    }

}