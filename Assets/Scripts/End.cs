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
    public GameManager gameManager;
  
    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // 获取 SpriteRenderer 组件
    }

    public void Start(){
        OpenColor();
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
        Invoke("ResetGameStage", 0.5f);  // 延迟1.0秒后调用ResetGameStage方法
        Destroy(gameObject, 0.5f);
        }
    }

    private void ResetGameStage()
{
    FindObjectOfType<GameManager>().ResetStage();  // 调用GameManager中的ResetStage方法
}


    public void OpenColor(){
        var gameManager = FindObjectOfType<GameManager>();
        Debug.Log(gameManager.BadGays);
        if(FindObjectOfType<GameManager>().BadGays == false){
            spriteRenderer.sprite = newSprite;
        }else{
            spriteRenderer.sprite = initialSprite;
        }
    }

}