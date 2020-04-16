﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance { get; private set;}

    bool isReadyToSpawnAnotherHole = false;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    private Pool GetPoolWithTag(string tag)
    {
        foreach(Pool pool in pools)
        {
            if(pool.tag == tag)
            {
                return pool;
            }
        }

        return null;
    }

    public void AddGameObjects(string tag , int amount)
    {
        Pool pool = GetPoolWithTag(tag);

        if (pool == null)
        {
            Debug.Log("There is no pool with the name " + tag);
            return;
        }

        for (int i = 0; i < amount; i++)
        {
            GameObject go = Instantiate(pool.prefab);
            go.SetActive(false);
            poolDictionary[tag].Enqueue(go);
        }
    }

    public GameObject SpawnFromPool(string tag , Vector3 position , Quaternion rotation)
    {
        isReadyToSpawnAnotherHole = false;
        if(poolDictionary[tag].Count == 0)
        {
            AddGameObjects(tag, 1);
        }

        GameObject go = poolDictionary[tag].Dequeue();
        go.SetActive(true);
        go.transform.SetPositionAndRotation(position, rotation);

        if (tag == "Hole")
        {
            isReadyToSpawnAnotherHole = true;
        }

        return go;
    }

    public void ReturnToPool(string tag, GameObject go)
    {
        poolDictionary[tag].Enqueue(go);
        go.SetActive(false);
        if (tag != "Particles" && tag != "PlayerParticles")
        {
            go.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public bool IsReadyToSpawnAnotherHole()
    {
        return isReadyToSpawnAnotherHole;
    }
}
