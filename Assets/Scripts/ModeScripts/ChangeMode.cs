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
        if (DataManager.Instance.isLocalDeploy && PlayerPrefs.GetString("mode") == mode) Debug.Log("�ڴ��гɹ�����ģʽΪ" + mode);
        else Debug.Log("�ɹ�����ģʽΪ" + mode);
    }

    //�������ģʽ��������Ӧ���Ѷ���ֵ
    private void changeDifficulty()
    {
        switch (mode)
        {
            case "race":
                if(DataManager.Instance.difficultyTime < 15)
                {
                    DataManager.Instance.difficultyTime = 60;
                    if (DataManager.Instance.isLocalDeploy) PlayerPrefs.SetInt("difficultyTime", 60);
                    Debug.Log("ʱ���Ѷ�����Ϊ60��");
                }
                break;
            case "limit":
                if (DataManager.Instance.difficultyTime > 8)
                {
                    DataManager.Instance.difficultyTime = 8;
                    if (DataManager.Instance.isLocalDeploy) PlayerPrefs.SetInt("difficultyTime", 8);
                    Debug.Log("ʱ���Ѷ�����Ϊ8��");
                }
                break;
            case "survival":
                if (DataManager.Instance.difficultyHp != 5)
                {
                    DataManager.Instance.difficultyHp = 5;
                    if (DataManager.Instance.isLocalDeploy) PlayerPrefs.SetInt("difficultyHp", 5);
                    Debug.Log("����ֵ�Ѷ�����Ϊ5��");
                }
                break;
            case "hell":
                if (DataManager.Instance.difficultyTime > 8)
                {
                    DataManager.Instance.difficultyTime = 8;
                    if (DataManager.Instance.isLocalDeploy) PlayerPrefs.SetInt("difficultyTime", 8);
                    Debug.Log("ʱ���Ѷ�����Ϊ8��");
                }
                break;
            default:
                Debug.LogError("����ģʽʱģʽ���ݴ���ģʽΪ" + mode);
                break;
        }
    }
}
