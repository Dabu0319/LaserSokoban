using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<GameManager>().totalBoxs++;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //     private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if(collision.CompareTag("IronBox"))
    //     {
    //         FindObjectOfType<GameManager>().finishedBoxs++;
    //         FindObjectOfType<GameManager>().CheckFinish();
    //         // GetComponent<SpriteRenderer>().color = finishColor;
    //     }

    //     if(collision.CompareTag("Box"))
    //     {
    //         FindObjectOfType<GameManager>().finishedBoxs++;
    //         FindObjectOfType<GameManager>().CheckFinish();
    //         // GetComponent<SpriteRenderer>().color = finishColor;
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("IronBox"))
    //     {
    //         FindObjectOfType<GameManager>().finishedBoxs--;
    //         // GetComponent<SpriteRenderer>().color = originColor;
    //     }

    //     if (collision.CompareTag("Box"))
    //     {
    //         FindObjectOfType<GameManager>().finishedBoxs--;
    //         // GetComponent<SpriteRenderer>().color = originColor;
    //     }
    // }

    
}
