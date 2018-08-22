using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
  public int health;

  public virtual void Damage(int amount)
  {
    health = Mathf.Clamp(health - amount, 0, int.MaxValue);
    if (health <= 0)
      Die();
  }

  public virtual void Die()
  {
    Destroy(gameObject);
  }
}
