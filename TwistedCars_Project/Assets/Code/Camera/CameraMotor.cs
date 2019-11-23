using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour {


    public float dampTime = 0.2f;
    public float screenEdgeBuffer = 4f;
    public float minSize = 6.5f;
    public Transform[] Targets;



    private Camera cam;
    private float zoomSpeed;
    private Vector3 moveVelocity;
    private Vector3 desiredPosition;



    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();

    }

    private void FixedUpdate()
    {

        Move();
        Zoom();
    }


    private void Move()
    {

        FindAveragePosition();
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref moveVelocity, dampTime);
    }


    private void FindAveragePosition()
    {

        Vector3 averagePos = new Vector3();
        int numTargets = 0;


        for (int i = 0; i < Targets.Length; i++)
        {

            if (!Targets[i].gameObject.activeSelf)
                continue;

            averagePos += Targets[i].position;
            numTargets++;

        }


        if (numTargets > 0)
        {

            averagePos /= numTargets;
            averagePos.y = transform.position.y;
            desiredPosition = averagePos;

        }

    }



    private void Zoom()
    {

        float requiredSize = FindRequiredSize();
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, requiredSize, ref zoomSpeed, dampTime);

    }


    private float FindRequiredSize()
    {
        Vector3 desiredLocalPos = transform.InverseTransformPoint(desiredPosition);
        float size = 0f;

        for (int i = 0; i < Targets.Length; i++)
        {

            if (!Targets[i].gameObject.activeSelf)
                continue;


            Vector3 targetLocalPos = transform.InverseTransformPoint(Targets[i].position);
            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / cam.aspect);
        }



        size += screenEdgeBuffer;
        size = Mathf.Max(size, minSize);
        return size;

    }



    public void SetStartPositionAndSize()
    {

        FindAveragePosition();
        transform.position = desiredPosition;
        cam.orthographicSize = FindRequiredSize();

    }
}

