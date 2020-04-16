using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using EZCameraShake;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;

    AudioSource audio;

    GameObject player;

    public float startExplosionDistance;

    public Color flashColor;

    public float explosionForce;

    public float explosionRadius;

    public bool exploding = false;

    //public AudioClip explosion_sfx;
    public AudioClip alert_sfx;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player = PlayerInstanceHandler.Instance.gameObject;

        audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        agent.SetDestination(player.transform.position);

        if (Vector3.Distance(transform.position, PlayerInstanceHandler.Instance.transform.position) < startExplosionDistance && !exploding)
        {
            exploding = true;
            agent.speed = 2;
            StartCoroutine(FlashBeforeExplosion(1, 0.1f));

            Invoke("SelfDestruct", 1);
        }
    }

    void SelfDestruct()
    {
        //audio.PlayOneShot(explosion_sfx);
        PlayerInstanceHandler.Instance.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
        PlayerInstanceHandler.Instance.GetComponent<PlayerMovement>().affectedByExplosion = true;
        PlayerInstanceHandler.Instance.GetComponent<PlayerMovement>().SpawnDeathParticles();
        ReturnItToPool();
        ObjectPooler.instance.SpawnFromPool("Particles", transform.position, Quaternion.identity);
        CameraShaker.Instance.ShakeOnce(2, 2, 0.1f, 1);
    }

    IEnumerator FlashBeforeExplosion(float duration , float durationBetweenFlashes)
    {
        float c_duration = duration;
        float c_durationBetweenFlashes = durationBetweenFlashes;

        while (c_duration > 0)
        {
            c_duration -= Time.fixedDeltaTime;

            if(c_durationBetweenFlashes > 0)
            {
                c_durationBetweenFlashes -= Time.fixedDeltaTime;
            }
            else
            {
                c_durationBetweenFlashes = durationBetweenFlashes;

                Material enemyMat = GetComponent<MeshRenderer>().material;
                Material trailMat = GetComponent<TrailRenderer>().material;

                if (enemyMat.color == Color.blue)
                {
                    enemyMat.color = flashColor;
                    trailMat.color = flashColor;
                    audio.PlayOneShot(alert_sfx);
                }
                else
                {
                    enemyMat.color = Color.blue;
                    trailMat.color = Color.blue;
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destroyer"))
        {
            ReturnItToPool();
        }
    }

    private void ReturnItToPool()
    {
        EnemySpawnSystem.instance.EnemyNumberDecreased();
        GetComponent<TrailRenderer>().time = 0;
        ObjectPooler.instance.ReturnToPool("Enemy", this.gameObject);
    }
}
