using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    GameObject player;
    public Vector3 offset;

    private void Start()
    {
        player = PlayerInstanceHandler.Instance.gameObject;
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        //transform.up = player.transform.forward;
    }
}
