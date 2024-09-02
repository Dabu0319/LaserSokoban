using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            moveDir = Vector2.right;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            moveDir = Vector2.left;

        if (Input.GetKeyDown(KeyCode.UpArrow))
            moveDir = Vector2.up;

        if (Input.GetKeyDown(KeyCode.DownArrow))
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, raycastDistance, detectLayer);

        if (!hit)
            return true;
        else
        {
            if (hit.collider.GetComponent<Box>() != null)
                return hit.collider.GetComponent<Box>().CanMoveToDir(dir);
        }

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
                FindObjectOfType<Laseritem>().GameOver();
            }
        }

    }
}
