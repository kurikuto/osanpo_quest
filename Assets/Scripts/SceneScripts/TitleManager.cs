using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// タイトル画面を管理する
public class TitleManager : MonoBehaviour
{
    // シーン移動管理
    public SceneTransitionManager sceneTransitionManager;
    // 進行状況
    public PlayerStatus playerStatus;

    public void Start()
    {
        //playerStatus = PlayerStatus.GetInstance();
        playerStatus.Load();
    }

    // はじめから
    public void OnClickFromBeginning(string sceneName)
    {

        // クリック音
        SoundManager.instance.PlaySE(0);
        Debug.Log("ボタンがクリックされました。");

        if (EditorUtility.DisplayDialog("はじめから", "前回までのセーブデータは削除されます。\nよろしいですか？", "はい", "いいえ"))
        {
            // セーブデータ初期化
            playerStatus.DeleteSavedData();
        }
        else
        {
            return;
        }

        // 画面遷移
        sceneTransitionManager.LoadTo(sceneName);
    }

    // つづきから
    public void OnClickContinue(string sceneName)
    {
        // クリック音
        SoundManager.instance.PlaySE(0);
        Debug.Log("ボタンがクリックされました。");

        //// ロード
        //PlayerStatus.instance.Load();

        // 画面遷移
        sceneTransitionManager.LoadTo(sceneName);

    }
}