using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBoard : MonoBehaviour
{
    public GameObject race;
    public GameObject limit;
    public GameObject survival;
    public GameObject hell;

    private void OnEnable()
    {
        SetBoard(true);
    }

    private void OnDisable()
    {
        SetBoard(false);
    }

    private void debugText(string text, bool set)
    {
        Debug.Log("设置计分板" + text + "为" + set);
    }

    void SetBoard(bool set)
    {
        switch (DataManager.Instance.mode)
        {
            case "race": race.SetActive(set); debugText(DataManager.Instance.mode, set); break;
            case "limit":limit.SetActive(set); debugText(DataManager.Instance.mode, set); break;
            case "survival":survival.SetActive(set); debugText(DataManager.Instance.mode, set); break;
            case "hell":hell.SetActive(set); debugText(DataManager.Instance.mode, set); break;
            default:Debug.LogError("未找到DataManager.Instance.mode：" + DataManager.Instance.mode);break;
        }
    }
}
