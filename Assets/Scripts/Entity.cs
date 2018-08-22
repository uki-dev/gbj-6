using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
  const float corpseDelay = 5f;

  public int health;

  public float walkSpeed;

  public int attackDamage;
  public float attackSpeed;
  public float attackCooldown;

  public float knockback;

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
    Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
    rigidbody.velocity = Vector2.zero;

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
    rigidbody.velocity = direction * walkSpeed;
    //rigidbody.MovePosition(rigidbody.position + direction * walkSpeed * Time.deltaTime);
    //rigidbody.AddForce(direction * walkSpeed, ForceMode2D.Impulse);

    Animator animator = GetComponent<Animator>();
    bool walking = direction != Vector2.zero;
    animator.SetBool("Walking", walking);
    if (walking)
    {
      animator.SetFloat("Walk Speed", walkSpeed / 32);
      animator.SetFloat("Direction X", direction.x);
      animator.SetFloat("Direction Y", direction.y);

      transform.localScale = new Vector3(direction.x > 0 ? 1f : -1f, 1f, 1f);
    }
  }
  public void Damage(int amount, Entity entity)
  {
    health = Mathf.Clamp(health - amount, 0, int.MaxValue);
    if (health <= 0)
      Die();

    Animator animator = GetComponent<Animator>();
    animator.SetTrigger("Damaged");

    Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
    Vector2 normal = (transform.position - entity.transform.position).normalized;
    rigidbody.MovePosition(rigidbody.position + normal * entity.knockback);
  }

  public virtual void Die()
  {
    Destroy(gameObject);
  }
}
