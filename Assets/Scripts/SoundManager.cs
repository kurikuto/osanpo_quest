using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 音声を管理
public class SoundManager : MonoBehaviour
{
    // BGM用定数
    private const int TITLE = 0;
    private const int MENU = 1;
    private const int HOME = 2;
    //private const int ITEMS = ; // MENUと同じ
    private const int WALKING = 3;
    private const int ENDROLL = 4;

    // シングルトン
    // ：シーン間でのデータ/オブジェクト共有
    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // シングルトン終わり

    public AudioSource audioSourceBGM; // BGMのスピーカー
    public AudioClip[] audioClipBGM; // 素材の配列

    public AudioSource audioSourceSE; // SEのスピーカー
    public AudioClip[] audioClipSE; // 素材の配列

    //void Start()
    //{
    //}

    // BGMを設定
    public void PlayBGM(string sceneName)
    {
        // 止める
        audioSourceBGM.Stop();

        // シーン名で出しわけ
        switch (sceneName)
        {
            default:
            case "Title":
                audioSourceBGM.clip = audioClipBGM[TITLE];
                break;
            case "Menu":
                audioSourceBGM.clip = audioClipBGM[MENU];
                break;
            case "Home":
                audioSourceBGM.clip = audioClipBGM[HOME];
                break;
            case "Walking":
                audioSourceBGM.clip = audioClipBGM[WALKING];
                break;
            case "EndRoll":
                audioSourceBGM.clip = audioClipBGM[ENDROLL];
                break;
        }

        // 再生
        audioSourceBGM.Play();
    }

    public void PlaySE(int index, bool stop = false)
    {
        // BGMを止める場合
        if (stop)
        {
            audioSourceBGM.Stop();
        }
        // SEを再生
        audioSourceSE.PlayOneShot(audioClipSE[index]);
    }
}
