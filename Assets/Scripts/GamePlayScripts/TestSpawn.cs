using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TestSpawn : MonoBehaviour
{
    //��Ϸ�������Ͻǵ���������: (18.68, -26.20, -0.60)
    //��������(0.00, 610.00)
    public GameObject background;
    public GameObject obj;
    public Transform parentObject; // ������
    private Vector3 world = new Vector3(18.68f, -26.20f, 0);
    private Vector3 screen = new Vector3(0.00f, 610.00f, 0);
    public TextMeshProUGUI text;

    void Start()
    {
        //float X=background.transform.position.x;
        //float Y=background.transform.position.y;
        //float Z=background.transform.position.z;
        //Vector3 back=new Vector3(X, Y, Z);
        //GameObject spawnedObject = Instantiate(obj, back, Quaternion.identity, parentObject);
        //Debug.Log("��������");
        //// ��������Ƿ�ɹ�����
        //if (spawnedObject != null)
        //{
        //    Debug.Log("����ɹ�����: " + spawnedObject.name);
        //}
        //else
        //{
        //    Debug.LogError("��������ʧ�ܣ�");
        //}
        test();
    }

    void Update()
    {
        
    }

    void test()
    {
        text.color = new Color(247f / 255f, 150f / 255f, 30f / 255f); // ��ɫ
    }
}
