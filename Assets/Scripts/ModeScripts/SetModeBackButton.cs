using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetModeBackButton : MonoBehaviour
{
    public bool isChooser;
    public bool isSeter;
    public string chooserName;
    public GameObject initialBackButton;
    public GameObject clearBackButton;

    private void OnEnable()
    {
        if (isChooser)
        {
            DataManager.Instance.theModeSceneFrom = chooserName;
        }
        else if (isSeter)
        {
            if (DataManager.Instance.theModeSceneFrom == "InitialMenu")
            {
                initialBackButton.SetActive(true);
                clearBackButton.SetActive(false);
            }
            else if (DataManager.Instance.theModeSceneFrom == "ClearMenu")
            {
                initialBackButton.SetActive(false);
                clearBackButton.SetActive(true);
            }
            else Debug.LogError("ģʽѡ�����ǰ�ý������ô���" + DataManager.Instance.theModeSceneFrom);
        }
        else Debug.LogError("δ�����Ƿ�Ϊchooser����seter");
    }
}
