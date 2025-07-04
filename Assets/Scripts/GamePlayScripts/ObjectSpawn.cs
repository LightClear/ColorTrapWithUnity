using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.UI;

#region �������
//�������
//һ����ť��С smallSize
//���弰ͼ���С��СΪԭ���İٷ�֮һ

//�����������ѡ�� moreOptions
//�����ġ���������ĸ���ѡ��

//�����ڰ� darkness
//ȫ���ڰ���ֻ�������Χ��С��Χ����

//�ġ����� flip��������
//��Ŀ��ѡ�ת����Ҫ������ƣ���ʱʱ��鿴

//�塢��Ŀ��ʱ��ʾ timedDisplay

//������Ŀ�Ͱ�ťλ�ý��� placeExchange

//�ߡ���ʾ���� error
//����ȱʧ
//�ı�����
//�ı���������

//�ˡ�ԭ��������ת�� rotate

//�š�ѡ��һ����ʱ�롢˳ʱ���ƶ� move

//ʮ�����λ�ó��� randomPlace
#endregion

public class ObjectSpawn : MonoBehaviour
{
    //private float WINDOWSWIDTH, WINDOWSHEIGHT;                                                      //���ڿ��
    //private float LEFTX, LEFTY;                                                                     //���������Ͻ�����
    //private float questionX, questionY, questionZ, questionWidth, questonHeight, questionFontSize;  //�����������
    //private float initialQuestionFontSize = 150f;                                                   //��ʼ�����ı������С
    ////private double questionTextX, questionTextY, questionTextZ;                                      //�����ı����ꡪ����ʱ����Ҫ������question��XYZ
    //private float optionX, optionY, optionZ, optionWidth, optionHeight, optionFontSize;             //ѡ���������
    //private float gapXNormal, gapYNormal, gapXCurse, gapYCurse;                                     //ѡ����
    //private float speedX, speedY, speedZ;                                                           //ѡ���ٶ�
    //private int amountOption;                                                                       //ѡ������

    //private string nowCurse;                                                                         //��ǰ����
    //private int curseAmount = 10;                                                                    //��������
    //private string[] curses = { "smallSize", "moreOptions", "darkness", "flip", "timedDisplay",      //��������
    //    "placeExchange", "bkImageMiss", "rotate", "move", "randomPlace" };
    

    //public GameObject questionPrefab;
    //public GameObject optionPrefab;

    //private GameObject theQuestion;
    //private GameObject[] theOption = new GameObject[20];
    
    //public Transform parentObject;



    public List<Button> optionList;           //ѡ���б�
    public TextMeshProUGUI questionText;          //��Ŀ
    public GameObject extraOptionGroup;           //�������option
    public List<Button> extraOptionList;      //����ѡ���б�

    private int extraOptionCurseCounter;

    public GameObject question;

    private TextMeshProUGUI theAnswerOption;

    //��ֵ��
    private List<string> textList = new List<string> { "��ɫ", "��ɫ", "��ɫ", "��ɫ", "��ɫ", "��ɫ", "��ɫ", "��ɫ" };
    private List<Color> colorList = new List<Color> {
        new Color(0, 0, 0, 1f), new Color(254f / 255f, 250f / 255f, 82f/255f, 1f), new Color(167 / 255f,0,0, 1f),
        new Color(0, 169 / 255f, 0, 1f), new Color(0, 0, 165 / 255f, 1f), new Color(173 / 255f, 0, 165 / 255f, 1f),
        new Color(173 / 255f, 85 / 255f, 0, 1f), new Color(0, 167 / 255f, 165 / 255f, 1f)
    };

    private Dictionary<Color, string> dictionary=new Dictionary<Color, string>();
    private int isCurseUnlocked;
    private string answerColor;
    public event Action KillFadeTween;

    private void Awake()
    {
        extraOptionCurseCounter = 0;
        //��ʼ��dictionary
        InitializeDictionary();
    }

    private void OnEnable()
    {
        Debug.Log("the count of optionList "+optionList.Count);
        SetOptionQuestionText();
        

    }

    //��ʼ��dictionary
    void InitializeDictionary()
    {
        Debug.Log("��ʼ��ʼ���ı�����ɫdictionary");
        for (int i = 0; i < textList.Count; i++)
        {
            dictionary[colorList[i]] = textList[i];
            Debug.Log(colorList[i]);
        }
    }

