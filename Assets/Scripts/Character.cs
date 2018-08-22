using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
  public float walkSpeed;

  public int attackDamage;
  public float attackSpeed;
  public float attackCooldown;

  public float knockback;

  protected bool walking;
  protected Vector2 direction;

  protected bool attacking;
  protected bool canAttack = true;

  protected virtual void Update()
  {
    Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
    if (walking)
      rigidbody.velocity = direction * walkSpeed;
    else
      rigidbody.velocity = Vector2.zero;

    Animator animator = GetComponent<Animator>();
    animator.SetBool("Walking", walking);
    animator.SetFloat("Walk Speed", walkSpeed / 32);
    animator.SetFloat("Direction X", direction.x);
    animator.SetFloat("Direction Y", direction.y);
    transform.localScale = new Vector3(direction.x > 0 ? 1f : -1f, 1f, 1f);
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

  public override void Damage(int amount)
  {
    base.Damage(amount);

    if (amount > 0)
    {
      Animator animator = GetComponent<Animator>();
      animator.SetTrigger("Damaged");
    }
  }
}
