using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class RankMgr : MonoBehaviour
{
    public static RankMgr instance;
    public Sprite[] ranks, risefall;
    public Image myRankImage, RiseFall;
    public Text myRankGrade, allUsers;
    private int myRank;
    // Start is called before the first frame update
    public delegate void RankCallback();
    RankCallback callback;
    LeaderboardScoreData leaderboard;
    private void Awake() {
        leaderboard = new LeaderboardScoreData(Social.localUser.id);
        myRank = leaderboard.PlayerScore.rank;
        callback = SetStatus;
    }
    void Start()
    {
        callback();
    }

    void SetStatus(){
        int tmpRank =leaderboard.PlayerScore.rank;
        if(myRank<tmpRank) RiseFall.sprite = risefall[1];
        else if(myRank==tmpRank) RiseFall.sprite = risefall[0];
        else RiseFall.sprite = risefall[2];
    }

    void CalculateRank(int r){
        if(r==1) {
            myRankImage.sprite = ranks[0];
            myRankGrade.text = r.ToString();
        }
        else if(r<5) {
            myRankImage.sprite = ranks[1];
        }
        else if(r<10) {
            myRankImage.sprite = ranks[2];
        }
        else if(r<20){
            myRankImage.sprite = ranks[3];
        }
        else if(r<50){
            myRankImage.sprite = ranks[4];

        }
        else if(r<100){
            myRankImage.sprite = ranks[5];
        }
        else if(r<300){
            myRankImage.sprite = ranks[6];

        }
        else{
            myRankImage.sprite = ranks[7];
        }
        //i need rank percentage
        allUsers.text = leaderboard.Scores.Length.ToString();
    }
}
