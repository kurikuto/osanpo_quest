using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WalkingManager : MonoBehaviour
{
    // シーン移動管理
    public SceneTransitionManager sceneTransitionManager;
    // ダイアログテキストを管理
    public DialogTextManager dialogTextManager;
    // ボタン
    public Canvas Buttons;
    // 背景
    [SerializeField] GameObject background;
    // 歩いた回数
    private int walkCount;
    // 進行状況
    public PlayerStatus playerStatus;
    // イベント発生ステージ（暫定）
    readonly int[] eventStages = { 3, 7, 15, 20 };

    private void Start()
    {
        ShowButtons(false);
        playerStatus = PlayerStatus.GetInstance();
        dialogTextManager.SetScenarios(new string[] { "いい天気だな・・。", "どこへいこう？" });
    }

    public void ShowButtons(bool isTrue)
    {
        Buttons.gameObject.SetActive(isTrue);
    }

    // 進むボタン押下後
    IEnumerator Walking()
    {
        //dialogTextManager.SetScenarios(new string[] { "てくてく…" });

        // 拡大→完了後戻し
        background.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 2f)
            .OnComplete(() => background.transform.localScale = new Vector3(1, 1, 1));
        SpriteRenderer backgrounfRenderer = background.GetComponent<SpriteRenderer>();
        // フェードアウト→完了後戻し
        backgrounfRenderer.DOFade(0, 2f)
            .OnComplete(() => backgrounfRenderer.DOFade(1, 0));

        // 2秒間処理を待機させる
        yield return new WaitForSeconds(2f);

        walkCount = playerStatus.AddWalkCount();
        Debug.Log("歩いた回数：" + walkCount);

        bool isEventStage = false;
        foreach (int eventStage in eventStages)
        {
            if(walkCount == eventStage)
            {
                isEventStage = true;
            }
        }

        if (isEventStage)
        {
            Debug.Log("イベント発生！");
            DoEvent();
        }
        else
        {
            dialogTextManager.SetScenarios(new string[] { "ふう・・。" });
        }
    }

    // 進むボタン押下
    public void OnNextButton()
    {
        SoundManager.instance.PlaySE(0);
        // 二度押しさせないためボタン非表示
        ShowButtons(false);

        StartCoroutine(Walking());
    }


    // 戻るボタン押下
    public void OnBackButton(string sceneName)
    {
        // クリック音
        SoundManager.instance.PlaySE(0);
        Debug.Log("ボタンがクリックされました。");
        // 画面遷移（暫定）
        sceneTransitionManager.LoadTo(sceneName);
    }

    // ストーリー進行
    void DoEvent()
    {
        // クリック音
        SoundManager.instance.PlaySE(0);
        // 画面遷移（暫定）
        sceneTransitionManager.LoadTo("Story");
    }
}