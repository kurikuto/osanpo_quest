using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ストーリー進行
public class StoryManager : MonoBehaviour
{
    // シーン移動管理
    public SceneTransitionManager sceneTransitionManager;
    // 背景を管理
    public BackgroundManager backgroundManager;
    // ダイアログテキストを管理
    public DialogTextManager dialogTextManager;
    // ボタン
    public Button button1;
    public Button button2;
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
    // ボタンテキスト1
    [SerializeField] Text button1Text;
    // ボタンテキスト2
    [SerializeField] Text button2Text;

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
        var buttonTextArray = (List<object>)story.buttonTextDictionary[textIndex.ToString()];

        // 背景をセット
        backgroundManager.SetImage(currentStory);

        //dialogTextManager.SetScenarios(new string[] { "おや・・・？", "（何か聞こえる・・。）" });

        ShowText(textArray, buttonTextArray);
    }

    public void ShowButtons(bool isTrue = true)
    {
        // 全テキスト表示が終わっていたら、ボタン表示せずメニューに戻る
        if (isCompleted)
        {
            BackToHome();
        }
        else
        {
            button1.gameObject.SetActive(isTrue);
            if (button2Text.text != "") button2.gameObject.SetActive(isTrue);
        }
    }

    public void OnClickButton()
    {
        var textArray = (List<object>)story.textDictionary[textIndex.ToString()];
        var buttonTextArray = (List<object>)story.buttonTextDictionary[textIndex.ToString()];

        ShowText(textArray, buttonTextArray);
    }

    void ShowText(List<object> textArray, List<object> buttonTextArray)
    {
        // ボタン非表示
        ShowButtons(false);

        // 文字列として詰め直し
        List<string> textList = new List<string>();
        foreach (object obj in textArray)
        {
            textList.Add((string)obj);
        }

        // ボタン用テキストをセット(暫定)
        button1Text.text = (string)buttonTextArray[0];
        button2Text.text = (buttonTextArray.Count > 1) ? (string)buttonTextArray[1] : "";

        // ストーリーテキスト表示
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
            // 画面遷移
            sceneTransitionManager.LoadTo("Menu");
        } else
        {
            // TODO：エンドロールを出す

            // クリック音
            SoundManager.instance.PlaySE(0);
            // 画面遷移（暫定）
            sceneTransitionManager.LoadTo("Menu");

        }
    }

}
