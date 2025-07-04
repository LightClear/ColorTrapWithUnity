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
        if (thisPageParentObject == null) Debug.LogError("thisPageParentObject不存在！");
        if (otherpageParentObject == null) Debug.LogError("otherpageParentObject不存在！");

        if (pageNumber == 1)
        {
            SetPreRuleButtonFalse(thisPageParentObject);
            //SetPreRuleButtonFalse(otherpageParentObject);
            DataManager.Instance.preRuleButton = theFirstRuleButton.name;
            Debug.Log("触发" + theFirstRuleButton.name);
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
            Debug.Log("找到规则按钮" + preRule.name);
            preRule.SetActive(true);
        }
        else
        {
            Debug.LogError("未找到规则按钮" + preRule.name + "，可能不在本页" + parentObject + "内");
        }
    }

    GameObject FindInactiveChildByName(Transform parent, string name)
    {
        // 获取所有子对象，包括非活动的
        var allChildren = parent.GetComponentsInChildren<Transform>(true);

        // 查找具有指定名称的子对象
        var foundChild = allChildren.FirstOrDefault(child => child.name == name);

        // 返回找到的GameObject
        return foundChild != null ? foundChild.gameObject : null;
    }
}
