using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDifficulty : MonoBehaviour
{
    public int difficulty;
    public string theName;
    public Button thisButton;

    public void onClick()
    {
        DataManager.Instance.haveUpdatedMode = true;
        Debug.Log("设置模式" + "难度为" + theName + difficulty.ToString());
        if (theName == "difficultyTime")
        {
            DataManager.Instance.difficultyTime = difficulty;
        }
        else
        {
             DataManager.Instance.difficultyHp = difficulty;
        }
        if (DataManager.Instance.isLocalDeploy) PlayerPrefs.SetInt(theName, difficulty);
        thisButton.enabled = true;
        
    }
}
