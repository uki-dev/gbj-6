using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Entity
{
  public int damage;
  public float knockback;

  void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.tag == "Level")
    {
      Destroy(gameObject);
    }
    Player player = collider.GetComponent<Player>();
    if (player)
    {
      player.Damage(damage, gameObject, knockback);
      Destroy(gameObject);
    }
  }
}
