using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

#region 诅咒概览
//诅咒概览
//一、按钮缩小 smallSize
//字体及图像大小缩小为原来的百分之一

//二、更多干扰选项 moreOptions
//出现四、六个多余的干扰选项

//三、黑暗 darkness
//全屏黑暗，只有鼠标周围有小范围光亮

//四、翻牌 flip——废弃
//题目和选项翻转，需要点击翻牌，限时时间查看

//五、题目限时显示 timedDisplay

//六、题目和按钮位置交换 placeExchange

//七、显示错误 error
//背景缺失
//文本镜像
//文本上下排列

//八、原地绕中心转动 rotate

//九、选项一起逆时针、顺时针移动 move

//十、随机位置出现 randomPlace
#endregion

public class Option : MonoBehaviour
{
    //组件
    public GameObject background;
    public Image backgroundImage;
    public TextMeshProUGUI text;
    public Image bkImage;
    private Color bkImageColor;

    public GameObject objectSpawn;
    public GameObject question;
    public GameObject extraOptions;
    //public GameObject theNextOption;

    public GameObject bingo;
    public GameObject wrong;

    public event Action HaveClicked;
    public event Action DestroyHeart;

    private string curse;
    private int isCurseUnlocked;

    private float timer;
    private float speedRotation;
    private bool isRotated;

    //诅咒相关
    private Tween theTween;                 //动画对象
    private Tween rotateTween;              //旋转动画
    private Tween moveTween;                //移动动画
    private Tween fadeTween;                //消失动画
    public Vector3 initialScale;            //初始大小系数
    public float smallSize;                 //缩小倍数
    public float rotateTime;                //旋转一圈时间
    public float rotateDirection;           //旋转方向
    public Quaternion initialRotation;      //初始旋转系数
    public Vector3 initialPosition;         //初始坐标
    public float fadeDuration;              //消失时间
    public Vector3[] currentScale = {       //移动方向
        new Vector3(), new Vector3(),
        new Vector3(), new Vector3()
    };
    public float speedX, speedY;
    public float screenLeftX, screenRightX, //窗口左上角、右下角坐标
        screenBottomY, screenTopY;
    public float moveLeftX, moveRightX,     //移动诅咒中左上角、右下角坐标
        moveBottomY, moveTopY;
    private enum theDirection
    {
        up, down, left, right
    };
    private theDirection direction;
    public float moveTimer;
    //private Vector3 theNextPosition;
    private float moveDuration;
    //public float 


    private void Awake()
    {
        //让objectspawn的set事件订阅HaveCliked
        ObjectSpawn obtSp = FindObjectOfType<ObjectSpawn>();
        if (obtSp != null)
        {
            HaveClicked += obtSp.SetOptionQuestionText;
            obtSp.KillFadeTween += KillFadeTween;
        }
        else Debug.LogError("无法找到ObjectSpawn");

        bkImage = gameObject.GetComponent<Image>();
        bkImageColor = bkImage.color;

        initialScale = transform.localScale;
        initialRotation = transform.localRotation;
        initialPosition = transform.position;

        rotateTime = 1f;
        smallSize = 0.45f;
        fadeDuration = 3.8f;

        RectTransform rectTransform = background.transform.GetComponent<RectTransform>();
        Vector3[] corners=new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        screenLeftX = corners[1].x; screenRightX = corners[3].x;
        screenTopY = corners[1].y; screenBottomY = corners[3].y;

        moveLeftX = 183.89f;
        moveRightX = 1374.23f;
        moveTopY = -84.375f;
        moveBottomY = -293.125f;

        //speedX = (580.67f - 183.89f) / 1.5f;
        //speedY = (-84.375f - (-293.125f)) / 1.5f;
        speedX = 0.000001f;
        speedY = 0.000001f;

        //theNextPosition=theNextOption.transform.position;
        moveDuration = 1.5f;

        curse = "null";

        SetMoveDirection();
    }

    private void OnEnable()
    {
        DataManager.Instance.isCurseOperated = true;
        moveTimer = 0;
        wrong.SetActive(false);
        bingo.SetActive(false);
        if (DataManager.Instance.isCurseUnlocked == 1) RecoverCurse();
    }

    private void Update()
    {
        if(DataManager.Instance.isCurseUnlocked == 1 && DataManager.Instance.score >= 5)
        {
            if (curse != DataManager.Instance.curse)
            {
                RecoverCurse();
                curse = DataManager.Instance.curse;
            }
            if (DataManager.Instance.curse == "optionMove" && moveTimer >= 0.1f) DoMoveCurse();
            else if(!DataManager.Instance.isCurseOperated)OperateCurse();
        }
    }

    private void OnDisable()
    {
        if (DataManager.Instance.isCurseUnlocked == 1 && DataManager.Instance.score >= 5) RecoverCurse();
    }


    public void OnClick()
    {
        CheckAnswer();
        if (DataManager.Instance.curse == "moreOptions" && DataManager.Instance.score >= 5 &&
            DataManager.Instance.score % 5 == 0 && extraOptions.activeSelf)
        {
            extraOptions.SetActive(false);
        }
        HaveClicked?.Invoke();
        if(DataManager.Instance.isCurseUnlocked==1 && DataManager.Instance.curse == "optionTimedDisplay")
        {
            if(theTween!=null)theTween.Kill();
        }
    }

    bool CheckRight()
    {
        if (text.text[0] == DataManager.Instance.answer[0]) return true;
        else return false;
    }

