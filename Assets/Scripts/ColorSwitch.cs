using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ColorSwitch : MonoBehaviour
{
    public Material background;
    public Material holes;
    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        holes.color = Color.black;
        background.color = Color.white;
        StartCoroutine(ChangeColor(holes, duration));
        StartCoroutine(ChangeColor(background, duration));
    }

    IEnumerator ChangeColor(Material mat, float duration)
    {
        while (true)
        {
            if (mat.color == Color.black)
            {
                mat.DOColor(Color.white, duration);
            }

            if (mat.color == Color.white)
            {
                mat.DOColor(Color.black, duration);
            }

            yield return new WaitForSecondsRealtime(duration);
        }
    }
}
