using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalModeText : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void OnEnable()
    {
        switch (DataManager.Instance.finalMode)
        {
            case "race": text.text = "竞速模式-" + DataManager.Instance.finalDifficultyTime + "秒"; break;
            case "limit": text.text = "单限模式-" + DataManager.Instance.finalDifficultyTime + "秒"; break;
            case "survival": text.text = "生存模式-" + DataManager.Instance.finalDifficultyHp + "生命值"; break;
            case "hell": text.text = "地狱模式-" + DataManager.Instance.finalDifficultyTime + "秒-" + DataManager.Instance.finalDifficultyHp + "命"; break;
            default: Debug.LogError("DataManager.Instance.finalMode错误！" + DataManager.Instance.finalMode); break;
        }

        if (DataManager.Instance.isCurseUnlocked == 1)
        {
            text.text += "\n诅咒模式：开启";
        }
    }
}
