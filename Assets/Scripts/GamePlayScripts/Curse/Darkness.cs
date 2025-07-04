using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{
    Vector3 screenPosition;
    Vector3 mousePoitionOnScreen;
    Vector3 mousePositionInWorld;

    void Update()
    {
        MouseFollow();
    }

    void MouseFollow()
    {
        screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        mousePoitionOnScreen=Input.mousePosition;
        mousePoitionOnScreen.z = screenPosition.z;
        mousePositionInWorld=Camera.main.ScreenToWorldPoint(mousePoitionOnScreen);
        transform.position = mousePositionInWorld;
    }
}
