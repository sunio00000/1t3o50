using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionMgr : MonoBehaviour
{
    public Button LeaderBoard, Setting, Secret, Post;
    public GameObject gLeaderBoard, gSetting, gSecret, gPost;
    private Button ClosePanel;
    private GameObject currOption;
    public static bool isOpened;
    void Awake()
    {
        isOpened = false;
        LeaderBoard.onClick.AddListener(delegate(){Open(gLeaderBoard);});
        Setting.onClick.AddListener(delegate(){Open(gSetting);});
        Post.onClick.AddListener(delegate(){Open(gPost);});
        //Secret.onClick.AddListener(delegate(){Open("Secret");});
    }

    private void Open(GameObject go){
        if(isOpened) return;
        //Load();
        isOpened = true;
        currOption = go;
        go.SetActive(true);
    }
    public void Close(){
        //Save();
        isOpened = false;
        currOption.SetActive(false);
    }
}
