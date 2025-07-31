using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerMovement movement;

    private Animator animator;
    private CharacterController cc;
    private InputManager input;

    private bool wasAttacking = false;
    private bool isAttacking = false;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        input = GetComponent<InputManager>();
    }

    void FixedUpdate()
    {
        isAttacking = input.BasicAttackValue > 0 || input.HeavyAttackValue > 0;

        if (input.BasicAttackValue > 0)
        {
            BasicAttack();
            //if (animator.GetBool("sprint") && cc.isGrounded)
            //{
            //    SprintAttack();
            //}
            //else if (!animator.GetBool("sprint") && cc.isGrounded)
            //{
            //    BasicAttack();
            //}
        }

        if (input.HeavyAttackValue > 0)
        {
            HeavyAttack();
            //if (animator.GetBool("sprint") && cc.isGrounded)
            //{
            //    SprintHeavyAttack();
            //}
            //else if (!animator.GetBool("sprint") && cc.isGrounded)
            //{
            //    HeavyAttack();
            //}
        }

        wasAttacking = isAttacking;
    }

    void BasicAttack() 
    {
        if (isAttacking && !wasAttacking && cc.isGrounded && !animator.GetBool("sprint"))
        {
            animator.SetTrigger("BasicAttack");
        }
    }

    void HeavyAttack() 
    {
        if (isAttacking && !wasAttacking && cc.isGrounded && !animator.GetBool("sprint"))
        {
            animator.SetTrigger("HeavyAttack");
        }
    }

    //void SprintAttack()
    //{

    //}

    //void SprintHeavyAttack()
    //{

    //}
}
