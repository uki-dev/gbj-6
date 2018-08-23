using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
  [HideInInspector]
  public Player target;
  public float range;

  public int goldAmount;
  public GameObject goldPrefab;

  public static List<Enemy> enemies = new List<Enemy>();

  protected override void Start()
  {
    enemies.Add(this);
    target = Player.instance;
    base.Start();
  }

  void OnDestroy()
  {
    enemies.Remove(this);
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

  void OnCollisionEnter2D(Collision2D collision)
  {
    Player player = collision.collider.GetComponent<Player>();
    if (player)
    {
      player.Damage(attackDamage);
      Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();
      Vector2 normal = (player.transform.position - transform.position).normalized;
      rigidbody.MovePosition(rigidbody.position + normal * knockback);
    }
  }
}
