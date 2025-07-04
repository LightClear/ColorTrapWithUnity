using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindScreenLocation : MonoBehaviour
{
    private Camera targetCamera;

    void Start()
    {
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("GameSceneCamera");
        // 检查是否找到了摄像机
        if (cameras.Length > 0)
        {
            // 选择第一个找到的摄像机
            targetCamera = cameras[0].GetComponent<Camera>();
            Debug.Log("找到摄像机: " + targetCamera.name);
        }
        else
        {
            Debug.LogError("没有找到具有标签 '" + "' 的摄像机。");
            return; // 退出Start方法
        }

        // 获取屏幕左上角的屏幕坐标
        Vector2 screenPosition = new Vector2(0, Screen.height);

        // 将屏幕坐标转换为世界坐标
        Vector3 worldPosition = targetCamera.ScreenToWorldPoint(screenPosition);

        // 输出左上角的世界坐标
        Debug.Log("游戏窗口左上角的世界坐标: " + worldPosition);
        Debug.Log("窗口坐标" + screenPosition);
    }

    void Update()
    {
        
    }
}
