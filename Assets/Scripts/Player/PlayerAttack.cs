using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float damage;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float attackRange;
    [SerializeField] private AudioClip attackSound;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private AudioSource audioSource;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) /*&& cooldownTimer >= attackCoolDown && playerMovement.canAttack()*/)
            Attack1();
        else if (Input.GetMouseButtonUp(0))
            anim.SetBool("Attack1", false);

        cooldownTimer = 0;
    }

    private void Attack1()
    {
        // anim.CrossFade
        
        anim.SetBool("Attack1",true);
        cooldownTimer = 0;
       
        StartCoroutine(AttackCoroutine());
        audioSource.PlayOneShot(attackSound, 0.3f);
    }
    private void VurDamage()
    {
        Vector2 attackDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D[] hitEnemies = Physics2D.RaycastAll(transform.position, attackDirection, attackRange, enemyLayer);
        foreach (RaycastHit2D hitEnemy in hitEnemies)
        {
            if(!hitEnemy.transform.CompareTag("Player"))
            hitEnemy.collider.GetComponent<Health>()?.TakeDamage(damage);
        }
    }
    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("Attack1", false);
    }
    private void OnDrawGizmosSelected()
    {
        Vector2 attackDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Gizmos.DrawRay(transform.position, attackDirection * attackRange);
    }

    private void hitEnemy()
    {
        
    }

}
