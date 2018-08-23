using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
  public GameObject projectilePrefab;
  public float projectileSpeed;

  protected override IEnumerator Attack()
  {
    if (projectilePrefab)
    {
      Vector2 normal = (target.transform.position - transform.position).normalized;
      Vector2 direction = Utility.Direction(normal);
      GameObject projectileObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
      projectileObject.tag = gameObject.tag;
      projectileObject.transform.up = direction;
      Projectile projectile = projectileObject.GetComponent<Projectile>();
      projectile.damage = attackDamage;
      Rigidbody2D rigidbody = projectileObject.GetComponent<Rigidbody2D>();
      rigidbody.velocity = direction * projectileSpeed;
    }
    yield return StartCoroutine(base.Attack());
  }
}
