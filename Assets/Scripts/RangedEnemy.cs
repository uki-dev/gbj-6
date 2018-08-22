using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
  public GameObject projectile;
  public float projectileSpeed;

  protected override IEnumerator Attack()
  {
    if (this.projectile)
    {
      Vector2 normal = (target.transform.position - transform.position).normalized;
      Vector2 direction = Utility.Direction(normal);
      GameObject gameObject = Instantiate(this.projectile, transform.position, Quaternion.identity);
      gameObject.tag = this.gameObject.tag;
      gameObject.transform.up = direction;
      Projectile projectile = gameObject.GetComponent<Projectile>();
      projectile.damage = attackDamage;
      Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
      rigidbody.velocity = direction * projectileSpeed;
    }
    yield return StartCoroutine(base.Attack());
  }
}
