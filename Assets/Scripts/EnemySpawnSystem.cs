using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnSystem : MonoBehaviour
{
    public static EnemySpawnSystem instance;
    GameObject player;
    [SerializeField] GameObject[] EnemyTypes;
    public int maxNumberOfEnemies;
    public int currentNumberOfEnemies = 0;
    [SerializeField] LayerMask PlayerAndEnemies;
    public bool spawning = true;

    private float timeBetweenSpawns = 5;
    public float decreaseTimeBetweenSpawnsBy;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = PlayerInstanceHandler.Instance.gameObject;
        ObjectPooler.instance.AddGameObjects("Enemy", maxNumberOfEnemies);
        //SpawnEnemy();
        //StartCoroutine(SpawnEnemy2());
    }

    private void FixedUpdate()
    {
        //timeBetweenSpawns -= Time.deltaTime;
    }

    public void SpawnEnemy()
    {
        //float xPos = Random.Range(-40f, 40f);
        //float zPoa = Random.Range(-40f, 40f);
        //float yPos = 0.5f;

        //Vector3 spawnPos = new Vector3(xPos, yPos, zPoa);

        float r = 20f;
        float angle = Random.Range(0, 180) - 90 + PlayerInstanceHandler.Instance.transform.eulerAngles.y;
        //float angle = Random.Range(0, Mathf.PI) + PlayerInstanceHandler.Instance.transform.rotation.y * Mathf.Deg2Rad - 90 * Mathf.Deg2Rad;
        Vector2 pos = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad) * r, Mathf.Cos(angle * Mathf.Deg2Rad) * r);
        Vector3 spawnPos = PlayerInstanceHandler.Instance.transform.position + new Vector3(pos.x, -0.99f - 0.5f, pos.y);

        if (Physics.OverlapSphere(spawnPos, 0.7f, PlayerAndEnemies).Length == 0)
        {
            GameObject go = ObjectPooler.instance.SpawnFromPool("Enemy", spawnPos, Quaternion.identity);
            go.GetComponent<MeshRenderer>().material.color = Color.blue;
            go.GetComponent<TrailRenderer>().material.color = Color.blue;
            go.GetComponent<TrailRenderer>().time = 1;
            go.GetComponent<NavMeshAgent>().speed = 4;
            go.GetComponent<EnemyMovement>().exploding = false;
            currentNumberOfEnemies++;
            if (currentNumberOfEnemies < maxNumberOfEnemies)
            {
                Invoke("SpawnEnemy", timeBetweenSpawns);
                timeBetweenSpawns -= decreaseTimeBetweenSpawnsBy;
            }
            else
            {
                spawning = false;
            }
        }
        else
        {
            if (currentNumberOfEnemies < maxNumberOfEnemies)
            {
                Invoke("SpawnEnemy", timeBetweenSpawns/2);
                timeBetweenSpawns -= decreaseTimeBetweenSpawnsBy;
            }
            else
            {
                spawning = false;
            }
        }
    }

    public void EnemyNumberDecreased()
    {
        currentNumberOfEnemies--;
        if (!spawning)
        {
            spawning = true;
            Invoke("SpawnEnemy", timeBetweenSpawns);
            timeBetweenSpawns -= decreaseTimeBetweenSpawnsBy;
        }
    }

    //public static IEnumerator SpawnEnemy2(Transform parentObject, LayerMask layer, float delay, float duration)
    //{
    //    yield return new WaitForSeconds(delay);
    //    for (int i = 0; i < (duration/Time.fixedDeltaTime); i++)
    //    {
    //        yield return new WaitForFixedUpdate();
    //        float r = 20f;
    //        float angle = Random.Range(0, 180) - 90 + PlayerInstanceHandler.Instance.transform.eulerAngles.y;
    //        //float angle = Random.Range(0, Mathf.PI) + PlayerInstanceHandler.Instance.transform.rotation.y * Mathf.Deg2Rad - 90 * Mathf.Deg2Rad;
    //        Vector2 pos = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad) * r, Mathf.Cos(angle * Mathf.Deg2Rad) * r);
    //        Vector3 spawnPos = PlayerInstanceHandler.Instance.transform.position + new Vector3(pos.x, -0.99f - 0.5f, pos.y);

    //        if (Physics.OverlapSphere(spawnPos, 0.7f, layer).Length == 0)
    //        {
    //            GameObject go = ObjectPooler.instance.SpawnFromPool("Enemy", spawnPos, Quaternion.identity);
    //            //GameObject go = Instantiate(holePrefab, spawnPos, Quaternion.identity);
    //            go.transform.parent = parentObject;
    //            EnemySpawnSystem.instance.EnemyNumberIncreased();
    //        }
    //    }  
    //}
}
