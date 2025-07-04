using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetModeText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private string mode;
    private int difficultyHp;
    private int difficultyTime;

    void Start()
    {
        UnlockCurse unlockCurse = FindObjectOfType<UnlockCurse>();
        if (unlockCurse != null)
        {
            unlockCurse.UpdateText += updateText;
        }

        text= GetComponent<TextMeshProUGUI>();
        text.text = "当前模式为-";
        //mode = PlayerPrefs.GetString("mode", "null");
        //difficultyHp = PlayerPrefs.GetInt("difficultyHp", -1);
        //difficultyTime = PlayerPrefs.GetInt("difficultyTime", -1);
        mode = DataManager.Instance.mode;
        difficultyHp = DataManager.Instance.difficultyHp;
        difficultyTime = DataManager.Instance.difficultyTime;
        if (checkDatasRight())
        {
            updateText();
        }
        else
        {
            Debug.Log("加载数据失败");
            text.text = "ERROR!请选择模式";
        }
    }

    void Update()
    {
        if (DataManager.Instance.haveUpdatedMode)
        {
            mode = DataManager.Instance.mode;
            difficultyTime = DataManager.Instance.difficultyTime;
            difficultyHp = DataManager.Instance.difficultyHp;
            DataManager.Instance.haveUpdatedMode = false;
            updateText();
        }
    }

    //更新文本内容
    private void updateText()
    {
        text.text = "当前模式为-";
        switch(mode)
        {
            case"race":
                text.text += "竞速模式-" + difficultyTime.ToString() + "s";
                break;
            case "limit":
                text.text += "单限模式-" + difficultyTime.ToString() + "s";
                break;
            case "survival":
                text.text += "生存模式-" + difficultyHp.ToString() + "命";
                break;
            case"hell":
                text.text += "地狱模式-" + difficultyTime.ToString() + "s-" + difficultyHp.ToString() + "命";
                break;
            default:
                Debug.LogError("更新模式文本数据时模式数据错误，模式为" + mode);
                break;
        }

        text.text += "\n诅咒模式：";
        if (DataManager.Instance.isCurseUnlocked == 1)
        {
            text.text += "开启";
        }
        else
        {
            text.text += "关闭";
        }
    }

    //检查数据是否加载正确
    private bool checkDatasRight()
    {
        bool passed = true;
        if (mode== "null")
        {
            Debug.LogError("模式数据加载错误");
            passed = false;
        }
        if (passed && difficultyHp == -1)
        {
            Debug.LogError("血量数据加载错误");
            passed = false;
        }
        if (passed && difficultyTime == -1)
        {
            Debug.LogError("时间数据加载错误");
            passed = false;
        }
        return passed;
    }
}
