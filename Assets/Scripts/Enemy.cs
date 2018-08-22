﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
  [HideInInspector]
  public Player target;
  public float range;

  public GameObject gold;
  public GameObject[] bones;

  void Start()
  {
    target = FindObjectOfType<Player>();
  }

  protected override void Update()
  {
    walking = false;
    if (target)
    {
      if (Vector3.Distance(transform.position, target.transform.position) > range)
      {
        Vector2 normal = (target.transform.position - transform.position).normalized;
        direction = Utility.Direction(normal);
        walking = true;
      }
      else if (canAttack)
      {
        StartCoroutine(Attack());
      }
    }
    else
    {
      Animator animator = GetComponent<Animator>();
      animator.StopPlayback();
    }
    base.Update();
  }

  public override void Die()
  {
    base.Die();
    if (gold)
    {
      /*
      SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
      position.x += Mathf.Round((Random.value - 0.5f) * 8);
      position.y -= spriteRenderer.bounds.extents.y - 2;
      */
      Vector3 position = transform.position;
      GameObject gold = Instantiate(this.gold, position, Quaternion.identity);
    }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.collider.tag != gameObject.tag)
    {
      Entity entity = collision.collider.GetComponent<Entity>();
      if (entity)
      {
        entity.Damage(attackDamage);
        Rigidbody2D rigidbody = entity.GetComponent<Rigidbody2D>();
        Vector2 normal = (entity.transform.position - transform.position).normalized;
        rigidbody.MovePosition(rigidbody.position + normal * knockback);
      }
    }
  }
}
