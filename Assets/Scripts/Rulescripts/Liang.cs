using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liang : MonoBehaviour
{
    public GameObject otherLiang;

    private Vector3 mousePositionInWorld;
    private Vector3 thisPositionIntWorld;

    public float leftX, rightX;
    public float topY, bottomY;
    public float stopDistance, moveDistance;


    private void Update()
    {
        GetPositionInWorld();
        //Test();
        if (IsInMoveRect())
        {
            transform.position = new Vector3(mousePositionInWorld.x, transform.position.y, transform.position.z);
        }
        else if(CalDistance(mousePositionInWorld, transform.position) <= moveDistance)
        {
            AddPositionX();
            if (transform.position.x < leftX || transform.position.x > rightX)
            {
                transform.position = new Vector3(transform.position.x < leftX ? leftX : rightX,
                    transform.position.y, transform.position.z);
                otherLiang.transform.position = new Vector3(transform.position.x, otherLiang.transform.position.y);
                otherLiang.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
    }

    void AddPositionX()
    {
        if (mousePositionInWorld.x < transform.position.x)
        {
            transform.position += new Vector3(Mathf.Sqrt(moveDistance + 0.1f - Mathf.Pow(mousePositionInWorld.y - transform.position.y, 2))
                , 0f, 0f);
        }
        else
        {
            transform.position -= new Vector3(Mathf.Sqrt(moveDistance + 0.1f - Mathf.Pow(mousePositionInWorld.y - transform.position.y, 2))
                , 0f, 0f);
        }
    }

    void GetPositionInWorld()
    {
        mousePositionInWorld = Input.mousePosition;
        //print("mousePositionInScreen: " + mousePositionInWorld);
        mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionInWorld);
        //print("mousePositionInWorld: " + mousePositionInWorld);
        //mousePositionInWorld.z=transform.position.z;
        //print("LiangPositionInworld" + transform.position);
    }

    bool IsInMoveRect()
    {
        if (mousePositionInWorld.x < leftX || mousePositionInWorld.x > rightX) return false;
        if (transform.name == "LiangUp" && mousePositionInWorld.y > topY)
        {
            return false;
        }
        else if (transform.name == "LiangDown" && mousePositionInWorld.y < bottomY)
        {
            return false;
        }
        return true;
    }

    float CalDistance(Vector3 v1, Vector3 v2)
    {
        return Mathf.Sqrt(Mathf.Pow(mousePositionInWorld.x - transform.position.x, 2) +
                Mathf.Pow(mousePositionInWorld.y - transform.position.y, 2));
    }

    void Test()
    {
        string input= Input.inputString;
        if(input == "1")
        {
            //print("mousePositionInWorld" + mousePositionInWorld);
            //transform.position = new Vector3(mousePositionInWorld.x, transform.position.y, transform.position.z);
            //Vector3 thisPositionInScreen = Camera.main.WorldToScreenPoint(transform.position);
            //print("thisPositionInScreen" + thisPositionInScreen);
            float distance = Mathf.Sqrt(Mathf.Pow(mousePositionInWorld.x - transform.position.x, 2) +
                Mathf.Pow(mousePositionInWorld.y - transform.position.y, 2));
            print("Distance: " + /*Vector3.Distance(mousePositionInWorld, thisPositionIntWorld)*/distance);
            print("mousePositionInWorld" + mousePositionInWorld);
            print("LiangPosition" + transform.position);
        }
        
    }
}
