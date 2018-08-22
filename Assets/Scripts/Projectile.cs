using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  public int damage;

  void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.tag != gameObject.tag)
    {
      Entity entity = collider.GetComponent<Entity>();
      if (entity)
      {
        entity.Damage(damage);
      }
    }
  }
}
