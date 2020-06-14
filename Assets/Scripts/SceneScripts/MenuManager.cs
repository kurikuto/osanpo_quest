using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// メニュー画面を管理する
public class MenuManager : MonoBehaviour
{
    // シーン移動管理
    public SceneTransitionManager sceneTransitionManager;
    // 進行状況
    public PlayerStatus playerStatus;

    private void Start()
    {
        playerStatus = PlayerStatus.GetInstance();
        Debug.Log(playerStatus.WalkCount.ToString());
        Debug.Log(playerStatus.CurrentStory.ToString());
    }

    // ボタン押下
    public void OnClickButton(string sceneName)
    {
        // クリック音
        SoundManager.instance.PlaySE(0);
        Debug.Log("ボタンがクリックされました。");
        // 画面遷移（暫定）
        sceneTransitionManager.LoadTo(sceneName);
    }

    // 記録帳
    public void SaveData()
    {
        if (EditorUtility.DisplayDialog("記録帳", "記録帳にこれまでの出来事を書き残しますか？", "はい", "いいえ"))
        {
            // 保存
            playerStatus.Save();
            EditorUtility.DisplayDialog("記録帳", "記録帳に書き残しました。", "はい");
        }
        else
        {
            return;
        }
    }

}
