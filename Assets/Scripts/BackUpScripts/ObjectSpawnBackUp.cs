using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

#region 诅咒概览
//诅咒概览
//一、按钮缩小 smallSize
//字体及图像大小缩小为原来的百分之一

//二、更多干扰选项 moreOptions
//出现四、六个多余的干扰选项

//三、黑暗 darkness
//全屏黑暗，只有鼠标周围有小范围光亮

//四、翻牌 flip
//题目和选项翻转，需要点击翻牌，限时时间查看

//五、题目限时显示 timedDisplay

//六、题目和按钮位置交换 placeExchange

//七、选项背景消失 bkImageMiss

//八、原地绕中心转动 rotate

//九、选项一起逆时针、顺时针移动 move

//十、随机位置出现 randomPlace
#endregion

public class ObjectSpawnBackUp : MonoBehaviour
{
    //private float WINDOWSWIDTH, WINDOWSHEIGHT;                                                      //窗口宽高
    //private float LEFTX, LEFTY;                                                                     //窗口最左上角坐标
    //private float questionX, questionY, questionZ, questionWidth, questonHeight, questionFontSize;  //问题组件数据
    //private float initialQuestionFontSize = 150f;                                                   //初始问题文本字体大小
    ////private double questionTextX, questionTextY, questionTextZ;                                      //问题文本坐标——暂时不需要，就用question的XYZ
    //private float optionX, optionY, optionZ, optionWidth, optionHeight, optionFontSize;             //选项组件数据
    //private float gapXNormal, gapYNormal, gapXCurse, gapYCurse;                                     //选项间距
    //private float speedX, speedY, speedZ;                                                           //选项速度
    //private int amountOption;                                                                       //选项数量

    //private string nowCurse;                                                                         //当前诅咒
    //private int curseAmount = 10;                                                                    //诅咒数量
    //private string[] curses = { "smallSize", "moreOptions", "darkness", "flip", "timedDisplay",      //诅咒名称
    //    "placeExchange", "bkImageMiss", "rotate", "move", "randomPlace" };
    

    //public GameObject questionPrefab;
    //public GameObject optionPrefab;

    //private GameObject theQuestion;
    //private GameObject[] theOption = new GameObject[20];
    
    //public Transform parentObject;

    public List<GameObject> optionList;     //选项列表
    public GameObject question;             //题目
    public GameObject extraOptionGroup;          //诅咒更多option
    public List<GameObject> extraOptionList;//更多选项列表

    //赋值用
    private List<string> textList = new List<string> { "黑色", "黄色", "红色", "绿色", "蓝色", "紫色", "棕色", "青色" };
    private List<Color> colorList = new List<Color> {
        new Color(0, 0, 0), new Color(254f / 255f, 250f / 255f, 82f/255f), new Color(167 / 255f,0,0),
        new Color(0, 169 / 255f, 0), new Color(0, 0, 165 / 255f), new Color(173 / 255f, 0, 165 / 255f),
        new Color(173 / 255f, 85 / 255f, 0), new Color(0, 167 / 255f, 165 / 255f)
    };

    private Dictionary<Color, string> dictionary;
    private int isCurseUnlocked;
    private string answerColor;

    private void Start()
    {
        //初始化dictionary
        InitializeDictionary();
    }

    private void OnEnable()
    {
        SetOptionQuestionText();
        isCurseUnlocked = PlayerPrefs.GetInt("isCurseUnlocked", 0);
        //是否为更多选项诅咒
        if (isCurseUnlocked == 1 && PlayerPrefs.GetString("curse","null")=="moreOptions")
        {
            extraOptionGroup.SetActive(true);
            SetExtraOptionText();
        }
        else
        {
            extraOptionGroup.SetActive(false);
        }
    }

    private void Update()
    {
        
    }

    //初始化dictionary
    void InitializeDictionary()
    {
        Debug.Log("开始初始化文本与颜色dictionary");
        for (int i = 0; i < textList.Count; i++)
        {
            dictionary[colorList[i]] = textList[i];
        }
    }

