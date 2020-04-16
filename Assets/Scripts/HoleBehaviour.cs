using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleBehaviour : MonoBehaviour
{

    public LayerMask holeLayer;

    private void Start()
    {
        if(Physics.OverlapSphere(transform.position, 3, holeLayer).Length > 1)
        {
            ObjectPooler.instance.ReturnToPool("Hole", this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 || other.CompareTag("Destroyer"))
        {
            ObjectPooler.instance.ReturnToPool("Hole", this.gameObject);
        }
    }
}
