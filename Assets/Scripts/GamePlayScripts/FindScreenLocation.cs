using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindScreenLocation : MonoBehaviour
{
    private Camera targetCamera;

    void Start()
    {
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("GameSceneCamera");
        // ����Ƿ��ҵ��������
        if (cameras.Length > 0)
        {
            // ѡ���һ���ҵ��������
            targetCamera = cameras[0].GetComponent<Camera>();
            Debug.Log("�ҵ������: " + targetCamera.name);
        }
        else
        {
            Debug.LogError("û���ҵ����б�ǩ '" + "' ���������");
            return; // �˳�Start����
        }

        // ��ȡ��Ļ���Ͻǵ���Ļ����
        Vector2 screenPosition = new Vector2(0, Screen.height);

        // ����Ļ����ת��Ϊ��������
        Vector3 worldPosition = targetCamera.ScreenToWorldPoint(screenPosition);

        // ������Ͻǵ���������
        Debug.Log("��Ϸ�������Ͻǵ���������: " + worldPosition);
        Debug.Log("��������" + screenPosition);
    }

    void Update()
    {
        
    }
}
