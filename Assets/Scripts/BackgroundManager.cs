using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 背景を管理
public class BackgroundManager : MonoBehaviour
{
    private const int BEGINNING = 0;
    private const int INTERVAL = 1;
    private const int ENDING = 2;

    public GameObject[] backgrounds;

    // ストーリーに合わせた背景をセット
    public void SetImage(string currentStory)
    {
        switch (currentStory)
        {
            default:
            case "beginning":
                backgrounds[BEGINNING].SetActive(true);
                break;
            case "interval":
                backgrounds[INTERVAL].SetActive(true);
                break;
            case "ending":
                backgrounds[ENDING].SetActive(true);
                break;
        }
    }

}