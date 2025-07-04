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
        text.text = "��ǰģʽΪ-";
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
            Debug.Log("��������ʧ��");
            text.text = "ERROR!��ѡ��ģʽ";
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

    //�����ı�����
    private void updateText()
    {
        text.text = "��ǰģʽΪ-";
        switch(mode)
        {
            case"race":
                text.text += "����ģʽ-" + difficultyTime.ToString() + "s";
                break;
            case "limit":
                text.text += "����ģʽ-" + difficultyTime.ToString() + "s";
                break;
            case "survival":
                text.text += "����ģʽ-" + difficultyHp.ToString() + "��";
                break;
            case"hell":
                text.text += "����ģʽ-" + difficultyTime.ToString() + "s-" + difficultyHp.ToString() + "��";
                break;
            default:
                Debug.LogError("����ģʽ�ı�����ʱģʽ���ݴ���ģʽΪ" + mode);
                break;
        }

        text.text += "\n����ģʽ��";
        if (DataManager.Instance.isCurseUnlocked == 1)
        {
            text.text += "����";
        }
        else
        {
            text.text += "�ر�";
        }
    }

    //��������Ƿ������ȷ
    private bool checkDatasRight()
    {
        bool passed = true;
        if (mode== "null")
        {
            Debug.LogError("ģʽ���ݼ��ش���");
            passed = false;
        }
        if (passed && difficultyHp == -1)
        {
            Debug.LogError("Ѫ�����ݼ��ش���");
            passed = false;
        }
        if (passed && difficultyTime == -1)
        {
            Debug.LogError("ʱ�����ݼ��ش���");
            passed = false;
        }
        return passed;
    }
}
