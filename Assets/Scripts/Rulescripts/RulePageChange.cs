using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RulePageChange : MonoBehaviour
{
    public Transform thisPageParentObject;
    public Transform otherpageParentObject;
    public Button theFirstRuleButton;
    public GameObject thisPage;
    public GameObject previousPage;
    public GameObject otherPageButton;
    public int pageNumber;

    private void OnEnable()
    {
        if (thisPageParentObject == null) Debug.LogError("thisPageParentObject�����ڣ�");
        if (otherpageParentObject == null) Debug.LogError("otherpageParentObject�����ڣ�");

        if (pageNumber == 1)
        {
            SetPreRuleButtonFalse(thisPageParentObject);
            //SetPreRuleButtonFalse(otherpageParentObject);
            DataManager.Instance.preRuleButton = theFirstRuleButton.name;
            Debug.Log("����" + theFirstRuleButton.name);
            previousPage.SetActive(false);
            theFirstRuleButton.onClick.Invoke();
        }
        
    }

    public void onClick()
    {
        TextMeshProUGUI otherText = otherPageButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI thisText = this.GetComponentInChildren<TextMeshProUGUI>();
        if(otherText != null)
        {
            otherText.fontSize = 55;
        }
        if(thisText != null)
        {
            thisText.fontSize = 70;
        }

        SetPreRuleButtonFalse(otherpageParentObject);
        thisPage.SetActive(true);
        theFirstRuleButton.onClick.Invoke();
        previousPage.SetActive(false);
        
    }

    void SetPreRuleButtonFalse(Transform parentObject)
    {
        GameObject preRule = FindInactiveChildByName(parentObject, DataManager.Instance.preRuleButton);
        Debug.Log("previousRuleButtonName: " + DataManager.Instance.preRuleButton);
        if (preRule != null)
        {
            Debug.Log("�ҵ�����ť" + preRule.name);
            preRule.SetActive(true);
        }
        else
        {
            Debug.LogError("δ�ҵ�����ť" + preRule.name + "�����ܲ��ڱ�ҳ" + parentObject + "��");
        }
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
