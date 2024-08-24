using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerdirection : MonoBehaviour
{
    public LaserController laserController; // 引用激光控制器

    void Update()
    {
        if (laserController != null)
        {
            // 获取当前激光方向
            Vector3 laserDirection = laserController.transform.right;

            // 如果你的人物图案前方是向上的，我们需要调整方向
            // 以使图案的上方与激光的右方向对齐
            transform.up = laserDirection;
        }
    }
}
