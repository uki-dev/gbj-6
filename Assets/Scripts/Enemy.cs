using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
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
}
