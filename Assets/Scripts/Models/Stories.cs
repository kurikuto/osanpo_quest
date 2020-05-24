using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;

// ストーリー情報を管理する
public class Stories
{
    private List<string> stories;
    // 進行状況
    public PlayerStatus playerStatus;
    // 読み込むjson
    public Dictionary<string, object> textDictionary;

    public Stories(string currentStory)
    {
        playerStatus = PlayerStatus.GetInstance();
        stories = new List<string>() { "beginning", "interval", "ending" };

        // 該当するストーリーのjsonファイルをロード
        TextAsset asset = Resources.Load(currentStory) as TextAsset;
        string jsonText = asset.text;

        textDictionary = (Dictionary<string, object>)Json.Deserialize(jsonText);
    }

    // 次のストーリーをセット
    public bool SetNextStory()
    {
        int currentIndex = stories.IndexOf(playerStatus.CurrentStory);

        // 最後まで終わっている場合
        if (currentIndex == (stories.Count - 1)) return false;

        string nextStory = stories[(currentIndex + 1)];
        playerStatus.CurrentStory = nextStory;
        return true;
    }
}
