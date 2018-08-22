using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
  public Entity parent;

  void OnTriggerEnter2D(Collider2D collider)
  {
    Debug.Log(collider);
    if (collider.tag != parent.gameObject.tag)
    {
      Entity entity = collider.GetComponent<Entity>();
      if (entity)
        entity.Damage(parent.attackDamage, parent);
    }
  }
}
