using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    public GameObject BoardGroup;
    public List<string> myScoreList= new List<string>(); // 7개의 기록만 남김.
    public string myTopScore, myCurrentScore;
    private const int TextChild=1;
    public static string filePath;
    private void Awake()
    {
    }
    private void OnEnable()
    {
        ReadFromFile();
        SetBoard();
    }
    public void SetBoard(){
        for(int i=0; i<BoardGroup.transform.childCount; ++i){
            Transform score = BoardGroup.transform.GetChild(i).GetChild(TextChild);
            if(myScoreList.Count<=i || myScoreList[i] =="")  score.GetComponent<Text>().text = "No Record";
            else {
                if(myScoreList[i] == GameMgr.inst.recentRecord)
                    score.GetComponent<Text>().color = Color.red;
                score.GetComponent<Text>().text = myScoreList[i];
            }
        }
    }
    public void ReadFromFile(){
        filePath = FileMgr.path+"/LeaderBoard.txt";
        myScoreList = FileMgr.Read(filePath);
        // read from file and set string list scores
    }
}
