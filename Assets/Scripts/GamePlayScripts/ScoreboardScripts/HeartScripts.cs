using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScripts : MonoBehaviour
{
    public GameObject thisGameObject;

    private void OnDisable()
    {
        Debug.Log(gameObject.name);
        Destroy(thisGameObject);
    }
}
