using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void OnEnable()
    {
        SetText();
    }

    void Update()
    {
        SetText();
    }

    void SetText()
    {
        text.text = "当前分数：" + DataManager.Instance.score;
        if (DataManager.Instance.score <= 10) text.color = Color.green;
        else if (DataManager.Instance.score <= 20) text.color = new Color(0, 0, 215 / 255f);
        else text.color = Color.yellow;
    }
}
