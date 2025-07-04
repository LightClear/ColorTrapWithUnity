using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MusicPageButton : MonoBehaviour
{
    public GameObject thisPage;
    public GameObject cards;
    public GameObject musicScene;

    private void Awake()
    {
        TextMeshProUGUI thisText = transform.GetComponentInChildren<TextMeshProUGUI>();
        if (transform.name=="PageOneButton") thisText.fontSize = 75;
    }

    public void OnClicked()
    {
        if (DataManager.Instance.nowMusicPage == thisPage.name) return;

        GameObject prePage = cards.transform.Find(DataManager.Instance.nowMusicPage).gameObject;

        TextMeshProUGUI thisText = transform.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI preText=musicScene.transform.Find(DataManager.Instance.nowMusicPage+"Button").GetComponentInChildren<TextMeshProUGUI>();
        if (thisText != null)
        {
            thisText.fontSize = 75;
        }
        if(preText != null)
        {
            preText.fontSize = 50;
        }
        DataManager.Instance.nowMusicPage = thisPage.name;
        prePage.SetActive(false);
        thisPage.SetActive(true);
    }
}
