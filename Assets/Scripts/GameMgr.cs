using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public enum State{
    NONE,
    PLAY,
    CLEAR,
    OPTION,
    Count
}

public class GameMgr : MonoBehaviour
{
    public static GameMgr inst;

    public static Dictionary<int,bool> IsExist = new Dictionary<int, bool>();
    private const int MAX =5;
    private const float PAD = 155.0f;
    public GameObject btn, gameView, Current, Counted;
    public Text showTime;
    public static int currNum;
    public static State GameState;
    private string time; // 시작시간, 걸린시간, ...
    public static DateTime dateTime,currTime;
    // (x,y) 좌상단부터 시작.
    public void Initialize(){
        currNum = 1;
        GameState = State.NONE;
        time = "00:00.0000";
        showTime.text = time;
        for(int y=0; y<MAX; ++y){
            for(int x=0; x<MAX; ++x){
                Transform tr = Instantiate(btn).transform;
                tr.name = ((x+1) + y*MAX).ToString();
                tr.SetParent(gameView.transform);
                tr.GetComponent<RectTransform>().anchoredPosition = new Vector3(x*PAD, -y*PAD,0);
                SetNumber( tr,1,26);
            }
        }
    }
    public void SetViewNumb(int curr, int bef){
        if(Counted.GetComponent<Animation>().isPlaying)
            Counted.GetComponent<Animation>().Stop();
        Current.GetComponent<Text>().text = curr.ToString();
        Counted.GetComponent<Text>().text = bef.ToString();
        Counted.GetComponent<Animation>().Play();
    }
    public static bool IsTSN(char n){
        return n=='3' || n == '6' || n == '9';
    }

    public void Clear(){
        MessageMgr.inst.Stop();
        Initialize();
    }
    private void SaveScore(){
        
    }
    private char ConvertIntToChar(int i){
        return (char)(i+48);
    }
    
    public static void SetNumber( Transform tr, int min , int max){
        int num;
        do{
            num = UnityEngine.Random.Range(min,max);
        }while(IsExist.ContainsKey(num));
        IsExist[num] = true;
        if(num.ToString().Length==1){
            if(IsTSN(num.ToString()[0])) {
                tr.GetChild(0).GetComponent<Text>().text = "★";
                tr.GetComponent<NumberMgr>().isTSN = true;
            }
            else tr.GetChild(0).GetComponent<Text>().text = num.ToString();

        }
        else if(num.ToString().Length==2){
            if(IsTSN(num.ToString()[0]) || IsTSN(num.ToString()[1])) {
                tr.GetChild(0).GetComponent<Text>().text = "★";
                tr.GetComponent<NumberMgr>().isTSN = true;
            }
            else tr.GetChild(0).GetComponent<Text>().text = num.ToString();
        }
        tr.GetComponent<Normal>().myNum = num;
    }

    private void Awake(){
        if(inst == null) inst = this;
        else if(inst != this) Destroy(gameObject);
        Initialize();
    }

    // 조작 관리
    private void Update(){
        if(GameState == State.NONE){
            if(Input.GetMouseButton(0)){
                MessageMgr.inst.CountDown();
            }
        } // 게임 대기
        else if(GameState == State.PLAY){
            TimeSpan curr = DateTime.Now-dateTime;
            showTime.text = curr.ToString().Substring(3,curr.ToString().Length-6);
        } // 게임 진행 , 시간 기록, 버튼 클릭
        else if(GameState == State.OPTION){} // 옵션
        else if(GameState ==State.CLEAR){} // 옵션2
    }
}
