using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
  [HideInInspector]
  public float range;

  public int goldAmount;
  public GameObject goldPrefab;

  public static List<Enemy> enemies = new List<Enemy>();

  protected override void Start()
  {
    enemies.Add(this);
    base.Start();
  }

  void OnDestroy()
  {
    enemies.Remove(this);
  }

  protected override void Update()
  {
    Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
    rigidbody.velocity = Vector2.zero;
    if (Player.instance && !Player.instance.dead)
    {
      if (Vector3.Distance(transform.position, Player.instance.transform.position) > range)
      {
        Vector2 normal = (Player.instance.transform.position - transform.position).normalized;
        direction = Utility.Direction(normal);
        rigidbody.velocity = direction * walkSpeed;
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

  protected override void Die()
  {
    base.Die();
    if (goldAmount > 0 && goldPrefab)
    {
      Vector3 position = transform.position;
      GameObject goldObject = Instantiate(this.goldPrefab, position, Quaternion.identity);
      Gold gold = goldObject.GetComponent<Gold>();
      gold.amount = goldAmount;
    }
  }

  void OnCollisionStay2D(Collision2D collision)
  {
    Player player = collision.collider.GetComponent<Player>();
    if (player)
    {
      player.Damage(attackDamage, gameObject, knockback);
    }
  }
}
