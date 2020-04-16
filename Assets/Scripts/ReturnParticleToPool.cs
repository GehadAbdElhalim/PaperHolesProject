using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnParticleToPool : MonoBehaviour
{
    ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        if(tag != "PlayerParticles")
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
    }

    // Update is called once per frame
    void Update()
    {
        if (ps.isStopped)
        {
            if (tag != "PlayerParticles")
            {
                ObjectPooler.instance.ReturnToPool("Particles", this.gameObject);
            }
            else
            {
                ObjectPooler.instance.ReturnToPool(tag, this.gameObject);
            }
        }
    }
}
