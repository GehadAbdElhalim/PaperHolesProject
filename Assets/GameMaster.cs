using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    public UnityEvent OnGameUpdate;
    public bool giveSecondChance = true;
    public float timeElapsed = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        timeElapsed = 0;
        GameMenusController.instance.SetLoadingCircleStartSize();
        StartCoroutine(GameMenusController.instance.ShrinkLoadingCircle(3, 1));
        StartCoroutine(EnablePlayerMovement(4));
        StartCoroutine(StartSpawningEnemies(4));
    }

    private void Update()
    {
        if (!GameMenusController.instance.paused && !GameMenusController.instance.isGameOver)
        {
            timeElapsed += Time.deltaTime;
        }
    }

    IEnumerator EnablePlayerMovement(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayerInstanceHandler.Instance.GetComponent<PlayerMovement>().enabled = true;
    }

    IEnumerator StartSpawningEnemies(float delay)
    {
        yield return new WaitForSeconds(delay);
        EnemySpawnSystem.instance.SpawnEnemy();
    }
}
