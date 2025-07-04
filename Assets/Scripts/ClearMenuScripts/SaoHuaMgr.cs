using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaoHuaMgr : MonoBehaviour
{
    public List<GameObject> cai;
    public List<GameObject> good;
    public List<GameObject> excellent;
    public GameObject nb;
    public GameObject man;

    public GameObject BGMPlayer;

    private GameObject theShown;

    private void OnEnable()
    {
        if (theShown != null) theShown.SetActive(false);

        if (DataManager.Instance.score <= 10)
        {
            theShown = cai[Random.Range(0, cai.Count)];
            theShown.SetActive(true);
        }
        else if(DataManager.Instance.score < 24)
        {
            theShown = good[Random.Range(0, good.Count)];
            theShown.SetActive(true);
        }
        else if(DataManager.Instance.score == 24)
        {
            theShown = man;
            theShown.SetActive(true);
        }
        else if(DataManager.Instance.score < 50)
        {
            theShown = excellent[Random.Range(0, excellent.Count)];
            theShown.SetActive(true);
        }
        else
        {
            theShown = nb;
            theShown.SetActive(true);
        }
    }
}
