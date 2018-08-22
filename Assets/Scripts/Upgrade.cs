using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
  public enum Type { Damage, Speed, Health }
  public Type type;

  public float amount;

  public void Purchase(Player player)
  {
    switch (type)
    {
      case Type.Damage:
        player.attackDamage += (int)amount;
        break;
      case Type.Speed:
        player.attackSpeed += amount;
        break;
      case Type.Health:
        player.health += (int)amount;
        break;
    }
  }

  void OnTriggerStay2D(Collider2D collider)
  {
    Player player = collider.GetComponent<Player>();
    if (player)
    {
      player.canAttack = false;
      if (Input.GetButtonDown("A"))
      {

      }
    }
  }
}
