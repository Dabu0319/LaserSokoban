using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laseritem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            Box box = other.GetComponent<Box>();
            if (box != null)
            {
                box.DestroyBox();
                GameOver();
            }
        }

        if (other.CompareTag("End"))
        {
            Debug.Log("End detected");
            End end = other.GetComponent<End>();
            if (end != null)
            {
                end.DestroyEnd();
            }
        }

        if (other.CompareTag("Good People"))
        {
            Debug.Log("Good People detected");
            GoodPeople goodPeople = other.GetComponent<GoodPeople>();
            if (goodPeople != null)
            {
                goodPeople.DestroyGoodPeople();
                GameOver();
            }
        }

        if (other.CompareTag("BadPeople"))
        {
            Debug.Log("Bad People detected");
            BadPeople badPeople = other.GetComponent<BadPeople>();
            if(badPeople != null)
            {
                badPeople.DestroyBadPeople();
                FindObjectOfType<GameManager>().BadPeoplezero();
                FindObjectOfType<End>().OpenColor();
            }
        }

    }
    public void GameOver()
    {
        // 这里可以扩展游戏失败的逻辑，比如显示失败消息、播放失败音效等
        Debug.Log("Game Over! You have failed.");

        // 重新加载当前场景以重启关卡
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
