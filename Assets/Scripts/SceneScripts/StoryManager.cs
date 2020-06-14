using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ストーリー進行
public class StoryManager : MonoBehaviour
{
    // シーン移動管理
    public SceneTransitionManager sceneTransitionManager;
    // ダイアログテキストを管理
    public DialogTextManager dialogTextManager;
    // ボタン
    public Canvas Buttons;
    // 現在のステージ
    public string currentStory;
    // ストーリー情報
    Stories story;
    // 進行状況
    public PlayerStatus playerStatus;
    // テキスト位置
    private int textIndex = 1;
    // 完了フラグ
    private bool isCompleted = false;

    // Start is called before the first frame update
    void Start()
    {
        ShowButtons(false);
        playerStatus = PlayerStatus.GetInstance();
        // 現在ステージ
        currentStory = playerStatus.CurrentStory;

        // 現在ステージに応じてストーリーを出し分ける。
        story = new Stories(currentStory);

        var textArray = (List<object>)story.textDictionary[textIndex.ToString()];

        //dialogTextManager.SetScenarios(new string[] { "おや・・・？", "（何か聞こえる・・。）" });

        ShowText(textArray);
    }

    public void ShowButtons(bool isTrue)
    {
        // 全テキスト表示が終わっていたら、ボタン表示せずメニューに戻る
        if (isCompleted)
        {
            BackToHome();
        }
        else
        {
            Buttons.gameObject.SetActive(isTrue);
        }
    }

    public void OnClickButton()
    {
        var textArray = (List<object>)story.textDictionary[textIndex.ToString()];
        ShowText(textArray);
    }

    void ShowText(List<object> textArray)
    {
        // ボタン非表示
        ShowButtons(false);

        // 文字列として詰め直し
        List<string> textList = new List<string>();
        foreach (object obj in textArray)
        {
            textList.Add((string)obj);
        }

        // テキスト表示
        dialogTextManager.SetScenarios(textList.ToArray());

        if (story.textDictionary.Count == textIndex)
        {
            isCompleted = true;
        }
        else
        {
            textIndex++;
        }
    }

    void BackToHome()
    {
        // ストーリー進行度増加
        if (story.SetNextStory())
        {
            // クリック音
            SoundManager.instance.PlaySE(0);
            // 画面遷移（暫定）
            sceneTransitionManager.LoadTo("Menu");
        }
    }

}
