using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TryAgainScript : MonoBehaviour
{
    [SerializeField]Image TimerImage;
    public float TimerSpeed;

    // Start is called before the first frame update
    void OnEnable()
    {
        TimerImage.fillAmount = 1;
    }

    
    void FixedUpdate()
    {
        if (TimerImage.fillAmount > 0)
        {
            TimerImage.fillAmount -= Time.fixedDeltaTime * TimerSpeed;
        }
        else
        {
            GameMenusController.instance.Skip();
        }
    }
}
