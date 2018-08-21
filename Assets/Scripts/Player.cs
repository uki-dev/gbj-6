using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
  public float attackSpeed = 1f;
  public float attackCooldown = 1f;
  public bool canAttack = true;
  bool isAttacking;

  void Update()
  {
    if (!isAttacking)
      Move();

    if (Input.GetKeyDown(KeyCode.Z) && canAttack)
      StartCoroutine(Attack());
  }

  IEnumerator Attack()
  {
    Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
    rigidbody.velocity = Vector3.zero;

    Animator animator = GetComponent<Animator>();
    animator.SetFloat("Attack Speed", 1f / attackSpeed);
    animator.SetTrigger("Attacking");

    isAttacking = true;
    canAttack = false;
    yield return new WaitForSeconds(attackSpeed);
    isAttacking = false;
    yield return new WaitForSeconds(attackCooldown - attackSpeed);
    canAttack = true;
  }

  void Move()
  {
    Animator animator = GetComponent<Animator>();
    Vector2 direction = Vector2.zero;
    if (Input.GetKey(KeyCode.UpArrow))
      direction.y += 1;
    if (Input.GetKey(KeyCode.DownArrow))
      direction.y -= 1;
    if (Input.GetKey(KeyCode.LeftArrow))
      direction.x -= 1;
    if (Input.GetKey(KeyCode.RightArrow))
      direction.x += 1;

    Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
    rigidbody.velocity = direction * speed;

    bool running = direction != Vector2.zero;
    animator.SetBool("Running", running);
    if (running)
    {
      animator.SetFloat("Run Speed", speed / 32);
      animator.SetFloat("Direction X", direction.x);
      animator.SetFloat("Direction Y", direction.y);

      SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
      spriteRenderer.flipX = direction.x < 0;
    }
  }
}
