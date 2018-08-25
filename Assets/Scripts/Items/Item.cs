using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
  public enum Type { Health, Damage, Speed, Potion }
  public Type type;
  public float amount;
  public int cost;

  Player player;
  bool triggered;

  void Update()
  {
    if (player)
    {
      if (Input.GetButtonDown("A"))
      {
        Purchase(player);
      }
    }
  }

  public void Purchase(Player player)
  {
    if (player.gold >= cost && !(type == Type.Potion && player.health == player.healthMax))
    {
      player.gold -= cost;
      switch (type)
      {
        case Type.Health:
          player.healthMax += (int)amount;
          player.health += (int)amount;
          break;
        case Type.Damage:
          player.attackDamage = (int)amount;
          break;
        case Type.Speed:
          player.attackSpeed *= 1f - amount;
          player.attackCooldown *= 1f - amount;
          break;
        case Type.Potion:
          player.health = player.healthMax;
          break;
      }
      GetComponent<AudioSource>().Play();
    }
  }

  void OnTriggerStay2D(Collider2D collider)
  {
    Player player = collider.GetComponent<Player>();
    if (player)
    {
      this.player = player;
      player.canAttack = false;
    }
  }

  void OnTriggerExit2D(Collider2D collider)
  {
    Player player = collider.GetComponent<Player>();
    if (player && player == this.player)
    {
      // this happens before player update ??
      player.canAttack = true;
      this.player = null;
    }
  }
}
