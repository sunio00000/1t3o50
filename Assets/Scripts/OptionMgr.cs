using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class OptionMgr : MonoBehaviour
{
    public Button LeaderBoard, Setting, Secret, Post;
    public GameObject gLeaderBoard, gSetting, gSecret, gPost;
    private Button ClosePanel;
    private GameObject currOption;
    public static bool isOpened;
    public static DateTime openTime, closeTime;
    public static TimeSpan acumTime;
    public Sprite soundOn , soundOff;
    void Start()
    {
        openTime = closeTime = DateTime.Now;
        acumTime = closeTime-openTime;
        isOpened = false;
        LeaderBoard.onClick.AddListener(delegate(){Open(gLeaderBoard);});
        Setting.onClick.AddListener(delegate(){GameController.instance.Option();});
        Post.onClick.AddListener(delegate(){GameController.instance.SoundSetState();});
        Post.onClick.AddListener(delegate(){SoundSprite(GameController.instance.sound);});
        gLeaderBoard.GetComponent<LeaderBoard>().ReadFromFile();
        //Secret.onClick.AddListener(delegate(){Open("Secret");});
    }

    public void SoundSprite(bool state){
        if(state){
            Post.GetComponent<Image>().sprite = soundOn;
        }
        else{
            Post.GetComponent<Image>().sprite = soundOff;
        }
    }
    private void Open(GameObject go){
        if(isOpened) return;
        GameMgr.inst.OnClick();
        openTime = DateTime.Now;
        isOpened = true;
        currOption = go;
        go.SetActive(true);
    }
    public void Close(){
        //Save();
        isOpened = false;
        GameMgr.inst.OnClick();
        closeTime = DateTime.Now;
        acumTime = Delay();
        currOption.SetActive(false);
    }

    public static TimeSpan Delay(){
        return  acumTime.Add(closeTime - openTime);
    }
    
}