    //设置选项以及问题的文本内容
    void SetOptionQuestionText()
    {
        Debug.Log("开始设置选项以及问题的文本内容");
        TextMeshProUGUI tmpText;
        System.Random range = new System.Random();
        int num = 8;

        //先随机排列文本以及颜色
        while (num > 1)
        {
            int k=range.Next(num--);
            string tmp= textList[num];
            textList[num]= textList[k];
            textList[k]=tmp;
        }
        num = 8;
        while (num > 1)
        {
            int k = range.Next(num--);
            Color tmp = colorList[num];
            colorList[num] = colorList[k];
            colorList[k] = tmp;
        }

        //赋值
        for (int i = 0; i < optionList.Count; i++)
        {
            tmpText = optionList[i].GetComponent<TextMeshProUGUI>();
            tmpText.text = textList[i];
            tmpText.color = colorList[i];
        }

        //设置question
        tmpText = question.GetComponent<TextMeshProUGUI>();
        if (tmpText != null)
        {
            tmpText.text = textList[Random.Range(0, textList.Count)];
            tmpText.color = colorList[Random.Range(0, colorList.Count)];
            PlayerPrefs.SetString("answer", dictionary[tmpText.color]);

            answerColor = dictionary[tmpText.color];
        }
        else Debug.LogError("无法找到question的Text组件");
    }
    
    //设置额外选项文本
    void SetExtraOptionText()
    {
        Debug.Log("开始设置额外选项文本");
        TextMeshProUGUI tmpText;
        for (int i = 0; i < extraOptionList.Count; i++)
        {
            tmpText = extraOptionList[i].GetComponentInParent<TextMeshProUGUI>();
            while ((tmpText.text = textList[Random.Range(0, colorList.Count)]) != answerColor) ;
            tmpText.color = colorList[Random.Range(0, colorList.Count)];
        }
    }

    //将颜色值转换为字符串
    string ColorToString(Color color)
    {
        // 将RGB值转换为0到255之间的整数
        int r = Mathf.RoundToInt(color.r * 255);
        int g = Mathf.RoundToInt(color.g * 255);
        int b = Mathf.RoundToInt(color.b * 255);

        // 返回字符串格式
        return $"R:{r}, G:{g}, B:{b}";
    }

    //private void Start()
    //{
    //    //常量
    //    WINDOWSWIDTH = 1920.0f;WINDOWSHEIGHT = 973.91f;
    //    LEFTX = -3.0518e-05f - WINDOWSWIDTH / 2;LEFTY = 0.0020752f + WINDOWSHEIGHT / 2;
    //}

    ////游戏窗口左上角的世界坐标: (18.68, -26.20, -0.60)
    //private void OnEnable()
    //{
    //    //唤醒时初始化各数据
    //    setQuestionSize(initialQuestionFontSize);
    //}


    //void Update()
    //{

    //}

    ////设置问题相关数据内容
    //private void setQuestionSize(float fontSize)
    //{
    //    questionWidth = 650f; questonHeight = 320;
    //    questionX = (WINDOWSWIDTH - questionWidth) / 2 + LEFTX; questionY = 260f; questionZ = 0;
    //    questionFontSize = fontSize;
    //    //questionTextX= 
    //}

    ////
    //private void setOptionSize()
    //{

    //}

    //private void spawnObjects()
    //{

    //}


    //private void spawnObjects()
    //{
    //    //生成问题
    //    theQuestion = Instantiate(questionPrefab, new Vector3(questionX, questionY, questionZ), Quaternion.identity, parentObject);

    //    //生成选项
    //    for (int i = 0; i <= amountOption; i++)
    //    {
    //        theOption[i] = Instantiate(optionPrefab, new Vector3(optionX, optionY, optionZ), Quaternion.identity, parentObject);
    //    }
    //}
}
