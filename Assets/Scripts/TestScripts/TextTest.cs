using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTest : MonoBehaviour
{
    public GameObject theText;
    public TextMeshProUGUI text;

    private void Awake()
    {
        text = theText.transform.GetComponent<TextMeshProUGUI>();
        if (text != null)
        {
            print("\ntest");
            string tmp = "ÄãºÃ";
            tmp.Insert(1, "\n");
            print(tmp);
            for(int i = 0; i < tmp.Length; i++)
            {
                print($"×Ö·û£º{i}: {(int)tmp[i]}");
            }
            text.text = tmp[0] + "\n" + tmp[1];
        }
    }
}
