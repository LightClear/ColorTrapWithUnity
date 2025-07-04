using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RuleButton : MonoBehaviour
{
    /*
    public string childObjectName = "MyInactiveChildObject"; // 要查找的子对象名称

    void Start()
    {
        // 查找非活动子对象
        GameObject foundChild = 

        // 检查是否找到该子对象
        if (foundChild != null)
        {
            Debug.Log("找到非活动子对象: " + foundChild.name);
            // 你可以在这里对找到的GameObject进行操作
        }
        else
        {
            Debug.LogError("未找到名称为 '" + childObjectName + "' 的非活动子对象。");
        }
    }

    */

    public Transform parentObject; // 父对象
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
        //查找前一个显示的规则按钮，将其初始化
        GameObject preRule = FindInactiveChildByName(parentObject, DataManager.Instance.preRuleButton);
        Debug.Log("previousRuleButtonName: " + DataManager.Instance.preRuleButton);
        if (preRule != null)
        {
            Debug.Log("找到规则按钮" +  preRule.name);
            preRule.SetActive(true);
        }
        else
        {
            Debug.LogError("未找到规则按钮" + DataManager.Instance.preRuleButton + "，可能为上页按钮");
        }

        ruleButton.SetActive(false);
        ruleImage.SetActive(true);
        disabledImage.SetActive(true);
        DataManager.Instance.preRuleButton = thisRuleName;
        Debug.Log("当前按钮" + DataManager.Instance.preRuleButton);
        Debug.Log("转换为模式介绍" + thisRuleName);
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
