using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomHoles : MonoBehaviour
{
    public int holeCount;

    public LayerMask HolesLayer;

    public GameObject HolePrefab;

    private void Start()
    {
        for (int i = 0; i < holeCount; i++)
        {
            SpawnHole();
        }
    }

    void SpawnHole()
    {
        float xPos = Random.Range(-40f, 40f);
        float zPoa = Random.Range(-40f, 40f);
        float yPos = -0.99f;

        Vector3 spawnPos = new Vector3(xPos, yPos, zPoa);

        if (Physics.OverlapSphere(spawnPos, 3f, HolesLayer).Length == 0)
        {
            GameObject go = Instantiate(HolePrefab, spawnPos, Quaternion.identity);
            go.transform.parent = transform;
            return;
        }
        else
        {
            SpawnHole();
        }
    }
}
