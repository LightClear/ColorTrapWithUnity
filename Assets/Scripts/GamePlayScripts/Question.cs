using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading;
using System.Net.NetworkInformation;
//using UnityEngine.UIElements;

public class Question : MonoBehaviour
{
    //子对象组件
    public GameObject background;
    public Image backgroundImage;
    public TextMeshProUGUI text;
    private Image bkImage;
    private Color bkImageColor;

    public Vector3 initialScale;
    public Vector3 initialPosition;
    public float initialFontSize;
    private float timer;

    public float fadeDuration;
    public CanvasGroup textCanvasGroup;
    public Quaternion initialRotation;
    public Tween rotateTween;
    public Tween fadeTween;

    private string theCurse;

    public float bottomY;
    public float topY;
    public float leftX;
    public float rightX;
    

    private void Awake()
    {
        bkImage = transform.Find("Image").GetComponent<Image>();
        bkImageColor = bkImage.color;

        ObjectSpawn obtSp = FindObjectOfType<ObjectSpawn>();
        if (obtSp != null)
        {
            obtSp.KillFadeTween += KillFadeTween;
        }
        else Debug.LogError("无法找到ObjectSpawn");

        initialRotation = transform.localRotation;
        initialPosition=transform.position;
        initialScale = transform.localScale;
        initialFontSize = text.fontSize;
        timer = 0;

        textCanvasGroup=text.transform.GetComponent<CanvasGroup>();

        RectTransform rectTransform = background.transform.GetComponent<RectTransform>();
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        leftX = corners[1].x; rightX = corners[3].x;
        topY = corners[1].y; bottomY = corners[3].y;
    }

    private void OnEnable()
    {
        transform.localScale = initialScale;
        text.fontSize = initialFontSize;
        timer = 0;
    }

    private void Update()
    {


        //诅咒开启时
        if (DataManager.Instance.isCurseUnlocked == 1 && DataManager.Instance.score >= 5)
        {
            if (theCurse != DataManager.Instance.curse)
            {
                RecoverCurse();
                theCurse = DataManager.Instance.curse;
            }
            if(!DataManager.Instance.isCurseOperated) OperateCurse();
        }
    }

    private void OnDisable()
    {
        if (DataManager.Instance.isCurseUnlocked == 1 && DataManager.Instance.score >= 5) RecoverCurse();
    }

    void RecoverCurse()
    {
        switch (theCurse)
        {
            case "smallSize":
                transform.localScale = initialScale;
                break;
            case "questionTimedDisplay":
                CanvasGroup canvasGroup = transform.Find("Text").GetComponent<CanvasGroup>();
                fadeTween.Kill();
                canvasGroup.alpha = 1.0f;
                break;
            case "localRotate":
                rotateTween.Kill();
                transform.rotation = initialRotation;
                break;
            case "errorDisplay":
                bkImageColor.a = 1f;
                bkImage.color = bkImageColor;
                text.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                break;
            case "randomPosition":
                transform.position = initialPosition;
                break;
        }
    }

    void OperateCurse()
    {
        if (DataManager.Instance.curse == "localRotate" && rotateTween != null && rotateTween.IsPlaying()) rotateTween.Kill();
        //RecoverCurse();
        print("执行question诅咒");
        switch (DataManager.Instance.curse)
        {
            case "smallSize":
                SetScaleCurse(0.3f);
                DataManager.Instance.theOperatedAmount++;
                break;
            case "questionTimedDisplay":
                DoFadeCurse();
                break;
            case "randomPosition":
                DoRandomPositionCurse();
                DataManager.Instance.theOperatedAmount++;
                break;
            case "localRotate":
                DoLocalRotateCurse();
                DataManager.Instance.theOperatedAmount++;
                break;
            case "errorDisplay":
                DoErrorDisplayCurse();
                DataManager.Instance.theOperatedAmount++;
                break;
        }
    }

    void SetScaleCurse(float to)
    {
        transform.localScale = initialScale * to;
        //DataManager.Instance.isCurseOperated = true;
    }

    void DoFadeCurse()
    {
        if (textCanvasGroup != null)
        {
            textCanvasGroup.alpha = 1.0f;
            fadeTween = textCanvasGroup.DOFade(0f, fadeDuration);
            DataManager.Instance.isCurseOperated = true;
        }
    }

    void KillFadeTween()
    {
        if (fadeTween != null)
        {
            fadeTween.Kill();
        }
    }

    void DoRandomPositionCurse()
    {
        transform.position = new Vector3(Random.Range(leftX, rightX), Random.Range(bottomY, topY));
        Debug.Log(transform.name + "随机坐标为" + transform.position);
        //DataManager.Instance.isCurseOperated = true;
    }

