using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

#region �������
//�������
//һ����ť��С smallSize
//���弰ͼ���С��СΪԭ���İٷ�֮һ

//�����������ѡ�� moreOptions
//�����ġ���������ĸ���ѡ��

//�����ڰ� darkness
//ȫ���ڰ���ֻ�������Χ��С��Χ����

//�ġ����� flip
//��Ŀ��ѡ�ת����Ҫ������ƣ���ʱʱ��鿴

//�塢��Ŀ��ʱ��ʾ timedDisplay

//������Ŀ�Ͱ�ťλ�ý��� placeExchange

//�ߡ�ѡ�����ʧ bkImageMiss

//�ˡ�ԭ��������ת�� rotate

//�š�ѡ��һ����ʱ�롢˳ʱ���ƶ� move

//ʮ�����λ�ó��� randomPlace
#endregion

public class ObjectSpawnBackUp : MonoBehaviour
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

    public List<GameObject> optionList;     //ѡ���б�
    public GameObject question;             //��Ŀ
    public GameObject extraOptionGroup;          //�������option
    public List<GameObject> extraOptionList;//����ѡ���б�

    //��ֵ��
    private List<string> textList = new List<string> { "��ɫ", "��ɫ", "��ɫ", "��ɫ", "��ɫ", "��ɫ", "��ɫ", "��ɫ" };
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
        //��ʼ��dictionary
        InitializeDictionary();
    }

    private void OnEnable()
    {
        SetOptionQuestionText();
        isCurseUnlocked = PlayerPrefs.GetInt("isCurseUnlocked", 0);
        //�Ƿ�Ϊ����ѡ������
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

    //��ʼ��dictionary
    void InitializeDictionary()
    {
        Debug.Log("��ʼ��ʼ���ı�����ɫdictionary");
        for (int i = 0; i < textList.Count; i++)
        {
            dictionary[colorList[i]] = textList[i];
        }
    }

    //����ѡ���Լ�������ı�����
    void SetOptionQuestionText()
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

        //��ֵ
        for (int i = 0; i < optionList.Count; i++)
        {
            tmpText = optionList[i].GetComponent<TextMeshProUGUI>();
            tmpText.text = textList[i];
            tmpText.color = colorList[i];
        }

        //����question
        tmpText = question.GetComponent<TextMeshProUGUI>();
        if (tmpText != null)
        {
            tmpText.text = textList[Random.Range(0, textList.Count)];
            tmpText.color = colorList[Random.Range(0, colorList.Count)];
            PlayerPrefs.SetString("answer", dictionary[tmpText.color]);

            answerColor = dictionary[tmpText.color];
        }
        else Debug.LogError("�޷��ҵ�question��Text���");
    }
    
    //���ö���ѡ���ı�
    void SetExtraOptionText()
    {
        Debug.Log("��ʼ���ö���ѡ���ı�");
        TextMeshProUGUI tmpText;
        for (int i = 0; i < extraOptionList.Count; i++)
        {
            tmpText = extraOptionList[i].GetComponentInParent<TextMeshProUGUI>();
            while ((tmpText.text = textList[Random.Range(0, colorList.Count)]) != answerColor) ;
            tmpText.color = colorList[Random.Range(0, colorList.Count)];
        }
    }

    //����ɫֵת��Ϊ�ַ���
    string ColorToString(Color color)
    {
        // ��RGBֵת��Ϊ0��255֮�������
        int r = Mathf.RoundToInt(color.r * 255);
        int g = Mathf.RoundToInt(color.g * 255);
        int b = Mathf.RoundToInt(color.b * 255);

        // �����ַ�����ʽ
        return $"R:{r}, G:{g}, B:{b}";
    }

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
