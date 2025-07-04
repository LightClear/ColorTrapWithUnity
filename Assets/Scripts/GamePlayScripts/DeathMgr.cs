using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMgr : MonoBehaviour
{
    public GameObject gameScene;
    public GameObject clearMenuScene;


    void Update()
    {
        if(DataManager.Instance.isDead)
        {
            DataManager.Instance.isDead = false;
            DataManager.Instance.finalMode = DataManager.Instance.mode;
            DataManager.Instance.finalDifficultyHp = DataManager.Instance.difficultyHp;
            DataManager.Instance.finalDifficultyTime = DataManager.Instance.difficultyTime;
            gameScene.SetActive(false);
            clearMenuScene.SetActive(true);

            UpdatePropAmount(DataManager.Instance.mode, DataManager.Instance.score, DataManager.Instance.finalDifficultyTime, DataManager.Instance.finalDifficultyHp);
        }
    }

    void UpdatePropAmount(string mode, int score, int difficultyTime, int difficultyHp)
    {
        if (mode == "race")
        {
            if (difficultyTime <= 30 && score >= 15)
            {
                DataManager.Instance.suspendPropAmount++;
                if(DataManager.Instance.isLocalDeploy) PlayerPrefs.SetInt("suspendPropAmount", DataManager.Instance.suspendPropAmount);
            }
        }
        else if(mode == "limit")
        {
            if(difficultyTime<=5 && score >= 18)
            {
                DataManager.Instance.suspendPropAmount++;
                if (DataManager.Instance.isLocalDeploy) PlayerPrefs.SetInt("suspendPropAmount", DataManager.Instance.suspendPropAmount);
            }
            if(difficultyTime<=3 && score >= 24)
            {
                DataManager.Instance.totemPropAmount++;
                if (DataManager.Instance.isLocalDeploy) PlayerPrefs.SetInt("totemPropAmount", DataManager.Instance.totemPropAmount);
            }
        }
        else if(mode == "survival")
        {
            if (difficultyHp <= 3 && score >= 20)
            {
                DataManager.Instance.goldApplePropAmount++;
                if (DataManager.Instance.isLocalDeploy) PlayerPrefs.SetInt("goldApplePropAmount", DataManager.Instance.goldApplePropAmount);
            }
            if(difficultyHp <= 1 && score >= 24)
            {
                DataManager.Instance.totemPropAmount++;
                if (DataManager.Instance.isLocalDeploy) PlayerPrefs.SetInt("totemPropAmount", DataManager.Instance.totemPropAmount);
            }
            if(difficultyHp <= 3 && score >= 8)
            {
                DataManager.Instance.hamburgerPropAmount++;
                if (DataManager.Instance.isLocalDeploy) PlayerPrefs.SetInt("hamburgerPropAmount", DataManager.Instance.hamburgerPropAmount);
            }
        }
        else if (mode == "hell")
        {
            if(difficultyHp <= 3 && difficultyTime <= 5 && score >= 15)
            {
                DataManager.Instance.suspendPropAmount++;
                if (DataManager.Instance.isLocalDeploy) PlayerPrefs.SetInt("suspendPropAmount", DataManager.Instance.suspendPropAmount);
            }
            if (difficultyHp <= 3 && difficultyTime <= 5 && score >= 18)
            {
                DataManager.Instance.goldApplePropAmount++;
                if (DataManager.Instance.isLocalDeploy) PlayerPrefs.SetInt("goldApplePropAmount", DataManager.Instance.goldApplePropAmount);
            }
            if (difficultyHp <= 1 && difficultyTime <= 3 && score >= 20)
            {
                DataManager.Instance.totemPropAmount++;
                if (DataManager.Instance.isLocalDeploy) PlayerPrefs.SetInt("totemPropAmount", DataManager.Instance.totemPropAmount);
            }
            if (difficultyHp <= 3 && difficultyTime <= 5 && score >= 8)
            {
                DataManager.Instance.hamburgerPropAmount++;
                if (DataManager.Instance.isLocalDeploy) PlayerPrefs.SetInt("hamburgerPropAmount", DataManager.Instance.hamburgerPropAmount);
            }
        }
    }
}