    void DoLocalRotateCurse()
    {
        int direction;
        while ((direction = UnityEngine.Random.Range(-1, 2)) == 0) ;
        rotateTween = transform.DORotate(new Vector3(0, 0, 360f * direction), 1f, RotateMode.LocalAxisAdd).
            SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        //DataManager.Instance.isCurseOperated = true;
    }

    void DoErrorDisplayCurse()
    {
        bkImageColor.a = 1f;
        bkImage.color = bkImageColor;
        int rnd = UnityEngine.Random.Range(0, 15);
        switch (rnd)
        {
            case 0:
                ZFlip();
                break;
            case 1:
                YFlip();
                break;
            case 2:
                text.transform.rotation = initialRotation;
                VerticalText();
                break;
            case 3:
                BkImageFade();
                break;

            case 4:
                text.transform.rotation = Quaternion.Euler(0f, 180f, 180f);
                break;
            case 5:
                ZFlip();
                VerticalText();
                break;
            case 6:
                ZFlip();
                BkImageFade();
                break;

            case 7:
                YFlip();
                VerticalText();
                break;
            case 8:
                YFlip();
                BkImageFade();
                break;

            case 9:
                text.transform.rotation = initialRotation;
                VerticalText();
                BkImageFade();
                break;

            case 10:
                text.transform.rotation = Quaternion.Euler(0f, 180f, 180f);
                VerticalText();
                break;
            case 11:
                text.transform.rotation = Quaternion.Euler(0f, 180f, 180f);
                BkImageFade();
                break;
            case 12:
                ZFlip();
                VerticalText();
                BkImageFade();
                break;
            case 13:
                YFlip();
                VerticalText();
                BkImageFade();
                break;

            case 14:
                text.transform.rotation = Quaternion.Euler(0f, 180f, 180f);
                VerticalText();
                BkImageFade();
                break;
        }

        //if (rnd == 0)
        //{
        //    text.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        //}
        //else if (rnd == 1)
        //{
        //    text.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        //}
        //else if (rnd == 2)
        //{
        //    text.transform.rotation = initialRotation;
        //    text.text = text.text[0] + "\n" + text.text[1];
        //}
        //else if (rnd == 3)
        //{
        //    text.transform.rotation = Quaternion.Euler(0f, 180f, 180f);
        //}
        //else if (rnd == 4)
        //{
        //    text.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        //    text.text = text.text[0] + "\n" + text.text[1];
        //}
        //else if(rnd == 5)
        //{
        //    text.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        //    text.text = text.text[0] + "\n" + text.text[1];
        //}
        //else if (rnd == 6)
        //{
        //    text.transform.rotation = Quaternion.Euler(0f, 180f, 180f);
        //    text.text = text.text[0] + "\n" + text.text[1];
        //}
        //else if(rnd == 7)
        //{
        //    bkImageColor.a = 0f;
        //    bkImage.color = bkImageColor;
        //}
        //else if(rnd== 8)
        //{
        //    bkImageColor.a = 0f;
        //    bkImage.color = bkImageColor;
        //    text.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        //}
    }

    void ZFlip()
    {
        text.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
    }

    void YFlip()
    {
        text.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

    void VerticalText()
    {
        text.text = text.text[0] + "\n" + text.text[1];
    }

    void BkImageFade()
    {
        bkImageColor.a = 0f;
        bkImage.color = bkImageColor;
    }

    //void DoErrorDisplayCurse()
    //{
    //    int rnd = Random.Range(0, 6);
    //    if (rnd == 0)
    //    {
    //        backgroundImage.enabled = false;
    //        text.transform.rotation = initialRotation;
    //    }
    //    else if(rnd == 1)
    //    {
    //        backgroundImage.enabled=true;
    //        text.transform.rotation=Quaternion.Euler(0f, 180f, 0f);
    //    }
    //    else if (rnd == 2)
    //    {
    //        text.text = text.text[0] + "\n" + text.text[1];
    //    }
    //    else if (rnd == 3)
    //    {
    //        backgroundImage.enabled = false;
    //        text.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    //    }
    //    else if (rnd == 4)
    //    {
    //        backgroundImage.enabled = false;
    //        text.transform.rotation = initialRotation;
    //        text.text = text.text[0] + "\n" + text.text[1];
    //    }
    //    else
    //    {
    //        text.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    //        text.text = text.text[0] + "\n" + text.text[1];
    //    }
    //    //DataManager.Instance.isCurseOperated = true;
    //}

}
