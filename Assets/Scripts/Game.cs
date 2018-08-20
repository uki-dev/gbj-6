using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
  [System.Serializable]
  public struct Spawn
  {
    public Enemy enemy;
    public float probability;
  }

  public Spawn[] spawns;

  public BoxCollider2D[] spawnAreas;

  public int enemyCountBase;
  public int enemyCountMax;
  public float enemyCountScale;
  public float enemyHealthScale;
  public float enemyDamageScale;

  public int wave;

  void Start()
  {
    StartWave();
  }
  void StartWave()
  {
    int enemyCount = Mathf.FloorToInt(enemyCountBase * Mathf.Pow(enemyCountScale, wave));
    for (int i = 0; i < enemyCount; i++)
    {
      Enemy enemy = Instantiate(RandomEnemy().gameObject).GetComponent<Enemy>();
      BoxCollider2D spawnArea = spawnAreas[Random.Range(0, spawnAreas.Length)];
      // try to spawn somewhere in the area
      while (true)
      {
        Vector2 offset = new Vector2(spawnArea.size.x * Random.value, spawnArea.size.y * Random.value) - spawnArea.size * 0.5f;
        Vector3 position = spawnArea.transform.position + (Vector3)offset;
        //position.x = Mathf.RoundToInt(position.x / 8) * 8;
        //position.y = Mathf.RoundToInt(position.y / 8) * 8;
        if (!Physics2D.OverlapBox((Vector2)position + new Vector2(4, -4), new Vector2(8, 8), 0f, ~LayerMask.GetMask("Ignore Raycast")))
        {
          enemy.transform.position = position;
          break;
        }
      }

      enemy.target = FindObjectOfType<Player>().transform;
    }
  }

  Enemy RandomEnemy()
  {
    float sum = 0;
    foreach (Spawn spawn in spawns)
    {
      sum += spawn.probability;
    }

    float value = Random.value * sum;
    foreach (Spawn spawn in spawns)
    {
      if (value <= spawn.probability)
        return spawn.enemy;
      else
        value -= spawn.probability;
    }
    return null;
  }

}
