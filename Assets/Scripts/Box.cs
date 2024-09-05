using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Color finishColor;
    Color originColor;

    private void Awake(){
        originColor = GetComponent<SpriteRenderer>().color;
        // FindObjectOfType<GameManager>().totalBoxs++;
    }
    
    public void Start()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Target"))
        {
            FindObjectOfType<GameManager>().finishedBoxs++;
            FindObjectOfType<GameManager>().CheckFinish();
            GetComponent<SpriteRenderer>().color = finishColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            FindObjectOfType<GameManager>().finishedBoxs--;
            GetComponent<SpriteRenderer>().color = originColor;
        }
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
        // FindObjectOfType<GameManager>().totalBoxs--;
        Destroy(gameObject); // 1f 表示延迟 1 秒

    }
}
