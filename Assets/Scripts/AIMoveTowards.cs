using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMoveTowards : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform target;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
    }
}
