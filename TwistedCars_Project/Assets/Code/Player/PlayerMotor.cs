using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    [Range(0,1000)]
    public float speed;
    [Range(0,1000)]
    public float rotationSpeed;
    private Rigidbody rb;
    private Vector3 input;
    public FloatingJoystick joystick;
    Quaternion targetRotation;


    private void Start()
    {
        joystick = FindObjectOfType<FloatingJoystick>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RotateChecker();
    }

    private void FixedUpdate()
    {
        Move();
    }



    private void RotateChecker()
    {

        Vector3 input = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        if (input != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(input);
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
        }

        // Vector3 movement = input.normalized;
        // movement *= speed;
        // //movement *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;
        //// movement += Vector3.up * speed;

        // rb.MovePosition(rb.position * speed * Time.deltaTime);



       

    }

    private void Move()
    {
  

        Vector3 dir = Vector3.zero;
        Vector3 movement = new Vector3(dir.x, 0, dir.z);
        dir.x = joystick.Horizontal;
        dir.z = joystick.Vertical;

    
        transform.Translate(dir * speed * Time.deltaTime,Space.World);
    }


}

