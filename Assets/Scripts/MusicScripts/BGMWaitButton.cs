using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BGMWaitButton : MonoBehaviour
{
    public GameObject playingButton;
    public GameObject BGMPlayer;
    public GameObject icon;
    public GameObject cards;
    public string thePage;
    public AudioSource thisBgm;

    private void Awake()
    {
        //icon.transform.DORotate(new Vector3(0, 0, 360f), 5f, RotateMode.LocalAxisAdd).
        //    SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        if (transform.parent.name == DataManager.Instance.playingBGM)
        {
            icon.transform.DORotate(new Vector3(0, 0, 360f), 5f, RotateMode.LocalAxisAdd).
            SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
            playingButton.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    public void OnClicked()
    {
        
        AudioSource BGMAudio = BGMPlayer.GetComponent<AudioSource>();
        Transform page = cards.transform.Find(DataManager.Instance.playingBGMPage);
        Transform preBGMPlaying = page.transform.Find(DataManager.Instance.playingBGM);
        print(DataManager.Instance.playingBGM);
        Button preBGMPlayingButton = preBGMPlaying.Find("Playing").GetComponent<Button>();

        if(preBGMPlaying != null)
        {
            preBGMPlayingButton.onClick.Invoke();
        }

        icon.transform.DORotate(new Vector3(0, 0, 360f), 5f, RotateMode.LocalAxisAdd).
            SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);

        DataManager.Instance.playingBGM = transform.parent.name;
        DataManager.Instance.playingBGMPage = thePage;
        BGMAudio.clip = thisBgm.clip;
        BGMAudio.Play();
        playingButton.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
