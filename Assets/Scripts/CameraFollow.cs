using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform controller;
    public Vector3 offset;


    private void FixedUpdate()
    {
        transform.position = controller.position+offset;
    }
}
