using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

public class GameManager : MonoBehaviour
{
    public int totalBoxs;
    public int finishedBoxs;
    public bool destination = false;
    private GameObject endpoint;
    public int totalBadPeople;    
    public int finishBadPeople;
    public bool BadGuys = false;
    public bool isPaused = false;
    public bool fail = false;
    
    public void Start()
    {
        endpoint = GameObject.Find("End");
            if (endpoint != null)
            {
                if(finishedBoxs != totalBoxs)
                // 如果找到了，隐藏这个物体
                endpoint.SetActive(false);
            }
            else
            {
                Debug.LogError("未能找到名为 'End' 的物体！");
            }
            
            finishBadPeople = totalBadPeople;
            Debug.Log("finish people" + finishBadPeople);

            BadPeoplezero();
            FindObjectOfType<End>().OpenColor();
    }
    
    private void Update()
    {
    if (Input.GetKeyDown(KeyCode.R))
    {
        ResetStage(); // 游戏恢复后重置阶段
    }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadPreviousLevel();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadNextLevel();
        }
        FindObjectOfType<End>().OpenColor();
    }

    public void CheckFinish()
    {
        Boxdisplay();
        if((destination == true) && (BadGuys == true)){
            print("YOU WIN!");
            StartCoroutine(LoadNextStage());
        }
    }

    public void ResetStage()
    {
        SetPause(false);
        fail = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        FindObjectOfType<MenuController>().FailUI.SetActive(false);
    }

    IEnumerator LoadNextStage()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BadPeoplezero(){
        if (finishBadPeople == 0){
            BadGuys = true;
        }else{
            BadGuys = false;
        }

    }

    public void Boxdisplay(){
        if (finishedBoxs == totalBoxs)
        {
            endpoint.SetActive(true);
        }else{
            endpoint.SetActive(false);
        }
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; // Optionally wrap around to the first scene
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void LoadPreviousLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int previousSceneIndex = currentSceneIndex - 1;
        if (previousSceneIndex < 0)
        {
            previousSceneIndex = SceneManager.sceneCountInBuildSettings - 1; // Optionally wrap around to the last scene
        }
        SceneManager.LoadScene(previousSceneIndex);
    }

    public void SetPause(bool pauseStatus)
    {
        isPaused = pauseStatus;

        // 设置时间流逝速度
        Time.timeScale = isPaused ? 0 : 1;

        // 可以在这里加入其他对游戏暂停状态的处理
        // 例如，显示暂停菜单，禁用某些游戏输入等
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}
