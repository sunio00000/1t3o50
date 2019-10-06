using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum State{
    NONE,
    PLAY,
    CLEAR,
    OPTION,
    Count
}

public class GameMgr : MonoBehaviour
{
    public static Dictionary<int,bool> IsExist = new Dictionary<int, bool>();
    private const int MAX =5;
    private const float PAD = 155.0f;
    public GameObject btn, gameView;
    public static int currNum;
    public static State GameState;
    private string time; // 시작시간, 걸린시간, ...
    // (x,y) 좌상단부터 시작.
    private void Initialize(){
        currNum = 1;

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
    public static void SetNumber( Transform tr, int min , int max){
        int num;
        do{
            num = Random.Range(min,max);
        }while(IsExist.ContainsKey(num));
        IsExist[num] = true;
        tr.GetChild(0).GetComponent<Text>().text = num.ToString();
        tr.GetComponent<Normal>().myNum = num;
    }
    private void Awake(){
        Initialize();
    }

    // 조작 관리
    private void Update(){
        if(GameState == State.NONE){} // 게임 대기
        else if(GameState == State.PLAY){} // 게임 진행 , 시간 기록, 버튼 클릭
        else if(GameState == State.OPTION){} // 옵션
        else if(GameState ==State.CLEAR){} // 옵션2
    }
}
