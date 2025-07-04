using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public int score;                               //����

    public int isCurseUnlocked;                     //�Ƿ�����������
    public string curse;                            //��ǰ������ʲô
    public string preCurse;                         //��һ������
    public bool isCurseOperated;                    //���������Ƿ��Ѳ���
    public bool isCurseUpdated;                     //�����Ƿ������
    public int theOperatedAmount;                   //����ִ�еĴ���

    public string answer;                           //��
    public string mode;                             //��ǰģʽ

    public int difficultyTime;                      //ʱ���Ѷ�
    public int difficultyHp;                        //����ֵ�Ѷ�
    public float penaltyTime;                       //�ͷ�ʱ��

    public string BGM;                              //��ǰ��������

    public string preRuleButton;                    //ǰһ�����õĹ���ť

    public bool haveUpdatedMode;                    //�Ƿ��Ѿ������˹���

    public bool isDead;                             //�Ƿ��Ѿ�����
    public string finalMode;                        //����ʱ����Ϸģʽ
    public int finalDifficultyHp;                   //����ʱ������ֵ�Ѷ�
    public int finalDifficultyTime;                 //����ʱ��ʱ���Ѷ�

    public float BGMVolume;                         //bgm����
    public float SFXVolume;                         //��Ч����
    public string playingBGM;                       //���ڲ��ŵ�BGM
    public string playingBGMPage;                   //���ڲ��ŵ�BGM��ҳ��
    public string nowMusicPage;                     //������ʾ������ҳ��

    public bool isPropUsed;                         //�����Ƿ�ʹ��
    public string theUsedPropName;                  //ʹ�õĵ�������
    public int suspendPropAmount;                   //ʱͣ��������
    public int goldApplePropAmount;                 //��ƻ����������
    public int hamburgerPropAmount;                 //������������
    public int totemPropAmount;                     //����ͼ�ڵ�������

    public bool isLocalDeploy = false;              //�Ƿ�Ϊ���ز���Ӧ��
    public GameObject initializeDatas;              //��ʼ������

    public string theModeSceneFrom;                 //���ĸ��������ģʽѡ�����

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("DataManager ʵ�����ɹ�");
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
