using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderScript : MonoBehaviour
{
    public Color flashColor;
    public float explosionForce;
    public float explosionRadius;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GetComponent<MeshRenderer>().material.color = flashColor;
            PlayerInstanceHandler.Instance.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, collision.contacts[0].point, explosionRadius);
            PlayerInstanceHandler.Instance.GetComponent<PlayerMovement>().affectedByExplosion = true;
            PlayerInstanceHandler.Instance.GetComponent<PlayerMovement>().SpawnDeathParticles();
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            Invoke("ReturnToNormalColor", 0.5f);
        }
    }

    void ReturnToNormalColor()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
