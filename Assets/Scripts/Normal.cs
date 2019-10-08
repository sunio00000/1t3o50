using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Normal : NumberMgr
{
    protected override void Appear(){}
    protected override void Disappear(){
        if(GameMgr.GameState == State.PLAY){
            if(myNum == GameMgr.currNum) NewNumber();
            else if(GameMgr.currNum < 10){
                if(isTSN && GameMgr.IsTSN((char)(GameMgr.currNum+48))) NewNumber();
            }
            else {
                if(isTSN && GameMgr.IsTSN((char)((GameMgr.currNum%10)+48))) NewNumber();
                else if(isTSN && GameMgr.IsTSN((char)((GameMgr.currNum/10)+48))) NewNumber();
            } 
        }
    }
    private IEnumerator Term(){
        yield return new WaitForSeconds(0.5f);
        //애니메이션으로 대체
        transform.GetComponent<Image>().color = Color.white;
        transform.GetChild(0).GetComponent<Text>().color = Color.black;
    }
    protected override  void FadeIn(){}
    protected override  void FadeOut(){}
    protected override  void NewNumber(){
        if(GameMgr.currNum == 50) GameMgr.inst.Clear();
        else{
            GameMgr.currNum++;
            GameMgr.inst.SetViewNumb(GameMgr.currNum, GameMgr.currNum-1);
            GameMgr.SetNumber(transform,26,51);
            transform.GetComponent<Image>().color = Color.clear;
            transform.GetChild(0).GetComponent<Text>().color = Color.clear;
            //gameObject.SetActive(false);
            // 코루틴 -> Appear();
            StartCoroutine(Term());
        }
    }
    protected override  bool IsCorrect(){return true;}
}