using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
  public int health;

  public float walkSpeed;

  public int attackDamage;
  public float attackSpeed;
  public float attackCooldown;

  protected bool canAttack = true;
  protected bool attacking;

  void Awake()
  {
    Animator animator = GetComponent<Animator>();
    animator.SetFloat("Direction Y", -1);
    Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
    rigidbody.gravityScale = 0;
    rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
  }

  protected virtual IEnumerator Attack()
  {
    Animator animator = GetComponent<Animator>();
    animator.SetFloat("Attack Speed", 1f / attackSpeed);
    animator.SetTrigger("Attacking");

    attacking = true;
    canAttack = false;
    yield return new WaitForSeconds(attackSpeed);
    attacking = false;
    yield return new WaitForSeconds(attackCooldown - attackSpeed);
    canAttack = true;
  }

  protected void Walk(Vector2 direction)
  {
    Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
    rigidbody.MovePosition(rigidbody.position + direction * walkSpeed * Time.deltaTime);

    Animator animator = GetComponent<Animator>();
    bool walking = direction != Vector2.zero;
    animator.SetBool("Walking", walking);
    if (walking)
    {
      animator.SetFloat("Walk Speed", walkSpeed / 32);
      animator.SetFloat("Direction X", direction.x);
      animator.SetFloat("Direction Y", direction.y);

      SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
      spriteRenderer.flipX = direction.x < 0;
    }
  }
}
