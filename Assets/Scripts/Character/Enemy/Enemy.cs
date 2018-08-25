using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
  public int collisionDamage;

  public int goldAmount;
  public GameObject goldPrefab;

  public AudioSource damagedSound;

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

  public override void Damage(int amount, GameObject initiator, float knockback)
  {
    damagedSound.Play();
    base.Damage(amount, initiator, knockback);
  }

  protected override void Die()
  {
    Destroy(GetComponent<Collider2D>());
    Destroy(GetComponent<Rigidbody2D>());
    Destroy(gameObject, damagedSound.clip.length);
    enabled = false;

    if (goldAmount > 0)
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
      player.Damage(collisionDamage, gameObject, knockback);
    }
  }
}
