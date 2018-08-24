using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
  public static Player instance;

  public float dashSpeed;
  public float dashCooldown;
  float nextDash;

  public BoxCollider2D attackHitbox;

  public float invincibilityTime;
  [HideInInspector]
  public bool invincible;

  public int gold;

  private List<Enemy> hit = new List<Enemy>();

  void Awake()
  {
    instance = this;
  }

  protected override void Update()
  {
    Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
    rigidbody.velocity = Vector2.zero;
    if (!dead)
    {
      if (canAttack && Input.GetButtonDown("A"))
      {
        StartCoroutine(Attack());
      }
      if (!attacking)
      {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction = Utility.Direction(direction.normalized);
        if (direction != Vector2.zero)
        {
          if (Input.GetButtonDown("B") && Time.time > nextDash)
          {
            rigidbody.MovePosition(rigidbody.position + direction * dashSpeed);
            nextDash = Time.time + dashCooldown;
          }
          else
          {
            rigidbody.velocity = direction * walkSpeed;
          }
          this.direction = direction;
        }
      }
    }
    base.Update();
  }

  protected override IEnumerator Attack()
  {
    hit.Clear();
    attackHitbox.offset = direction * attackHitbox.size;
    yield return StartCoroutine(base.Attack());
  }

  public override void Damage(int damage, GameObject initiator, float knockback)
  {
    if (!invincible)
    {
      base.Damage(damage, initiator, knockback);
      StartCoroutine(Invincibility());
    }
  }

  protected virtual IEnumerator Invincibility()
  {
    invincible = true;
    yield return new WaitForSeconds(invincibilityTime);
    invincible = false;
  }

  protected override void Die()
  {
    Game.instance.GameOver();
  }

  void OnTriggerStay2D(Collider2D collider)
  {
    if (attacking)
    {
      Enemy enemy = collider.GetComponent<Enemy>();
      if (enemy && !hit.Contains(enemy))
      {
        enemy.Damage(attackDamage, gameObject, knockback);
        hit.Add(enemy);
      }
    }
  }
}
