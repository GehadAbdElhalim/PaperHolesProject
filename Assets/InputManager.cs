using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float axisValue;


    private void Update()
    {
#if UNITY_ANDROID || UNITY_EDITOR 
        if(Input.GetMouseButton(0) && Input.mousePosition.x >= Screen.width / 2)
        {
            axisValue = 1;
        }
        else if(Input.GetMouseButton(0) && Input.mousePosition.x < Screen.width / 2){
            axisValue = -1;
        }
        else
        {
            axisValue = 0;
        }
#endif

#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL
        axisValue = Input.GetAxisRaw("Horizontal");
#endif
    }
}
