using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerBehaviour : MonoBehaviour
{
    GameObject player;
    public float offset;

    private void Start()
    {
        player = PlayerInstanceHandler.Instance.gameObject;
    }

    private void LateUpdate()
    {
        transform.forward = player.transform.forward;
        transform.position = player.transform.position + ((-player.transform.forward) * offset);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            ObjectPooler.instance.ReturnToPool("Hole", other.gameObject);
        }

        if(other.gameObject.layer == 11)
        {
            ObjectPooler.instance.ReturnToPool("Enemy", other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            ObjectPooler.instance.ReturnToPool("Hole", other.gameObject);
        }

        if (other.gameObject.layer == 11)
        {
            ObjectPooler.instance.ReturnToPool("Enemy", other.gameObject);
        }
    }
}
