using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FadeTest : MonoBehaviour
{
    public float fadeDuration;
    public float fadeTimer;
    private Tween theTween;
    public CanvasGroup theCanvasGroup;

    private void Start()
    {
        //CanvasGroup canvasGroup = transform.GetComponent<CanvasGroup>();
        if (theCanvasGroup != null)
        {
            theTween = theCanvasGroup.DOFade(0f, fadeDuration);
        }
    }

    private void Update()
    {
        fadeTimer += Time.deltaTime;
        if (fadeTimer > 5f)
        {
            theTween.Kill();
            theCanvasGroup.alpha = 1f;
        }
    }

    public void OnClicked()
    {
        print("Clicked!");
    }
}
