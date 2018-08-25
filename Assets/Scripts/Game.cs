using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
  public Text waveText;

  public Spawn[] spawns;

  public BoxCollider2D spawnArea;
  public int spawnRange;

  public GameObject spawnEffectPrefab;

  public int enemyCountBase;
  public int enemyCountMax;
  public float enemyCountScale;
  public float enemyHealthScale;
  public float enemyDamageScale;

  public float gameOverTime;
  public RectTransform gameOverText;

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

  void Update()
  {
    waveText.text = "Wave " + Game.instance.wave;
  }

  IEnumerator StartWave()
  {
    yield return new WaitForSeconds(waveTimer);

    int enemyCount = Mathf.Clamp(Mathf.FloorToInt(enemyCountBase * Mathf.Pow(enemyCountScale, wave)), 0, enemyCountMax);
    for (int i = 0; i < enemyCount; i++)
    {
      yield return StartCoroutine(StartSpawn());
    }

    while (Enemy.enemies.Count > 0)
    {
      yield return null;
    }
  }

  IEnumerator StartSpawn()
  {
    Vector2 spawnPosition;
    while (true)
    {
      spawnPosition = spawnArea.transform.position; ;
      spawnPosition.x += spawnArea.size.x * (Random.value - 0.5f);
      spawnPosition.y += spawnArea.size.y * (Random.value - 0.5f);
      if (Vector2.Distance(spawnPosition, Player.instance.transform.position) >= spawnRange)
      {

        break;
      }
    }

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
        SpawnEnemy(spawn.enemy.gameObject, spawnPosition);
        break;
      }
      else
      {
        value -= spawn.probability;
      }
    }
  }

  void SpawnEnemy(GameObject prefab, Vector2 position)
  {
    GameObject enemyObject = Instantiate(prefab, position, Quaternion.identity);
    Enemy enemy = enemyObject.GetComponent<Enemy>();
    enemy.healthMax = Mathf.FloorToInt(enemy.healthMax * Mathf.Pow(enemyHealthScale, wave));
    enemy.health = enemy.healthMax;
    enemy.collisionDamage = Mathf.FloorToInt(enemy.collisionDamage * Mathf.Pow(enemyDamageScale, wave));
    if (enemy.GetType() == typeof(RangedEnemy))
    {
      RangedEnemy rangedEnemy = (RangedEnemy)enemy;
      rangedEnemy.projectileDamage = Mathf.FloorToInt(rangedEnemy.projectileDamage * Mathf.Pow(enemyDamageScale, wave));
    }
  }

  public void GameOver()
  {
    StopAllCoroutines();
    StartCoroutine(IGameOver());
  }

  IEnumerator IGameOver()
  {
    gameOverText.gameObject.SetActive(true);
    StartCoroutine(DespawnEnemies());
    yield return new WaitForSeconds(gameOverTime);
    SceneManager.LoadScene("Title");
  }

  IEnumerator DespawnEnemies()
  {
    for (int i = Enemy.enemies.Count - 1; i >= 0; i--)
    {
      Enemy enemy = Enemy.enemies[i];
      Destroy(enemy.gameObject);
      GameObject spawnEffectObject = Instantiate(spawnEffectPrefab, enemy.transform.position, Quaternion.identity);
      Animator spawnEffectAnimator = spawnEffectObject.GetComponent<Animator>();
      yield return new WaitForSeconds(spawnEffectAnimator.GetCurrentAnimatorStateInfo(0).length);
      Destroy(spawnEffectObject);
    }
  }
}
