using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    //�����л���������Ľű��������ڰ�ť�ϣ����緵�ذ�ť����ʼ���水ť
    public GameObject thisScene;
    public GameObject nextScene;

    public void onClick()
    {
        thisScene.SetActive(false);
        nextScene.SetActive(true);
    }
}
