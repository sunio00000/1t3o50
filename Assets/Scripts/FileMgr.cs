using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class FileMgr : MonoBehaviour
{
    public static string path;

    private void Awake(){
        path = Application.dataPath+"/Users";
    }
    public static void CreateFile(string filePath){
        if(!ExistFile(path)) Directory.CreateDirectory(path);
        if(File.Exists(filePath)) return;
        else File.CreateText(filePath);
    }

    public static bool ExistFile(string filePath){
        if(Directory.Exists(filePath)) return true;
        else return false;
    }

    public static void Write(string filePath, List<string> content){
        CreateFile(filePath);
        string contents="";
        foreach(var value in content) contents+= value+"\n"; 
        File.WriteAllText(filePath,contents);
    }

    public static List<string> Read(string filePath){
        CreateFile(filePath);
        string reads = File.ReadAllText(filePath);
        List<string> readDatas = new List<string>(reads.Split('\n'));
        readDatas.Remove("");
        return readDatas;
    }
}
