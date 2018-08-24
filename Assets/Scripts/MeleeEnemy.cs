using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
  protected override void Update()
  {
    Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
    rigidbody.velocity = Vector2.zero;
    if (Player.instance && !Player.instance.dead)
    {
      Vector2 normal = (Player.instance.transform.position - transform.position).normalized;
      direction = Utility.Direction(normal);
      rigidbody.velocity = direction * walkSpeed;
    }
    else
    {
      Animator animator = GetComponent<Animator>();
      animator.StopPlayback();
    }
    base.Update();
  }
}
