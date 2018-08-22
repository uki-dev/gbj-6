using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
  public GameObject[] bones;

  [HideInInspector]
  public Transform target;
  public float range;

  void Update()
  {
    Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
    if (target)
    {
      if (Vector3.Distance(transform.position, target.position) > range)
        Walk((target.position - transform.position).normalized);
      else
        Attack();
    }
  }

  public override void Die()
  {
    base.Die();
    if (bones.Length > 0)
    {
      GameObject prefab = this.bones[Random.Range(0, this.bones.Length)];
      GameObject bones = Instantiate(prefab, transform.position, Quaternion.identity);
      // Destroy(bones, boneDecay);
    }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.collider.tag != gameObject.tag)
    {
      Entity entity = collision.collider.GetComponent<Entity>();
      if (entity)
        entity.Damage(attackDamage, this);
    }
  }
}
