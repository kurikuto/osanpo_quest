using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ホーム画面を管理する
public class HomeManager : MonoBehaviour
{
    // シーン移動管理
    public SceneTransitionManager sceneTransitionManager;
    // ダイアログテキストを管理
    public DialogTextManager dialogTextManager;
    // ボタン
    public Canvas Buttons;

    private void Start()
    {
        Buttons.gameObject.SetActive(false);
        dialogTextManager.SetScenarios(new string[] { "暇だなあ・・。" });
    }

    public void ShowButtons()
    {
        Buttons.gameObject.SetActive(true);
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

}
