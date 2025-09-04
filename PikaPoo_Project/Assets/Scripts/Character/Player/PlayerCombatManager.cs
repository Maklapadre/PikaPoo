using System.Collections;
using UnityEngine;
using Interfaces;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    private CharacterController cc;
    private PlayerInputManager input;

    private bool wasAttacking = false;
    private bool isAttacking = false;

    [SerializeField] private float Damage;
    [SerializeField] private float DamageAfterTime; 
    [SerializeField] private float HeavyDamageAfterTime;
    private AttackArea attackArea;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        input = GetComponent<PlayerInputManager>();
    }

    void FixedUpdate()
    {
        isAttacking = input.BasicAttackValue > 0 || input.HeavyAttackValue > 0;

        if (input.BasicAttackValue > 0)
        {
            OnBasicAttack();
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
            OnHeavyAttack();
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

    void OnBasicAttack() 
    {
        if (isAttacking && !wasAttacking && cc.isGrounded && !animator.GetBool("sprint"))
        {
            animator.SetTrigger("BasicAttack");
            StartCoroutine("Hit", false);
        }
    }

    void OnHeavyAttack() 
    {
        if (isAttacking && !wasAttacking && cc.isGrounded && !animator.GetBool("sprint"))
        {
            animator.SetTrigger("HeavyAttack");
            StartCoroutine("Hit", true);
        }
    }

    //void SprintAttack()
    //{

    //}

    //void SprintHeavyAttack()
    //{

    //}

    private IEnumerator Hit(bool heavy)
    {
        yield return new WaitForSeconds(heavy ? HeavyDamageAfterTime : DamageAfterTime);
        
        attackArea = GetComponentInChildren<AttackArea>();
        if (attackArea != null)
        {
            foreach (IDamageable damageable in attackArea.DamageablesInRange)
            {
                if (damageable.IsAlive())
                {
                    damageable.TakeDamage(heavy ? Damage * 1.75f : Damage);
                }
            }
        }
    }
}
