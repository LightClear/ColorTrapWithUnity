using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RuleButton : MonoBehaviour
{
    /*
    public string childObjectName = "MyInactiveChildObject"; // Ҫ���ҵ��Ӷ�������

    void Start()
    {
        // ���ҷǻ�Ӷ���
        GameObject foundChild = 

        // ����Ƿ��ҵ����Ӷ���
        if (foundChild != null)
        {
            Debug.Log("�ҵ��ǻ�Ӷ���: " + foundChild.name);
            // �������������ҵ���GameObject���в���
        }
        else
        {
            Debug.LogError("δ�ҵ�����Ϊ '" + childObjectName + "' �ķǻ�Ӷ���");
        }
    }

    */

    public Transform parentObject; // ������
    public GameObject ruleImage;
    public GameObject ruleButton;
    public GameObject disabledImage;
    public string thisRuleName;

    private void OnEnable()
    {
        ruleImage.SetActive(false);
        disabledImage.SetActive(false);
    }

    public void onClick()
    {
        //����ǰһ����ʾ�Ĺ���ť�������ʼ��
        GameObject preRule = FindInactiveChildByName(parentObject, DataManager.Instance.preRuleButton);
        Debug.Log("previousRuleButtonName: " + DataManager.Instance.preRuleButton);
        if (preRule != null)
        {
            Debug.Log("�ҵ�����ť" +  preRule.name);
            preRule.SetActive(true);
        }
        else
        {
            Debug.LogError("δ�ҵ�����ť" + DataManager.Instance.preRuleButton + "������Ϊ��ҳ��ť");
        }

        ruleButton.SetActive(false);
        ruleImage.SetActive(true);
        disabledImage.SetActive(true);
        DataManager.Instance.preRuleButton = thisRuleName;
        Debug.Log("��ǰ��ť" + DataManager.Instance.preRuleButton);
        Debug.Log("ת��Ϊģʽ����" + thisRuleName);
    }

    GameObject FindInactiveChildByName(Transform parent, string name)
    {
        // ��ȡ�����Ӷ��󣬰����ǻ��
        var allChildren = parent.GetComponentsInChildren<Transform>(true);

        // ���Ҿ���ָ�����Ƶ��Ӷ���
        var foundChild = allChildren.FirstOrDefault(child => child.name == name);

        // �����ҵ���GameObject
        return foundChild != null ? foundChild.gameObject : null;
    }
}
