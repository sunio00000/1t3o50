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
    PAUSE,
    Count
}

public class GameMgr : MonoBehaviour
{
    public AudioSource audioMiss, btnOnClick;
    public static GameMgr inst;
    public RawImage IdPicture;
    public Text GoogleId;
    public static Dictionary<int,bool> IsExist = new Dictionary<int, bool>();
    public int MAX =5, endValue = 10;
    private const float PAD = 157.0f;
    public GameObject btn, gameView, Current, Counted, TimeView;
    public Text showTime;
    public static int currNum;
    public static State GameState;
    public string recentRecord;
    public string time; // ???????????, �ɸ�?????, ...
    public static DateTime dateTime,currTime;
    public static TimeSpan reserveTime;
    // (x,y) �»�???????? ??????.
    public void Initialize(){
        currNum = 1;
        GameState = State.NONE;
        showTime.text = time;
        IsExist.Clear();
    }
    public void CreateTiles(){
        for(int y=0; y<MAX; ++y){
            for(int x=0; x<MAX; ++x){
                Transform tr = Instantiate(btn).transform;
                tr.name = ((x+1) + y*MAX).ToString();
                tr.SetParent(gameView.transform);
                tr.GetComponent<RectTransform>().anchoredPosition = new Vector3(-313.0f + x*PAD, 317.0f-y*PAD,0);
                SetNumber( tr,1,26);
            }
        }
    }
    public void RefreshTiles(){
        Transform parent = gameView.transform;
        for(int child=0; child<parent.childCount;++child){
            SetNumber(parent.GetChild(child),1,26);
        }
    }

    public void OnClick(){
        btnOnClick.Play();
    }
    public void MissSound(){
        audioMiss.Play();
    }
    public void NormalBreakSound(){
        gameObject.GetComponent<AudioSource>().Play();
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
        GooglePlay.instance.ReportToBoard(100);
        //SaveScore();
        Initialize();
        RefreshTiles();
    }
    private void SaveScore(){
        List<string> scores = new List<string>(FileMgr.Read(LeaderBoard.filePath));
        scores.Add(time); scores.Sort();
        recentRecord = time;
        FileMgr.Write(LeaderBoard.filePath, scores);
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
                tr.GetChild(0).GetComponent<Text>().text = "#";
                tr.GetChild(0).GetComponent<Text>().color = Color.red;
                tr.GetComponent<NumberMgr>().isTSN = true;
            }
            else {
                tr.GetChild(0).GetComponent<Text>().text = num.ToString();
                tr.GetChild(0).GetComponent<Text>().color = new Color(0.4f,0.4f,0.4f,1);
            }

        }
        else if(num.ToString().Length==2){
            if(IsTSN(num.ToString()[0]) || IsTSN(num.ToString()[1])) {
                tr.GetChild(0).GetComponent<Text>().text ="#";
                tr.GetChild(0).GetComponent<Text>().color = Color.red;
                tr.GetComponent<NumberMgr>().isTSN = true;
            }
            else{
                tr.GetChild(0).GetComponent<Text>().text = num.ToString();
                tr.GetChild(0).GetComponent<Text>().color = new Color(0.4f,0.4f,0.4f,1);
            } 
        }
        tr.GetComponent<Normal>().myNum = num;
    }

    public string TimeToString(TimeSpan ts){
        return ts.ToString().Substring(3,ts.ToString().Length-6);
    }
    private void Awake(){
        if(inst == null) inst = this;
        else if(inst != this) Destroy(gameObject);
        time = "00:00.0000";
        Initialize(); CreateTiles();
    }

    private  void Start()
    {
        GoogleId.text = Social.localUser.userName;
        IdPicture.texture = Social.localUser.image;
    }

    // ?????? ???��?????.
    private void Update(){
        if(OptionMgr.isOpened) return;
        if(GameState == State.NONE){} // ���� ?????
        else if(GameState == State.PLAY){
            TimeSpan curr = (DateTime.Now-dateTime)
                                        .Subtract(OptionMgr.acumTime) // delayed
                                        .Add(NumberMgr.GetMissTime()); // miss block
            time = showTime.text = TimeToString(curr);
        } // ���� ���� , ????? ���, ��ư ?????
        else if(GameState == State.OPTION){} // ??????
        else if(GameState ==State.CLEAR){} // ??????2
    }
    public void GameStart(){
        MessageMgr.inst.CountDown();
    }
}
