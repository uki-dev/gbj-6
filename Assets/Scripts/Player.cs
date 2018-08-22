using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
  public int gold;

  protected override void Update()
  {
    if (canAttack && Input.GetButtonDown("A"))
    {
      StartCoroutine(Attack());
    }

    walking = false;
    if (!attacking)
    {
      Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
      direction.Normalize();
      if (direction != Vector2.zero)
      {
        this.direction = direction;
        walking = true;
      }
    }
    base.Update();
  }

  void OnTriggerEnter2D(Collider2D collider)
  {
    Enemy enemy = collider.GetComponent<Enemy>();
    if (enemy)
    {
      enemy.Damage(attackDamage);
      Rigidbody2D rigidbody = enemy.GetComponent<Rigidbody2D>();
      Vector2 normal = (enemy.transform.position - transform.position).normalized;
      rigidbody.MovePosition(rigidbody.position + normal * knockback);
    }
  }
}
