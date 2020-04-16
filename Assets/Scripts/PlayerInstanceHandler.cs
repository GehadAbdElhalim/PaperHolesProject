using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstanceHandler : MonoBehaviour
{
    public static PlayerInstanceHandler Instance;

    private void Awake()
    {
        Instance = this;
    }

}
