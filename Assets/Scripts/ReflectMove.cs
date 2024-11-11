using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectMove : MonoBehaviour
{
    public float raycastLength = 0.7f;  // 射线长度，可以在 Inspector 中调整
    public bool CanMoveToDir(Vector2 dir)
    {
        // 计算射线的起点和终点
        Vector3 start = transform.position + (Vector3)dir * raycastLength;
        Vector3 end = start + (Vector3)dir * raycastLength;
        
        // 发射射线
        RaycastHit2D hit = Physics2D.Raycast(start, dir, raycastLength);

        // 绘制射线以便在Unity编辑器中可视化
        Debug.DrawLine(start, end, hit ? Color.red : Color.green, raycastLength);

        // 如果没有碰撞，允许移动
        if (!hit)
        {
            transform.Translate(dir);
            return true;
        }

        // 如果有碰撞，不允许移动
        return false;
    }
}
