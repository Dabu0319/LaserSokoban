using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public Transform player; // 玩家的Transform
    public PlayerController playerController; // 玩家控制器的引用
    public float maxDistance = 5f; // 最大射线距离
    private LineRenderer lineRenderer; // 线渲染器
    public LayerMask layerMask; // 碰撞层筛选
    public LayerMask triggerLayerMask; // 用于触发器的碰撞层
    private Vector3 currentLaserDirection; // 当前激光方向
    private Vector3 currentTriggerDirection; // 当前触发器方向

    public enum Direction { Left, Right, Up, Down } // 方向枚举
    public Direction initialDirection; // 初始方向设置
    public float triggerDistance; // 触发器长度
    private EdgeCollider2D triggerCollider; // 触发器组件
    public 

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        triggerCollider = GetComponent<EdgeCollider2D>(); // 获取触发器组件
        SetInitialDirection(); // 设置初始激光和触发器方向
    }

    void SetInitialDirection()
    {
        switch (initialDirection)
        {
            case Direction.Left:
                currentLaserDirection = Vector2.left;
                currentTriggerDirection = Vector2.left;
                break;
            case Direction.Right:
                currentLaserDirection = Vector2.right;
                currentTriggerDirection = Vector2.right;
                break;
            case Direction.Up:
                currentLaserDirection = Vector2.up;
                currentTriggerDirection = Vector2.up;
                break;
            case Direction.Down:
                currentLaserDirection = Vector2.down;
                currentTriggerDirection = Vector2.down;
                break;
        }
        transform.right = currentLaserDirection; // 初始化激光方向
    }

    void Update()
    {
        if (playerController != null)
        {
            Vector2 moveDirection = playerController.lastMoveDir;

            if (moveDirection != Vector2.zero && !AreDirectionsOpposite(currentLaserDirection, moveDirection))
            {
                currentLaserDirection = new Vector3(moveDirection.x, moveDirection.y, 0);
                currentTriggerDirection = new Vector3(moveDirection.x, moveDirection.y, 0);
                transform.right = currentLaserDirection; // 更新激光发射器的方向
                UpdateTriggerDirection(); // 更新触发器的方向
                Debug.Log("Laser direction updated to: " + currentLaserDirection);
            }

        }
        EmitLaser(); // 发射激光
        ControlTrigger(); // 控制触发器
    }

    bool AreDirectionsOpposite(Vector3 dir1, Vector2 dir2)
    {
        return Vector2.Dot(dir1, dir2) < 0;
    }

    void EmitLaser()
    {
        Vector2 start = (Vector2)playerController.transform.position + (Vector2)currentLaserDirection.normalized * 0.2f;
        Vector2 direction = currentLaserDirection;

        RaycastHit2D hit = Physics2D.Raycast(start, direction, maxDistance, layerMask);

        if (hit.collider != null)
        {
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, hit.point);
            triggerDistance = Vector2.Distance(start, hit.point); // 计算两点直接距离
        }
        else
        {
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, start + direction * maxDistance);
            triggerDistance = maxDistance;
        }
    }

    void ControlTrigger()
    {
        // 设置触发器的位置
        triggerCollider.transform.position = (Vector2)playerController.transform.position + (Vector2)currentLaserDirection.normalized * 0.2f;

        // 设置触发器的长度
        Vector2[] points = new Vector2[]
        {
            Vector2.zero,
            new Vector2(triggerDistance, 0)
        };
        triggerCollider.points = points;
        Debug.Log("Trigger distance: " + triggerDistance);
    }

    void UpdateTriggerDirection()
    {
        // 调整触发器的方向，使其朝向 currentTriggerDirection
        float angle = Mathf.Atan2(currentTriggerDirection.y, currentTriggerDirection.x) * Mathf.Rad2Deg;
        triggerCollider.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}