using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScoreText : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void OnEnable()
    {
        int score = DataManager.Instance.score;
        text.text = "×îÖÕµÃ·Ö£º" + score;
        if (score < 10) text.color = Color.green;
        else if (score < 20) text.color = new Color(0, 0, 215 / 255f);
        else text.color = Color.yellow;

    }
}
