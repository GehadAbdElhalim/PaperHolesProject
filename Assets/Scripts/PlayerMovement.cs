using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    InputManager inputMangaer;
    Rigidbody rb;
    public bool StopMovement;
    public bool affectedByExplosion;

    public Ease _moveEase;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputMangaer = GetComponent<InputManager>();
        StopMovement = false;
        affectedByExplosion = false;
    }

    private void Update()
    {
        if (!StopMovement && !affectedByExplosion)
        {
            transform.RotateAround(transform.position, transform.up, inputMangaer.axisValue * rotateSpeed * Time.deltaTime);
            rb.velocity = transform.forward * moveSpeed * Time.fixedDeltaTime;
            //transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
        }

        if (affectedByExplosion)
        {
            transform.RotateAround(transform.position, transform.up, inputMangaer.axisValue * rotateSpeed * Time.deltaTime);
            Invoke("NotAffectedByExplosion", 0.3f);
        }


        if(StopMovement)
        {
            rb.velocity = Vector3.zero;
        }
    }

    void NotAffectedByExplosion()
    {
        affectedByExplosion = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9) // Hit a hole
        {
            transform.GetChild(0).transform.parent = null;
            FallInHole(other);
        }
    }

    void FallInHole(Collider hole)
    {
        StopMovement = true;
        GetComponent<AudioSource>().Play();
        transform.DOMoveX(hole.transform.position.x, 5).SetEase(_moveEase);
        transform.DOMoveZ(hole.transform.position.z, 5).SetEase(_moveEase);
        StartCoroutine(ScaleDown(1,4));
        //StartCoroutine(WhirlWindEffect(transform,new Vector3(hole.transform.position.x,transform.position.y,hole.transform.position.z),5));
        StartCoroutine(ScaleDownTrail(GetComponent<TrailRenderer>(),1,4));
        //Invoke("SpawnDeathParticles", 4);
        Invoke("ShowGameOver", 4);
    }

    public void SpawnDeathParticles()
    {
        ObjectPooler.instance.SpawnFromPool("PlayerParticles", transform.position, Quaternion.identity);
    }

    void ShowGameOver()
    {
        if (GameMaster.instance.giveSecondChance)
        {
            GameMaster.instance.giveSecondChance = false;
            GameMenusController.instance.ShowTryAgain();
        }
        else
        {
            GameMenusController.instance.GameOver();
        }
    }

    IEnumerator WhirlWindEffect(Transform go, Vector3 centerPoint, float duration)
    {
        float maxTime = duration;
        //go.forward = Quaternion.Euler(0, 90, 0) * ((centerPoint - go.position).normalized);

        while (duration > 0)
        {
            duration -= Time.fixedDeltaTime;

            //go.Translate(go.forward * (Time.fixedDeltaTime/duration) * 2 , Space.World);
            go.RotateAround(centerPoint, Vector3.up, ((maxTime - duration) / maxTime) * 10);
            //go.forward = Quaternion.Euler(0, 90, 0) * ((centerPoint - go.position).normalized);
            //go.Translate(go.forward * ((maxTime - duration) / maxTime) * 5, Space.World);
            //go.transform.position = centerPoint * ((maxTime-duration)/maxTime);
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator ScaleDown(float waitDuration , float duration)
    {
        yield return new WaitForSecondsRealtime(waitDuration);

        transform.DOScale(Vector3.zero, duration);
    }

    IEnumerator ScaleDownTrail(TrailRenderer trail,float waitDuration, float duration)
    {
        yield return new WaitForSecondsRealtime(waitDuration);

        while(trail.widthMultiplier > 0)
        {
            trail.widthMultiplier -= 2 * (Time.fixedDeltaTime/duration);
            yield return new WaitForFixedUpdate();
        }
    }
}
