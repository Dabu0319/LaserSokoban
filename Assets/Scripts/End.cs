using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 确保包含这个命名空间

public class End : MonoBehaviour
{
void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Laser"))
    {
        // 如果是激光触发，则进行延迟销毁
        StartCoroutine(DelayedDestruction());
    }

    // 检查触碰的物体是否属于Player
    if (other.CompareTag("Player"))
    {
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
    IEnumerator DelayedDestruction()
    {
        // 等待一秒钟
        yield return new WaitForSeconds(0.25f);

        // 销毁当前物体（即带有"End"标签的物体）
        Destroy(gameObject);

        // 调用游戏失败的逻辑
        GameOver();
    }

    void GameOver()
    {
        // 这里可以扩展游戏失败的逻辑，比如显示失败消息、播放失败音效等
        Debug.Log("Game Over! You have failed.");

        // 重新加载当前场景以重启关卡
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}