using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
  public float range;

  public int projectileCount;
  public int projectileDamage;
  public float projectileSpeed;
  public float projectileRangeMin;
  public float projectileRangeMax;
  public float projectileCooldown;
  public float projectileKnockback;
  public GameObject projectilePrefab;

  float nextProjectile;

  protected override void Update()
  {
    Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
    rigidbody.velocity = Vector2.zero;
    if (Player.instance && !Player.instance.dead)
    {
      float distance = Vector3.Distance(transform.position, Player.instance.transform.position);
      if (distance > range)
      {
        Vector2 normal = (Player.instance.transform.position - transform.position).normalized;
        direction = Utility.Direction(normal);
        rigidbody.velocity = direction * walkSpeed;
      }
      if (distance >= projectileRangeMin && distance <= projectileRangeMax && Time.time >= nextProjectile)
      {
        Vector2 normal = (Player.instance.transform.position - transform.position).normalized;
        Vector2 direction = Utility.Direction(normal);
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        if (projectileCount > 1)
        {
          for (int i = 0; i < projectileCount; i++)
          {
            SpawnProjectile(angle - 45 + 90 * (float)i / (projectileCount));
          }
        }
        else
        {
          SpawnProjectile(angle);
        }
        nextProjectile = Time.time + projectileCooldown;
      }
    }
    else
    {
      Animator animator = GetComponent<Animator>();
      animator.StopPlayback();
    }
    base.Update();
  }

  void SpawnProjectile(float angle)
  {
    Vector2 direction = Utility.Direction(Quaternion.Euler(0f, 0f, -angle) * Vector2.up);
    GameObject projectileObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    projectileObject.tag = gameObject.tag;
    projectileObject.transform.up = direction;
    Projectile projectile = projectileObject.GetComponent<Projectile>();
    projectile.damage = projectileDamage;
    projectile.knockback = projectileKnockback;
    Rigidbody2D rigidbody = projectileObject.GetComponent<Rigidbody2D>();
    rigidbody.velocity = direction * projectileSpeed;
  }
}
