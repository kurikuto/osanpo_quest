using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// シーンの移動を管理する
public class SceneTransitionManager : MonoBehaviour
{
    public void LoadTo(string sceneName)
    {
        // フェードアウト処理にロード処理を渡す
        FadeIOManager.instance.FadeOutToIn(() => Load(sceneName));
    }

    // 画面遷移
    public void Load(string sceneName)
    {
        // BGM切り替え
        SoundManager.instance.PlayBGM(sceneName);
        // 遷移先シーンのロード
        SceneManager.LoadScene(sceneName);
    }
}
