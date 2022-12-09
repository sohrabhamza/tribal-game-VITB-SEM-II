using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] Transform body;

    CharacterController cont;
    float oldY;
    float x, z;

    private void Start()
    {
        cont = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        ApplyMovemement();
    }

    void GetInput()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
    }

    void ApplyMovemement()
    {
        Vector3 movement = new Vector3(x, 0, z) * Time.deltaTime * speed;

        cont.Move(movement);

        if (cont.velocity.magnitude > 0)
        {
            Vector3 newBodRot = cont.velocity;
            Quaternion newRot = Quaternion.LookRotation(newBodRot);
            body.rotation = Quaternion.Lerp(body.rotation, newRot, Time.deltaTime * 5);
            body.eulerAngles = new Vector3(0, body.eulerAngles.y, 0);
        }
    }
}
