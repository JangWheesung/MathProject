using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void MoveAnim(float num)
    {
        if (num == 1 || num == -1)
            anim.SetBool("Move", true);
        else
            anim.SetBool("Move", false);
    }

    public void JumpAnim(float num)
    {
        if(num != 0)
            anim.SetBool("Jump", true);
        else
            anim.SetBool("Jump", false);
    }
}
