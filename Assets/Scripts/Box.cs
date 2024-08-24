using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Color finishColor;
    Color originColor;

    private void Start()
    {
        originColor = GetComponent<SpriteRenderer>().color;
    }

    public bool CanMoveToDir(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)dir * 0.5f, dir, 0.5f);

        if (!hit)
        {
            transform.Translate(dir);
            return true;
        }

        return false;
    }

    public void DestroyBox()
    {
        Destroy(gameObject, 0.25f); // 1f 表示延迟 1 秒
    }
    
}
