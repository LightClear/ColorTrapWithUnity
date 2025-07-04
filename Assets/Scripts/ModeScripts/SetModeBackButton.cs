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
            else Debug.LogError("模式选择界面前置界面设置错误" + DataManager.Instance.theModeSceneFrom);
        }
        else Debug.LogError("未设置是否为chooser或者seter");
    }
}
