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
    public Text myRankGrade;
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
        myRankGrade.text = tmpRank.ToString();
        if(myRank<tmpRank) RiseFall.sprite = risefall[1];
        else if(myRank==tmpRank) RiseFall.sprite = risefall[0];
        else RiseFall.sprite = risefall[2];
    }

    void CalculateRank(){
        PlayGamesPlatform.Instance
    }
}
