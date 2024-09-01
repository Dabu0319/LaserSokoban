using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public Transform player;
    public PlayerController playerController;
    public float maxDistance = 5f;
    private LineRenderer lineRenderer;
    public LayerMask layerMask;
    private Vector2 currentDirection;
    public GameObject triggerPrefab; // 触发器预制体

    public enum Direction { Left, Right, Up, Down }
    public Direction initialDirection;
    private List<GameObject> triggers = new List<GameObject>(); // 存储所有触发器实例

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        currentDirection = DirectionToVector2(initialDirection);
        transform.right = currentDirection;
    }

    void Update()
    {
        Vector2 moveDirection = playerController.lastMoveDir;
        if (moveDirection != Vector2.zero && Vector2.Dot(currentDirection, moveDirection) >= 0)
        {
            currentDirection = moveDirection;
            transform.right = currentDirection;
        }

        Vector2 start = (Vector2)player.position + currentDirection * 0.2f;
        ClearTriggers();
        UpdateLaserAndTrigger(start, currentDirection, maxDistance, 0);
    }

    private void ClearTriggers()
    {
        foreach (var trigger in triggers)
        {
            Destroy(trigger);
        }
        triggers.Clear();
    }

    private void UpdateLaserAndTrigger(Vector2 start, Vector2 direction, float distance, int reflections)
    {
        if (reflections > 5) return;

        RaycastHit2D hit = Physics2D.Raycast(start, direction, distance, layerMask);
        Vector2 end = hit.collider ? hit.point : start + direction * distance;
        end += direction.normalized * 0.3f; // 增加0.3f长度
        lineRenderer.positionCount = reflections + 2;
        lineRenderer.SetPosition(reflections, start);

        CreateTrigger(start, end); // 创建触发器

        if (hit.collider && hit.collider.tag == "Refractive")
        {
            Vector2 newDirection = Vector2.Reflect(direction, hit.normal);
            Vector2 reflectionStart = hit.point + newDirection * 0.01f;
            float newDistance = distance - hit.distance + 0.3f;
            UpdateLaserAndTrigger(reflectionStart, newDirection, newDistance, reflections + 1);
        }
        else
        {
            lineRenderer.SetPosition(reflections + 1, end);
        }
    }

    private void CreateTrigger(Vector2 start, Vector2 end)
    {
        GameObject trigger = Instantiate(triggerPrefab, Vector2.zero, Quaternion.identity);
        trigger.transform.position = (start + end) / 2;
        trigger.transform.right = (end - start).normalized;

        float length = Vector2.Distance(start, end);
        length += 0.3f; // 触发器长度增加0.3f
        EdgeCollider2D edgeCollider = trigger.GetComponent<EdgeCollider2D>();
        edgeCollider.points = new Vector2[] { new Vector2(-length / 2, 0), new Vector2(length / 2, 0) };

        triggers.Add(trigger);
    }

    private Vector2 DirectionToVector2(Direction dir)
    {
        switch (dir)
        {
            case Direction.Left: return Vector2.left;
            case Direction.Right: return Vector2.right;
            case Direction.Up: return Vector2.up;
            case Direction.Down: return Vector2.down;
        }
        return Vector2.right;
    }
}