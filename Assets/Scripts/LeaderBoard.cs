using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    public GameObject BoardGroup;
    public List<string> myScoreList= new List<string>(); // 7개의 기록만 남김.
    public string myTopScore, myCurrentScore;
    private const int TextChild=2;
    public string filePath;
    private void OnEnable()
    {
        ReadFromFile();
        SetBoard();
    }
    public void SetBoard(){
        for(int i=0; i<BoardGroup.transform.childCount; ++i){
            Transform score = BoardGroup.transform.GetChild(i).GetChild(TextChild);
            score.GetComponent<Text>().text = myScoreList[i];
        }
    }
    public void ReadFromFile(){
        filePath = FileMgr.path+"/LeaderBoard.txt";
        myScoreList = FileMgr.Read(filePath);
        // read from file and set string list scores
    }
}
