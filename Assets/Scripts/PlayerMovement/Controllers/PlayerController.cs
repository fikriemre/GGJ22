using System;
using Shinjingi;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public Jump jump;
    public Move move;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSizeMultiper = -1;
    private bool _disableInput = false;
    private bool isSpriteLeft = false;
    private static readonly int Idle = Animator.StringToHash("Idle");

    private void Update()
    {
        if (_disableInput)
            return;

        float moveInput = Input.GetAxis("Horizontal");
        if (moveInput == 0)
        {
            animator.SetBool(Idle, true);
        }
        else
        {
            animator.SetBool(Idle, false);
            if (moveInput > 0)
                animator.transform.localScale = new Vector3(-moveSizeMultiper, 1, 1);
            else
                animator.transform.localScale = new Vector3(moveSizeMultiper, 1, 1);
        }

        move.MoveInput(Input.GetAxis("Horizontal"));
        jump.JumpInput(Input.GetKey(KeyCode.Space));
    }

    public void SetEnableInput(bool enable)
    {
        _disableInput = !enable;
    }
}