using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Color finishColor;  // 完成时的颜色
    public float raycastLength = 0.7f;  // 射线长度，可以在 Inspector 中调整
    private Color originColor; // 原始颜色

    private void Awake()
    {
        originColor = GetComponent<SpriteRenderer>().color;  // 在Awake时保存初始颜色
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 当箱子移动到目标位置时触发
        if (collision.CompareTag("Target"))
        {
            FindObjectOfType<GameManager>().finishedBoxs++;  // 增加已完成的箱子计数
            FindObjectOfType<GameManager>().CheckFinish();   // 检查是否所有箱子都已完成
            FindObjectOfType<GameManager>().Boxdisplay();    // 更新显示状态
            GetComponent<SpriteRenderer>().color = finishColor;  // 改变箱子颜色为完成状态颜色
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 当箱子离开目标位置时触发
        if (collision.CompareTag("Target"))
        {
            FindObjectOfType<GameManager>().finishedBoxs--;  // 减少已完成的箱子计数
            FindObjectOfType<GameManager>().Boxdisplay();    // 更新显示状态
            GetComponent<SpriteRenderer>().color = originColor;  // 恢复箱子颜色为原始颜色
        }
    }

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

    public void DestroyBox()
    {
        // 销毁箱子
        Destroy(gameObject);
    }
}