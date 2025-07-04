using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    //用于切换场景画面的脚本，附着于按钮上，例如返回按钮、初始界面按钮
    public GameObject thisScene;
    public GameObject nextScene;

    public void onClick()
    {
        thisScene.SetActive(false);
        nextScene.SetActive(true);
    }
}
