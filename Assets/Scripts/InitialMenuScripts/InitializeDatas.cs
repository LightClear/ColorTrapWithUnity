using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeDatas : MonoBehaviour
{
    //数据如下：
    //是否已经初始化了数据：hadInitialized
    //模式：mode——race, survival, limit, race
    //难度：difficulty——difficultyHp，difficultyTime
    //道具数量：goldApple, totem, timeSuspend, hamburger
    //当前诅咒：curse
    //答案：answer

    private void Awake()
    {
        if(DataManager.Instance.isLocalDeploy)gameObject.SetActive(false);

        int hadInitialized = PlayerPrefs.GetInt("hadInitialized", -1);
        if (hadInitialized != 1)
        {
            Debug.Log("未进行过初始化，现在进行初始化");
            //声明已初始化
            PlayerPrefs.SetInt("hadInitialized", 1);
            //初始化模式
            PlayerPrefs.SetString("mode", "race");
            //初始化难度
            PlayerPrefs.SetInt("difficultyHp", 5);
            PlayerPrefs.SetInt("difficultyTime", 60);
            //初始化道具数量
            PlayerPrefs.SetInt("goldApple", 3);
            PlayerPrefs.SetInt("totem", 3);
            PlayerPrefs.SetInt("timesuspend", 3);
            PlayerPrefs.SetInt("hamburger", 3);

            checkInitializedSuccessfully();
        }
        else Debug.Log("已经进行过初始化");
    }

    //检测是否成功初始化
    void checkInitializedSuccessfully()
    {
        if (PlayerPrefs.GetString("mode", "null") != "race") Debug.LogError("模式未能成功初始化");
        if (PlayerPrefs.GetInt("difficultyHp", -1) != 5) Debug.LogError("血量难度未能成功初始化");
        if (PlayerPrefs.GetInt("difficultyTime", -1) != 60) Debug.LogError("时间难度未能成功初始化");
        if (PlayerPrefs.GetInt("goldApple", -1)!=3) Debug.LogError("金苹果道具数量未能成功初始化");
        if (PlayerPrefs.GetInt("totem", -1) != 3) Debug.LogError("不死图腾道具数量未能成功初始化");
        if (PlayerPrefs.GetInt("timesuspend", -1) != 3) Debug.LogError("时停道具数量未能成功初始化");
        if (PlayerPrefs.GetInt("hamburger", -1) != 3) Debug.LogError("汉堡道具数量未能成功初始化");
    }
}
