using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  public int damage;

  void OnTriggerEnter2D(Collider2D collider)
  {
    Player player = collider.GetComponent<Player>();
    if (player)
    {
      player.Damage(damage, gameObject, 0f);
      Destroy(gameObject);
    }
  }
}
