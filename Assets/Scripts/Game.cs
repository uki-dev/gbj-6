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

  public int wave;
  public float waveTimer;

  public Spawn[] spawns;

  public BoxCollider2D[] spawnAreas;

  public int enemyCountBase;
  public int enemyCountMax;
  public float enemyCountScale;
  public float enemyHealthScale;
  public float enemyDamageScale;

  public bool shopping
  {
    get
    {
      return _shopping;
    }
    set
    {
      _shopping = value;
      staircase.opened = value;
    }
  }
  bool _shopping;
  public Staircase staircase;

  IEnumerator Start()
  {
    while (true)
    {
      if (shopping)
      {
        yield return null;
      }
      else
      {
        yield return StartCoroutine(Wave());
        wave++;
        if (wave > 0 && wave % 5 == 0)
        {
          shopping = true;
        }
      }
    }
  }

  IEnumerator Wave()
  {
    yield return new WaitForSeconds(waveTimer);

    int enemyCount = Mathf.FloorToInt(enemyCountBase * Mathf.Pow(enemyCountScale, wave));
    Enemy[] enemies = new Enemy[enemyCount];
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
      enemies[i] = enemy;
    }

    while (true)
    {
      bool end = true;
      foreach (Enemy enemy in enemies)
      {
        if (enemy) end = false;
      }
      if (end) break;
      yield return null;
    }
    //yield return new WaitForSeconds(waveTimer);
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
      {
        return spawn.enemy;
      }
      else
      {
        value -= spawn.probability;
      }
    }
    return null;
  }

}
