using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
  public int gold;

  protected override void Update()
  {
    if (Input.GetKeyDown(KeyCode.Z) && canAttack)
    {
      StartCoroutine(Attack());
    }

    walking = false;
    if (!attacking)
    {
      Vector2 direction = Vector2.zero;
      if (Input.GetKey(KeyCode.UpArrow))
        direction.y += 1;
      if (Input.GetKey(KeyCode.DownArrow))
        direction.y -= 1;
      if (Input.GetKey(KeyCode.LeftArrow))
        direction.x -= 1;
      if (Input.GetKey(KeyCode.RightArrow))
        direction.x += 1;

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
    if (collider.tag != gameObject.tag)
    {
      Entity entity = collider.GetComponent<Entity>();
      if (entity)
      {
        entity.Damage(attackDamage);
        Rigidbody2D rigidbody = entity.GetComponent<Rigidbody2D>();
        Vector2 normal = (entity.transform.position - transform.position).normalized;
        rigidbody.MovePosition(rigidbody.position + normal * knockback);
      }
    }
  }
}
