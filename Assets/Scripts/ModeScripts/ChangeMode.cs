using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class ChangeMode : MonoBehaviour
{
    public GameObject thisScene;
    public GameObject nextScene;
    public string mode;
    private Button thisButton;

    private void Awake()
    {
        thisButton = GetComponent<Button>();
    }

    public void onClick()
    {
        thisButton.enabled = true;
        thisScene.SetActive(false);
        nextScene.SetActive(true);
        if (DataManager.Instance.isLocalDeploy) PlayerPrefs.SetString("mode", mode);
        DataManager.Instance.mode = mode;
        DataManager.Instance.haveUpdatedMode = true;
        changeDifficulty();
        if (DataManager.Instance.isLocalDeploy && PlayerPrefs.GetString("mode") == mode) Debug.Log("内存中成功设置模式为" + mode);
        else Debug.Log("成功设置模式为" + mode);
    }

    //点击更换模式后设置相应的难度数值
    private void changeDifficulty()
    {
        switch (mode)
        {
            case "race":
                if(DataManager.Instance.difficultyTime < 15)
                {
                    DataManager.Instance.difficultyTime = 60;
                    if (DataManager.Instance.isLocalDeploy) PlayerPrefs.SetInt("difficultyTime", 60);
                    Debug.Log("时间难度设置为60秒");
                }
                break;
            case "limit":
                if (DataManager.Instance.difficultyTime > 8)
                {
                    DataManager.Instance.difficultyTime = 8;
                    if (DataManager.Instance.isLocalDeploy) PlayerPrefs.SetInt("difficultyTime", 8);
                    Debug.Log("时间难度设置为8秒");
                }
                break;
            case "survival":
                if (DataManager.Instance.difficultyHp != 5)
                {
                    DataManager.Instance.difficultyHp = 5;
                    if (DataManager.Instance.isLocalDeploy) PlayerPrefs.SetInt("difficultyHp", 5);
                    Debug.Log("生命值难度设置为5命");
                }
                break;
            case "hell":
                if (DataManager.Instance.difficultyTime > 8)
                {
                    DataManager.Instance.difficultyTime = 8;
                    if (DataManager.Instance.isLocalDeploy) PlayerPrefs.SetInt("difficultyTime", 8);
                    Debug.Log("时间难度设置为8秒");
                }
                break;
            default:
                Debug.LogError("更换模式时模式数据错误，模式为" + mode);
                break;
        }
    }
}
