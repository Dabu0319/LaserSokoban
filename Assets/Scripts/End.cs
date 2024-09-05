using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 确保包含这个命名空间

public class End : MonoBehaviour
{

    public bool openend = false;
    public Sprite initialSprite; // 初始精灵
    public Sprite newSprite; // 之后的精灵
    private SpriteRenderer spriteRenderer; // 添加一个 SpriteRenderer 变量
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // 获取 SpriteRenderer 组件
        // OpenColor();
    }
    


    void OnTriggerEnter2D(Collider2D other)
    {
        // 检查触碰的物体是否属于Player
        if (other.CompareTag("Player"))
        {
            openend = true;
            var gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.destination = true;
                gameManager.CheckFinish();
            }
            else
            {
                Debug.LogError("GameManager not found in the scene.");
            }
        }
    }

        void OnTriggerExit2D(Collider2D collision)
    {
        // 检查触碰的物体是否属于Player
        if (collision.CompareTag("Player"))
        {
            var gameManager = FindObjectOfType<GameManager>();
            gameManager.destination = false;
            openend = false;
        }
    }


    public void DestroyEnd()
    {
        if (openend == false){
        Destroy(gameObject, 0.5f);
        FindObjectOfType<GameManager>().ResetStage();
        
        }
    }

    public void OpenColor(){
        if(FindObjectOfType<GameManager>().BadGays == true){
            spriteRenderer.sprite = initialSprite;
        }else{
            spriteRenderer.sprite = newSprite;
        }
    }

}