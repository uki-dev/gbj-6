using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
  public static Player instance;

  public int attackDamage;
  public float attackSpeed;
  public float attackCooldown;
  public BoxCollider2D attackHitbox;

  public float dashSpeed;
  public float dashCooldown;
  float nextDash;

  public float invincibilityTime;
  [HideInInspector]
  public bool invincible;

  public int gold;

  public AudioSource dashSound;
  public AudioSource damagedSound;
  public AudioSource deathSound;

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
            dashSound.Play();
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

  IEnumerator Attack()
  {
    Animator animator = GetComponent<Animator>();
    animator.SetFloat("Attack Speed", 1f / attackSpeed);
    animator.SetTrigger("Attacking");

    attacking = true;
    canAttack = false;
    attackHitbox.offset = direction * attackHitbox.size;
    hit.Clear();

    yield return new WaitForSeconds(attackSpeed);
    attacking = false;

    yield return new WaitForSeconds(attackCooldown - attackSpeed);
    canAttack = true;
  }

  public override void Damage(int amount, GameObject initiator, float knockback)
  {
    if (amount > 0 && !invincible)
    {
      base.Damage(amount, initiator, knockback);
      damagedSound.Play();
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
