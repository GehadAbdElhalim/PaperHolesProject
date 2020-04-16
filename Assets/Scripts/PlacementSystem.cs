using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    public GameObject placeHolder;
    GameObject currentPlaceHolder;


    private void Start()
    {
        currentPlaceHolder = Instantiate(placeHolder, Vector3.zero, Quaternion.identity);
        currentPlaceHolder.SetActive(false);
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            currentPlaceHolder.transform.position = hit.point;
            currentPlaceHolder.SetActive(true);
        }
    }
}
