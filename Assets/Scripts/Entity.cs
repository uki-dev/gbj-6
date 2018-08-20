using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
  public int health;
  public int damage;
  public float speed;

  void Awake()
  {
    Animator animator = GetComponent<Animator>();
    animator.SetFloat("Direction Y", -1);
    Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
    rigidbody.gravityScale = 0;
    rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
  }

  protected void Update()
  {
    Animator animator = GetComponent<Animator>();
    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

    Vector2 direction = rigidbody.velocity.normalized;

  }
}
