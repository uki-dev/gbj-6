using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MeleeEnemy
{
  void OnCollisionStay2D(Collision2D collision)
  {
    Player player = collision.collider.GetComponent<Player>();
    if (player)
    {
      player.Damage(collisionDamage, gameObject, knockback);
      Destroy(gameObject);
    }
  }
}
