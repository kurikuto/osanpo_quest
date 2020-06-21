using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// プレイヤー情報
[Serializable]
public class PlayerStatus
{
    // 歩いた回数
    [SerializeField]
    private int walkCount;
    // ストーリー進行状況
    [SerializeField]
    private string currentStory;
    // アイテム一覧
    [SerializeField]
    private List<string> items;
    // セーブ時のキー
    [SerializeField]
    const string SAVEKEY = "STATUS-SAVE-KEY";
    private static PlayerStatus instance = null;

    // コンストラクタ
    public PlayerStatus()
    {
        walkCount = 0;
        CurrentStory = "beginning";
        //instance = this;
    }

    // 各シーンからの呼び出し
    static public PlayerStatus GetInstance()
    {
        if (instance == null)
        {
            instance = new PlayerStatus();
        }
        return instance;
    }

    //// シングルトン
    //public static PlayerStatus instance;
    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;

    //        // defalt
    //        walkCount = 0; // 暫定
    //        CurrentStory = "beginning"; // 暫定

    //        DontDestroyOnLoad(this.gameObject);
    //    }
    //    else
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}

    // 歩いた回数のプロパティ
    public int WalkCount
    {
        //set { this.walkCount = value; }
        get { return this.walkCount; }
    }

    // 歩いた回数増加
    public int AddWalkCount()
    {
        this.walkCount++;
        return this.walkCount;
    }

    // ストーリー進行のプロパティ
    public string CurrentStory
    {
        set { this.currentStory = value; }
        get { return this.currentStory; }
    }

    // アイテム一覧のプロパティ
    public List<string> Items
    {
        get { return this.items; }
    }

    // アイテムゲット
    public void GetItem(string item)
    {
        this.items.Add(item);
    }

    // セーブ
    public void Save()
    {
        PlayerPrefs.SetString(SAVEKEY, JsonUtility.ToJson(this));
        PlayerPrefs.Save();
    }

    // ロード
    public void Load()
    {
        string jsonStatus = PlayerPrefs.GetString(SAVEKEY, JsonUtility.ToJson(new PlayerStatus()));
        instance = JsonUtility.FromJson<PlayerStatus>(jsonStatus);
    }

    // データ削除（はじめから）
    public void DeleteSavedData()
    {
        PlayerPrefs.DeleteKey(SAVEKEY);
        PlayerPrefs.Save();
        Load();
    }
}