    //����ѡ���Լ�������ı�����
    public void SetOptionQuestionText()
    {
        Debug.Log("��ʼ����ѡ���Լ�������ı�����");
        TextMeshProUGUI tmpText;
        System.Random range = new System.Random();
        int num = 8;

        //����������ı��Լ���ɫ
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

        //����question
        if (questionText != null)
        {

            questionText.text = textList[UnityEngine.Random.Range(0, textList.Count)];
            //��ɫ���ı����ܶ�Ӧ
            while (dictionary[questionText.color = colorList[UnityEngine.Random.Range(0, colorList.Count)]] == questionText.text) ;

            if (dictionary[questionText.color] == null) Debug.LogError("can't find the key of " + questionText.color);
            if (DataManager.Instance.answer == null) Debug.LogError("can't find " + "DataManager.Instance.answer");
            DataManager.Instance.answer = dictionary[questionText.color];

            answerColor = dictionary[questionText.color];
            Debug.Log("answerColor is " + answerColor);
        }
        else Debug.LogError("�޷��ҵ�question��Text���");

        //��ֵOption
        for (int i = 0; i < optionList.Count; i++)
        {
            if (optionList[i] == null) Debug.LogError("optionList is missing");
            tmpText = optionList[i].transform.Find("Text").GetComponent<TextMeshProUGUI>();

            if (tmpText == null)
            {
                Debug.LogError("�޷��ҵ�option��text���");
            }
            tmpText.text = textList[i];
            tmpText.color = colorList[i];
            if (tmpText.text == answerColor)
            {
                theAnswerOption = tmpText;
            }
        }

        if (DataManager.Instance.curse == "moreOptions" && extraOptionCurseCounter < 5) SetExtraOptionText();
        else extraOptionCurseCounter = 0;
        if (DataManager.Instance.isCurseUnlocked ==1 && DataManager.Instance.isCurseOperated) CheckCurse();
    }
    
    //���ö���ѡ���ı�
    public void SetExtraOptionText()
    {
        extraOptionCurseCounter++;
        Debug.Log("��ʼ���ö���ѡ���ı�");
        TextMeshProUGUI tmpText;
        for (int i = 0; i < extraOptionList.Count; i++)
        {
            tmpText = extraOptionList[i].GetComponentInChildren<TextMeshProUGUI>();
            while ((tmpText.text = textList[(int)UnityEngine.Random.Range(0, textList.Count)]) == answerColor) ;
            tmpText.color = colorList[UnityEngine.Random.Range(0, colorList.Count)];
        }

        //����ѡ��������һ������ѡ���ֵ����
        if (DataManager.Instance.curse == "moreOptions")
        {
            int rng = UnityEngine.Random.Range(1, 13);
            if (rng <= 4)
            {
                tmpText = extraOptionList[(int)UnityEngine.Random.Range(0, extraOptionList.Count)].GetComponentInChildren<TextMeshProUGUI>();
                Color tmpColor = tmpText.color;
                string tmpTmptext = tmpText.text;
                //print(tmpText.text);
                //print(tmpText.color);
                tmpText.color = theAnswerOption.color;
                tmpText.text = theAnswerOption.text;
                theAnswerOption.color = tmpColor;
                theAnswerOption.text = tmpTmptext;
            }
        }
        
    }

    void CheckCurse()
    {
        switch(DataManager.Instance.curse)
        {
            case "questionTimedDisplay":case "optionTimedDisplay": 
                KillFadeTween?.Invoke();
                DataManager.Instance.isCurseOperated = false;
                break;
            case "errorDisplay": case "randomPosition":
                DataManager.Instance.isCurseOperated=false;
                print("����DataManager.Instance.isCurseOperated��objectSpawn��");
                break;
        }
    }

    //����ɫֵת��Ϊ�ַ���
    //string ColorToString(Color color)
    //{
    //    // ��RGBֵת��Ϊ0��255֮�������
    //    int r = Mathf.RoundToInt(color.r * 255);
    //    int g = Mathf.RoundToInt(color.g * 255);
    //    int b = Mathf.RoundToInt(color.b * 255);

    //    // �����ַ�����ʽ
    //    return $"R:{r}, G:{g}, B:{b}";
    //}

    //private void Start()
    //{
    //    //����
    //    WINDOWSWIDTH = 1920.0f;WINDOWSHEIGHT = 973.91f;
    //    LEFTX = -3.0518e-05f - WINDOWSWIDTH / 2;LEFTY = 0.0020752f + WINDOWSHEIGHT / 2;
    //}

    ////��Ϸ�������Ͻǵ���������: (18.68, -26.20, -0.60)
    //private void OnEnable()
    //{
    //    //����ʱ��ʼ��������
    //    setQuestionSize(initialQuestionFontSize);
    //}


    //void Update()
    //{

    //}

    ////�������������������
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
    //    //��������
    //    theQuestion = Instantiate(questionPrefab, new Vector3(questionX, questionY, questionZ), Quaternion.identity, parentObject);

    //    //����ѡ��
    //    for (int i = 0; i <= amountOption; i++)
    //    {
    //        theOption[i] = Instantiate(optionPrefab, new Vector3(optionX, optionY, optionZ), Quaternion.identity, parentObject);
    //    }
    //}
}
