using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public int score;                               //分数

    public int isCurseUnlocked;                     //是否启用了诅咒
    public string curse;                            //当前诅咒是什么
    public string preCurse;                         //上一个诅咒
    public bool isCurseOperated;                    //诅咒内容是否已操作
    public bool isCurseUpdated;                     //诅咒是否更新了
    public int theOperatedAmount;                   //诅咒执行的次数

    public string answer;                           //答案
    public string mode;                             //当前模式

    public int difficultyTime;                      //时间难度
    public int difficultyHp;                        //生命值难度
    public float penaltyTime;                       //惩罚时间

    public string BGM;                              //当前背景音乐

    public string preRuleButton;                    //前一个启用的规则按钮

    public bool haveUpdatedMode;                    //是否已经更新了规则

    public bool isDead;                             //是否已经死亡
    public string finalMode;                        //死亡时的游戏模式
    public int finalDifficultyHp;                   //死亡时的生命值难度
    public int finalDifficultyTime;                 //死亡时的时间难度

    public float BGMVolume;                         //bgm音量
    public float SFXVolume;                         //音效音量
    public string playingBGM;                       //正在播放的BGM
    public string playingBGMPage;                   //正在播放的BGM的页码
    public string nowMusicPage;                     //正在显示的音乐页码

    public bool isPropUsed;                         //道具是否使用
    public string theUsedPropName;                  //使用的道具名称
    public int suspendPropAmount;                   //时停道具数量
    public int goldApplePropAmount;                 //金苹果道具数量
    public int hamburgerPropAmount;                 //汉堡道具数量
    public int totemPropAmount;                     //不死图腾道具数量

    public bool isLocalDeploy = false;              //是否为本地部署应用
    public GameObject initializeDatas;              //初始化数据

    public string theModeSceneFrom;                 //从哪个界面进入模式选择界面

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("DataManager 实例化成功");
        }
        else
        {
            Destroy(Instance);
        }

        isLocalDeploy = false;

        isPropUsed = false;
        theUsedPropName = null;

        isCurseUpdated = false;
        preCurse = null;
        theOperatedAmount = 0;

        nowMusicPage = "PageOne";

        if (isLocalDeploy)
        {

            score = 0;

            isCurseUnlocked = PlayerPrefs.GetInt("isCurseUnlocked");
            curse = "null";
            isCurseOperated = true;

            mode = PlayerPrefs.GetString("mode");

            difficultyTime = PlayerPrefs.GetInt("difficultyTime");
            difficultyHp = PlayerPrefs.GetInt("difficultyHp");
            penaltyTime = 0;

            BGM = PlayerPrefs.GetString("BGM");

            preRuleButton = "MainRuleButton";

            haveUpdatedMode = false;

            isDead = false;

            BGMVolume = PlayerPrefs.GetFloat("BGMVolume");
            SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
            playingBGM = PlayerPrefs.GetString("playingBGM");
            playingBGMPage = PlayerPrefs.GetString("playingBGMPage");

            suspendPropAmount = PlayerPrefs.GetInt("suspendPropAmount");
            goldApplePropAmount = PlayerPrefs.GetInt("goldApplePropAmount");
            hamburgerPropAmount = PlayerPrefs.GetInt("hamburgerPropAmount");
            totemPropAmount = PlayerPrefs.GetInt("totemPropAmount");

            theModeSceneFrom = "InitialMenu";
        }
        else
        {

            score = 0;

            isCurseUnlocked = 0;
            curse = "null";
            isCurseOperated = false;

            mode = "hell";

            difficultyTime = 8;
            difficultyHp = 5;
            penaltyTime = 0;

            BGM = "WaterBubble";

            preRuleButton = "MainRuleButton";

            haveUpdatedMode = false;

            isDead = false;

            BGMVolume = 1;
            SFXVolume = 1;
            playingBGM = "WaterBubble";
            playingBGMPage = "PageOne";

            suspendPropAmount = 30;
            goldApplePropAmount = 30;
            hamburgerPropAmount = 30;
            totemPropAmount = 30;

            theModeSceneFrom = "InitialMenu";
        }
    }
}
