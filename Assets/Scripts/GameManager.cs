﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalBoxs;
    public int finishedBoxs;
    public bool destination = false;
    private GameObject endpoint;
    public int totalBadPeople;    
    public int finishBadPeople;
    public bool BadGays = false;

    public void Awake(){
        BadPeoplezero();
    }

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
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ResetStage();

    }

    public void CheckFinish()
    {
        End end =  gameObject.GetComponent<End>();
        if(finishedBoxs == totalBoxs)
        {
            endpoint.SetActive(true);
        }



        if((destination == true) && (BadGays == true)){
            print("YOU WIN!");
            // StartCoroutine(LoadNextStage());
        }
    }

    void ResetStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator LoadNextStage()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BadPeoplezero(){
        if (finishBadPeople == 0){
            BadGays = true;
        }

    }
}
