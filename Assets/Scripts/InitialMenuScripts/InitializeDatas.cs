using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeDatas : MonoBehaviour
{
    //�������£�
    //�Ƿ��Ѿ���ʼ�������ݣ�hadInitialized
    //ģʽ��mode����race, survival, limit, race
    //�Ѷȣ�difficulty����difficultyHp��difficultyTime
    //����������goldApple, totem, timeSuspend, hamburger
    //��ǰ���䣺curse
    //�𰸣�answer

    private void Awake()
    {
        if(DataManager.Instance.isLocalDeploy)gameObject.SetActive(false);

        int hadInitialized = PlayerPrefs.GetInt("hadInitialized", -1);
        if (hadInitialized != 1)
        {
            Debug.Log("δ���й���ʼ�������ڽ��г�ʼ��");
            //�����ѳ�ʼ��
            PlayerPrefs.SetInt("hadInitialized", 1);
            //��ʼ��ģʽ
            PlayerPrefs.SetString("mode", "race");
            //��ʼ���Ѷ�
            PlayerPrefs.SetInt("difficultyHp", 5);
            PlayerPrefs.SetInt("difficultyTime", 60);
            //��ʼ����������
            PlayerPrefs.SetInt("goldApple", 3);
            PlayerPrefs.SetInt("totem", 3);
            PlayerPrefs.SetInt("timesuspend", 3);
            PlayerPrefs.SetInt("hamburger", 3);

            checkInitializedSuccessfully();
        }
        else Debug.Log("�Ѿ����й���ʼ��");
    }

    //����Ƿ�ɹ���ʼ��
    void checkInitializedSuccessfully()
    {
        if (PlayerPrefs.GetString("mode", "null") != "race") Debug.LogError("ģʽδ�ܳɹ���ʼ��");
        if (PlayerPrefs.GetInt("difficultyHp", -1) != 5) Debug.LogError("Ѫ���Ѷ�δ�ܳɹ���ʼ��");
        if (PlayerPrefs.GetInt("difficultyTime", -1) != 60) Debug.LogError("ʱ���Ѷ�δ�ܳɹ���ʼ��");
        if (PlayerPrefs.GetInt("goldApple", -1)!=3) Debug.LogError("��ƻ����������δ�ܳɹ���ʼ��");
        if (PlayerPrefs.GetInt("totem", -1) != 3) Debug.LogError("����ͼ�ڵ�������δ�ܳɹ���ʼ��");
        if (PlayerPrefs.GetInt("timesuspend", -1) != 3) Debug.LogError("ʱͣ��������δ�ܳɹ���ʼ��");
        if (PlayerPrefs.GetInt("hamburger", -1) != 3) Debug.LogError("������������δ�ܳɹ���ʼ��");
    }
}
