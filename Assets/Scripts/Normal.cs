using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Normal : NumberMgr
{
    protected override void Appear(){}
    protected override void Disappear(){
        if(myNum == GameMgr.currNum){
            GameMgr.currNum++;
            NewNumber();
            transform.GetComponent<Image>().color = Color.clear;
            transform.GetChild(0).GetComponent<Text>().color = Color.clear;
            //gameObject.SetActive(false);
            // 코루틴 -> Appear();
            StartCoroutine(Term());
        }
    }
    private IEnumerator Term(){
        yield return new WaitForSeconds(0.5f);
        transform.GetComponent<Image>().color = Color.white;
        transform.GetChild(0).GetComponent<Text>().color = Color.black;
    }
    protected override  void FadeIn(){}
    protected override  void FadeOut(){}
    protected override  void NewNumber(){
        GameMgr.SetNumber(transform,26,51);
    }
    protected override  bool IsCorrect(){return true;}
}