    void CheckAnswer()
    {
        if (CheckRight())
        {
            wrong.SetActive(false);
            bingo.SetActive(true);
            bingo.GetComponent<WrongBingo>().PlayeSound();
            DataManager.Instance.score++;
            if (DataManager.Instance.mode == "survival")
            {
                //饱食度加三（惩罚时间减三）
                DataManager.Instance.penaltyTime -= 3f;
            }

        }
        else if (DataManager.Instance.mode == "limit")
        {
            //惩罚时间加三
            bingo.SetActive(false);
            wrong.SetActive(true);
            wrong.GetComponent<WrongBingo>().PlayeSound();
            DataManager.Instance.penaltyTime += 3f;
        }
        else if (DataManager.Instance.mode == "survival" || DataManager.Instance.mode == "hell")
        {
            bingo.SetActive(false);
            wrong.SetActive(true);
            wrong.GetComponent<WrongBingo>().PlayeSound();
            DestroyHeart?.Invoke();
        }
        else if (DataManager.Instance.mode == "race")
        {
            bingo.SetActive(false);
            wrong.SetActive(true);
            wrong.GetComponent<WrongBingo>().PlayeSound();
        }
    }

    void RecoverCurse()
    {
        print("修复选项上一次的诅咒");
        switch (curse)
        {
            case "optionMove":
                transform.position=initialPosition;
                break;
            case "smallSize":
                transform.localScale = initialScale;
                break;
            case "optionTimedDisplay":
                CanvasGroup canvasGroup = transform.Find("Text").GetComponent<CanvasGroup>();
                fadeTween.Kill();
                canvasGroup.alpha = 1.0f;
                break;
            case "localRotate":
                rotateTween.Kill();
                transform.rotation = initialRotation;
                break;
            case "errorDisplay":
                text.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                bkImageColor.a = 1f;
                bkImage.color = bkImageColor;
                break;
            case "randomPosition":
                transform.position = initialPosition;
                break;
        }
    }

    void OperateCurse()
    {
        CanvasGroup canvasGroup = transform.Find("Text").GetComponent<CanvasGroup>();
        if(canvasGroup.alpha != 1.0f) canvasGroup.alpha = 1.0f;
        if (DataManager.Instance.curse == "localRotate" && rotateTween != null && rotateTween.IsPlaying()) rotateTween.Kill();
        switch (DataManager.Instance.curse) {
            case "smallSize":
                DataManager.Instance.theOperatedAmount++;
                DoSmallSizeCurse();
                break;
            case "optionTimedDisplay":
                DoTimedDisplay();
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
            case "randomPosition":
                DoRandomPositionCurse();
                DataManager.Instance.theOperatedAmount++;
                break;
        }
    }

    void DoSmallSizeCurse()
    {
        transform.localScale = initialScale * smallSize;
    }

    void RecoverInitialSize()
    {
        transform.localScale = initialScale;
    }

    void DoTimedDisplay()
    {
        if (fadeTween != null) fadeTween.Kill();
        CanvasGroup canvasGroup = transform.Find("Text").GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            
            canvasGroup.alpha = 1.0f;
            fadeTween = canvasGroup.DOFade(0f, fadeDuration);
        }
        //if(transform.name== "Option (1)")
        //{
        //    DataManager.Instance.isCurseOperated = true;
        //}
    }

    void KillFadeTween()
    {
        if (fadeTween != null)
        {
            fadeTween.Kill();
        }
    }

    void DoLocalRotateCurse()
    {
        int direction;
        while ((direction = UnityEngine.Random.Range(-1, 2)) == 0) ;
        rotateTween = transform.DORotate(new Vector3(0, 0, 360f * direction), 1f, RotateMode.LocalAxisAdd).
            SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
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

    void DoRandomPositionCurse()
    {
        transform.position = new Vector3(UnityEngine.Random.Range(screenLeftX, screenRightX),
            UnityEngine.Random.Range(screenBottomY, screenTopY));
        Debug.Log(transform.name + "随机坐标为" + transform.position);
    }

    //设置移动方向
    void SetMoveDirection()
    {
        if (transform.position.y == moveTopY)
        {
            if (transform.position.x < moveRightX)
            {
                direction = theDirection.right;
            }
            else direction = theDirection.down;
        }
        else
        {
            if (transform.position.x > moveLeftX)
            {
                direction = theDirection.left;
            }
            else direction = theDirection.up;
        }
    }

    void DoMoveCurse()
    {
        

        //this.transform.DOMove(theNextPosition,moveDuration);


        //moveTimer = 0;
        //switch (direction)
        //{
        //    case theDirection.left:
        //        transform.position -= new Vector3(speedX, 0f, 0f);
        //        if (transform.position.x <= moveLeftX)
        //        {
        //            transform.position = new Vector3(moveLeftX, transform.position.y);
        //            direction = theDirection.up;
        //        }
        //        break;
        //    case theDirection.right:
        //        transform.position += new Vector3(speedX, 0f, 0f);
        //        if (transform.position.x >= moveRightX)
        //        {
        //            transform.position = new Vector3(moveRightX, transform.position.y);
        //            direction = theDirection.down;
        //        }
        //        break;
        //    case theDirection.up:
        //        transform.position += new Vector3(0f, speedY);
        //        if (transform.position.y >= moveTopY)
        //        {
        //            transform.position = new Vector3(transform.position.x, moveTopY);
        //            direction = theDirection.right;
        //        }
        //        break;
        //    case theDirection.down:
        //        transform.position += new Vector3(0f, speedY);
        //        if (transform.position.y <= moveBottomY)
        //        {
        //            transform.position = new Vector3(transform.position.x, moveBottomY);
        //            direction = theDirection.left;
        //        }
        //        break;

        //}
    }
}