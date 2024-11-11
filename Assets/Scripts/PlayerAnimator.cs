using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator anim;
    private GameManager gameManager;
    private void Awake(){
        anim = GetComponent<Animator>();
        gameManager= FindObjectOfType<GameManager>();
    }

    private void Update(){

        SetAnimation();
    }

    private void SetAnimation(){
        Debug.Log("Setting animation state to: " + gameManager.isPaused);
        anim.SetBool("Fail", gameManager.isPaused);
    }

}
