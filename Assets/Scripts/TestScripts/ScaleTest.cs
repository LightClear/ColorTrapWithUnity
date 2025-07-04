using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTest : MonoBehaviour
{
    //public float size;
    //public float timer;
    //public Vector3 initialScale;

    //private void Awake()
    //{
    //    initialScale = transform.localScale;
    //}

    public float timer;
    public Tween rotateTween;
    public Quaternion initialScale;

    private void Awake()
    {
        initialScale = transform.rotation;
    }

    private void OnEnable()
    {
        timer = 0;
        rotateTween = transform.DORotate(new Vector3(0, 0, 360f), 1f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 7.5f)
        {
            rotateTween.Kill();
            transform.rotation = initialScale;
        }
        //timer += Time.deltaTime;
        //if (timer > 3 && timer < 6)
        //{
        //    transform.localScale = initialScale * size;
        //}
        //else if(timer > 6)
        //{
        //    transform.localScale = initialScale;
        //}

    }

}
