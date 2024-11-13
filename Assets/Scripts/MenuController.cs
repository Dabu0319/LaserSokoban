using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // 引入 UI 命名空间以使用 UI 组件

public class MenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject FailUI;
    // public GameObject settingsPanel; // 在 Inspector 中分配
    // public Slider volumeSlider; // 在 Inspector 中分配
    public void StartGame()
    {
        // 加载游戏主场景
        SceneManager.LoadScene("Level1.1");
        AudioManager audioManager = FindObjectOfType<AudioManager>();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    // 显示或隐藏设置面板
    // public void ToggleSettings()
    // {
    //     settingsPanel.SetActive(!settingsPanel.activeSelf);
    // }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuUI.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        FindObjectOfType<GameManager>().SetPause(false);
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        FindObjectOfType<GameManager>().SetPause(true);
    }

    public void RestartGame()
    {
        // 重新加载当前场景
        pauseMenuUI.SetActive(false);
        FindObjectOfType<GameManager>().SetPause(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        pauseMenuUI.SetActive(false);
        FindObjectOfType<GameManager>().SetPause(false);
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

public void FailGame()
{
    Debug.Log("FailGame method called");
    
    // 检查 FailUI 是否已经分配
    if (FailUI != null)
    {
        FailUI.SetActive(true); // 激活 FailUI 界面
    }
    else
    {
        Debug.LogError("FailUI is not assigned in the inspector!");
    }

    // 设置游戏为暂停状态
    FindObjectOfType<GameManager>().SetPause(true);
}

}