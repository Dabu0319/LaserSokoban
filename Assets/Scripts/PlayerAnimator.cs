using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator anim;
    public PlayerController playerController;

    private void Awake(){
        anim = GetComponent<Animator>();
        playerController.OnMoveSuccess += UpdateAnimationDirection;

    }

    private void UpdateAnimationDirection(Vector2 direction)
    {
        // 重置所有方向的动画
        anim.SetBool("MoveRight", false);
        anim.SetBool("MoveLeft", false);
        anim.SetBool("MoveUp", false);
        anim.SetBool("MoveDown", false);

        // 根据移动方向设置对应的布尔值
        if (direction == Vector2.right)
            anim.SetBool("MoveRight", true);
        else if (direction == Vector2.left)
            anim.SetBool("MoveLeft", true);
        else if (direction == Vector2.up)
            anim.SetBool("MoveUp", true);
        else if (direction == Vector2.down)
            anim.SetBool("MoveDown", true);
    }
     private void OnDestroy()
    {
        // 取消订阅事件
        playerController.OnMoveSuccess -= UpdateAnimationDirection;
    }
}
