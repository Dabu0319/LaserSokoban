using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveDir;
    public LayerMask detectLayer;
    public float raycastDistance = 1.0f;
    public event Action<Vector2> OnMoveSuccess; // 事件声明
    
    public GameObject upSprite;
    public GameObject downSprite;
    public GameObject leftSprite;
    public GameObject rightSprite;
    public Vector2 lastMoveDir;
    
    
    [SerializeField]
    private Vector2 currentDir;
    private GameManager gameManager;

    void Start()
    {
        // 在开始时找到GameManager的实例
        gameManager = FindObjectOfType<GameManager>();
    }
    
    void Update()
    {
        if (gameManager.IsPaused())
            return;

        if (Input.GetKeyDown(KeyCode.RightArrow) ||Input.GetKeyDown(KeyCode.D) )
            moveDir = Vector2.right;

        if (Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetKeyDown(KeyCode.A))
            moveDir = Vector2.left;

        if (Input.GetKeyDown(KeyCode.UpArrow) ||Input.GetKeyDown(KeyCode.W))
            moveDir = Vector2.up;

        if (Input.GetKeyDown(KeyCode.DownArrow)||Input.GetKeyDown(KeyCode.S))
            moveDir = Vector2.down;

        if(moveDir != Vector2.zero)
        {
            if(CanMoveToDir(moveDir))
            {
                Move(moveDir);
                lastMoveDir = moveDir; // 更新最后的移动方向
                OnMoveSuccess?.Invoke(lastMoveDir); // 触发事件
            }
        }
        moveDir = Vector2.zero;
    }

   
bool CanMoveToDir(Vector2 dir)
{
    // 添加 LayerMask 参数来指定检测的层
    LayerMask detectLayer = LayerMask.GetMask("Wall", "Box", "RefractiveBox"); // 这里添加了 Wall，你可以根据实际需要调整层的名称和包含的层

    // 发射射线，同时指定 LayerMask
    RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, raycastDistance, detectLayer);

    if (!hit)
    {
        // 如果没有碰撞，可以移动
        return true;
    }
    else
    {
        // 检查碰撞对象是否为 Wall 层
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            // 如果是 Wall，根据需要处理，这里假设不能移动
            return false;
        }

        // 检查其他类型的碰撞并递归调用
        if (hit.collider.GetComponent<Box>() != null)
        {
            // 如果碰到的是另一个 Box，递归检查是否可以移动
            return hit.collider.GetComponent<Box>().CanMoveToDir(dir);
        }
        else if (hit.collider.GetComponent<ReflectMove>() != null)
        {
            // 如果碰到的是具有反射移动能力的对象，检查是否可以移动
            return hit.collider.GetComponent<ReflectMove>().CanMoveToDir(dir);
        }
    }
    // 如果其他情况都不允许移动
    return false;
}
    void Move(Vector2 dir)
    {
        transform.Translate(dir);
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.down * raycastDistance);
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BadPeople"))
        {
            BadPeople badPeople = other.GetComponent<BadPeople>();
            if (badPeople != null)
            {
                FindObjectOfType<GameManager>().ResetStage();
            }
        }

    }
}
