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
            case "race": text.text = "����ģʽ-" + DataManager.Instance.finalDifficultyTime + "��"; break;
            case "limit": text.text = "����ģʽ-" + DataManager.Instance.finalDifficultyTime + "��"; break;
            case "survival": text.text = "����ģʽ-" + DataManager.Instance.finalDifficultyHp + "����ֵ"; break;
            case "hell": text.text = "����ģʽ-" + DataManager.Instance.finalDifficultyTime + "��-" + DataManager.Instance.finalDifficultyHp + "��"; break;
            default: Debug.LogError("DataManager.Instance.finalMode����" + DataManager.Instance.finalMode); break;
        }

        if (DataManager.Instance.isCurseUnlocked == 1)
        {
            text.text += "\n����ģʽ������";
        }
    }
}
