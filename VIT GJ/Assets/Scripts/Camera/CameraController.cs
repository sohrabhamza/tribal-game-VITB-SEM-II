using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed = 5;
    [SerializeField] Vector3 offset;
    Vector3 wantedPos;
    float myX;
    private void Awake()
    {
        offset.x = transform.position.x;
        offset.y = transform.position.y;
        myX = offset.x;
    }

    private void FixedUpdate()
    {
        wantedPos = target.TransformPoint(offset);
        wantedPos.x = myX;
        transform.position = Vector3.Lerp(transform.position, wantedPos, Time.deltaTime * speed);
    }
}

