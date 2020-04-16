using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomHoles2 : MonoBehaviour
{
    public GameObject holePrefab;

    public LayerMask HolesLayer;

    private Vector3 previousPlayerpos;

    void Start()
    {
        previousPlayerpos = PlayerInstanceHandler.Instance.transform.position;
        StartCoroutine(SpawnFirstWave2(transform,HolesLayer,3));
        //SpawnFirstWave();
        //ObjectPooler.instance.AddGameObjects("Hole", 100);
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(PlayerInstanceHandler.Instance.transform.position, previousPlayerpos) > 3f)
        {
            previousPlayerpos = PlayerInstanceHandler.Instance.transform.position;
            //SpawnHoles();
            StartCoroutine(SpawnHoles2(transform,HolesLayer,2));
        }
    }

    public static void SpawnFirstWave(Transform parentObject, LayerMask HolesLayer)
    {
        for (int i = 0; i < 140; i++)
        {
            Vector2 point = Random.insideUnitCircle * 5f;
            Vector3 spawnPos = new Vector3(point.x, 0.01f, point.y);
            if (Physics.OverlapSphere(spawnPos, 3, HolesLayer).Length == 0)
            {
                GameObject go = ObjectPooler.instance.SpawnFromPool("Hole", spawnPos, Quaternion.Euler(90,0,0));
                //GameObject go = Instantiate(holePrefab, spawnPos, Quaternion.identity);
                go.transform.parent = parentObject;
            }
        }
    }

    public IEnumerator SpawnFirstWave2(Transform parentObject, LayerMask HolesLayer , float duration)
    {
        for (int i = 0; i < (duration/Time.fixedDeltaTime); i++)
        {
            //yield return new WaitUntil(() => ObjectPooler.instance.IsReadyToSpawnAnotherHole());
            //yield return new WaitForSeconds(1);
            yield return new WaitForFixedUpdate();
            Vector2 point = Random.insideUnitCircle * 20f;
            Vector3 spawnPos = new Vector3(point.x, 0.01f, point.y);
            if (Physics.OverlapSphere(spawnPos, 3, HolesLayer).Length == 0)
            {
                GameObject go = ObjectPooler.instance.SpawnFromPool("Hole", spawnPos, Quaternion.Euler(90,0,0));
                //GameObject go = Instantiate(holePrefab, spawnPos, Quaternion.identity);
                go.transform.parent = parentObject;
            }
            //yield return new WaitUntil(ObjectPooler.instance.IsReadyToSpawnAnotherHole);
        }
    }

    private void SpawnHoles(Transform parentObject, LayerMask HolesLayer)
    {
        for (int i = 0; i < 100; i++)
        {
            float r = 20f;
            float angle = Random.Range(0, 180) - 90 + PlayerInstanceHandler.Instance.transform.eulerAngles.y;
            //float angle = Random.Range(0, Mathf.PI) + PlayerInstanceHandler.Instance.transform.rotation.y * Mathf.Deg2Rad - 90 * Mathf.Deg2Rad;
            Vector2 pos = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad) * r, Mathf.Cos(angle * Mathf.Deg2Rad) * r);
            Vector3 spawnPos = PlayerInstanceHandler.Instance.transform.position + new Vector3(pos.x, 0.01f - 0.5f, pos.y);

            if (Physics.OverlapSphere(spawnPos, 3f, HolesLayer).Length == 0)
            {
                GameObject go = ObjectPooler.instance.SpawnFromPool("Hole", spawnPos, Quaternion.Euler(90,0,0));
                //GameObject go = Instantiate(holePrefab, spawnPos, Quaternion.identity);
                go.transform.parent = parentObject;
            }
        }
    }

    public IEnumerator SpawnHoles2(Transform parentObject, LayerMask HolesLayer , float duration)
    {
        for (int i = 0; i < (duration/Time.fixedDeltaTime); i++)
        {
            yield return new WaitForFixedUpdate();
            float r = 20f;
            float angle = Random.Range(0, 180) - 90 + PlayerInstanceHandler.Instance.transform.eulerAngles.y;
            //float angle = Random.Range(0, Mathf.PI) + PlayerInstanceHandler.Instance.transform.rotation.y * Mathf.Deg2Rad - 90 * Mathf.Deg2Rad;
            Vector2 pos = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad) * r, Mathf.Cos(angle * Mathf.Deg2Rad) * r);
            Vector3 spawnPos = PlayerInstanceHandler.Instance.transform.position + new Vector3(pos.x, 0.01f - 0.5f, pos.y);

            if (Physics.OverlapSphere(spawnPos, 3f, HolesLayer).Length == 0)
            {
                GameObject go = ObjectPooler.instance.SpawnFromPool("Hole", spawnPos, Quaternion.Euler(90, 0, 0));
                //GameObject go = Instantiate(holePrefab, spawnPos, Quaternion.identity);
                go.transform.parent = parentObject;
            }
        }
    }

    bool PressedSpace()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
