using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
  public int health;
  public int healthMax;

  public bool dead
  {
    get
    {
      return health <= 0;
    }
  }

  public float walkSpeed;

  public int attackDamage;
  public float attackSpeed;
  public float attackCooldown;

  public float knockback;

  [HideInInspector]
  public Vector2 direction = Vector2.down;

  [HideInInspector]
  public bool attacking;
  [HideInInspector]
  public bool canAttack = true;

  protected virtual void Start()
  {
    health = healthMax;
  }

  protected override void Update()
  {
    Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

    Animator animator = GetComponent<Animator>();
    animator.SetBool("Walking", rigidbody.velocity != Vector2.zero);
    animator.SetFloat("Walk Speed", rigidbody.velocity.magnitude / 32);
    animator.SetFloat("Direction X", direction.x);
    animator.SetFloat("Direction Y", direction.y);
    animator.SetBool("Dead", dead);

    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.flipX = direction.x < 0;

    base.Update();
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

  public virtual void Damage(int amount, GameObject initiator, float knockback)
  {
    health = Mathf.Clamp(health - amount, 0, healthMax);
    if (health == 0)
    {
      Die();
    }

    Animator animator = GetComponent<Animator>();
    animator.SetTrigger("Damaged");

    Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
    Vector2 normal = (transform.position - initiator.transform.position).normalized;
    rigidbody.MovePosition(rigidbody.position + normal * knockback);
  }

  protected virtual void Die()
  {
    Destroy(gameObject);
  }
}
