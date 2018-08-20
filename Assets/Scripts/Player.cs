using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
  void Update()
  {
    Vector2 direction = Vector2.zero;
    if (Input.GetKey(KeyCode.UpArrow))
      direction.y += 1;
    if (Input.GetKey(KeyCode.DownArrow))
      direction.y -= 1;
    if (Input.GetKey(KeyCode.LeftArrow))
      direction.x -= 1;
    if (Input.GetKey(KeyCode.RightArrow))
      direction.x += 1;

    bool running = direction != Vector2.zero;
    Animator animator = GetComponent<Animator>();
    animator.SetBool("Running", running);
    if (running)
    {
      animator.SetFloat("Speed", speed / 32);
      animator.SetFloat("Direction X", direction.x);
      animator.SetFloat("Direction Y", direction.y);

      SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
      spriteRenderer.flipX = direction.x < 0;
    }

    Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
    rigidbody.velocity = direction * speed;
  }
}
