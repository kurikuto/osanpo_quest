using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// エンドロール
public class EndRollManager : MonoBehaviour
{
    // シーン移動管理
    public SceneTransitionManager sceneTransitionManager;

    [SerializeField] GameObject texts;
    private bool endFlg = false;
    private const int endPosition = 3950;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(texts.transform.position.y >= endPosition && !endFlg)
        {
            endFlg = true;
            StartCoroutine(End());
        } else if(!endFlg)
        {
            texts.transform.position += new Vector3(0, 1, 0);
            //Debug.Log(texts.transform.position.y);
        }
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(5f);
        BackToHome();
    }

    void BackToHome()
    {
        // 画面遷移
        sceneTransitionManager.LoadTo("Menu");
    }

}
