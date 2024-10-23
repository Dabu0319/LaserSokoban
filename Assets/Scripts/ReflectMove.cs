using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectMove : MonoBehaviour
{
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
}
