using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSceneFault : MonoBehaviour
{
    public GameObject Limit;
    public GameObject Race;
    public GameObject Survival;
    public GameObject Hell;
    void Start()
    {
        Limit.SetActive(false);
        Race.SetActive(false);
        Survival.SetActive(false);
        Hell.SetActive(false);
    }
}
