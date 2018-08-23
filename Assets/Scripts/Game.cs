using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
  public static Game instance;

  [System.Serializable]
  public struct Spawn
  {
    public Enemy enemy;
    public float probability;
  }

  public int wave;
  public float waveTimer;
  public int shopWave;

  public Spawn[] spawns;

  public BoxCollider2D[] spawnAreas;

  public GameObject spawnEffectPrefab;

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
      staircase.open = value;
    }
  }
  [SerializeField]
  bool _shopping;
  public Staircase staircase;

  void Awake()
  {
    instance = this;
  }

  IEnumerator Start()
  {
    while (true)
    {
      yield return StartCoroutine(StartWave());
      if (wave > 0 && wave % shopWave == 0)
      {
        shopping = true;
        while (shopping)
        {
          yield return null;
        }
      }
      wave++;
    }
  }

  IEnumerator StartWave()
  {
    yield return new WaitForSeconds(waveTimer);

    int enemyCount = Mathf.FloorToInt(enemyCountBase * Mathf.Pow(enemyCountScale, wave));
    Enemy[] enemies = new Enemy[enemyCount];
    for (int i = 0; i < enemyCount; i++)
    {
      yield return StartCoroutine(SpawnEnemy());
    }

    while (Enemy.enemies.Count > 0)
    {
      yield return null;
    }
  }

  IEnumerator SpawnEnemy()
  {
    BoxCollider2D spawnArea = spawnAreas[Random.Range(0, spawnAreas.Length)];
    Vector2 spawnPosition = spawnArea.transform.position;
    spawnPosition.x += spawnArea.size.x * (Random.value - 0.5f);
    spawnPosition.y += spawnArea.size.y * (Random.value - 0.5f);

    GameObject spawnEffectObject = Instantiate(spawnEffectPrefab, spawnPosition, Quaternion.identity);
    Animator spawnEffectAnimator = spawnEffectObject.GetComponent<Animator>();
    yield return new WaitForSeconds(spawnEffectAnimator.GetCurrentAnimatorStateInfo(0).length);
    Destroy(spawnEffectObject);

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
        GameObject enemyObject = Instantiate(spawn.enemy.gameObject, spawnPosition, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        break;
      }
      else
      {
        value -= spawn.probability;
      }
    }
  }
}
