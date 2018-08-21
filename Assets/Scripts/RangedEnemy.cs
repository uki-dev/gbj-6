using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
  public Projectile projectile;
  public float projectileSpeed;

  protected override IEnumerator Attack()
  {
    GameObject gameObject = Instantiate(this.projectile.gameObject);
    Projectile projectile = gameObject.GetComponent<Projectile>();
    projectile.parent = this;
    Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
    rigidbody.velocity = (target.position - transform.position).normalized * projectileSpeed;
    yield return StartCoroutine(base.Attack());
  }
}
