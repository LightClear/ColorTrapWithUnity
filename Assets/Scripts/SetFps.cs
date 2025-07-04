using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFps : MonoBehaviour
{
    public int fps;
    private void Awake()
    {
        Application.targetFrameRate = fps;
    }
}
