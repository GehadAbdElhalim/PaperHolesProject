using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HoleScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            Vector3 v = Vector3.zero;
            if(other.GetComponent<NavMeshAgent>().enabled == true)
            {
                v = other.GetComponent<NavMeshAgent>().velocity;
                other.GetComponent<NavMeshAgent>().isStopped = true;
                other.GetComponent<NavMeshAgent>().enabled = false;
            }
            other.GetComponent<Rigidbody>().velocity = v;
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.transform.GetChild(0).GetComponent<Animator>().SetBool("Falling", true);
            //other.isTrigger = true;
        }
    }
}
