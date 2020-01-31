using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCameraSystem : MonoBehaviour {


    #region Variables
    public Transform m_Target;
    public float m_Height = 10f;
    public float m_Distance = 20f;
    public float m_Angle = 45f;
    public float m_SmoothSpeed = 0.5f;
    private Vector3 refVelocity;
    #endregion

    #region Main Methods
    private void Start()
    {
        HandleCamera();
    }

    private void Update()
    {
        HandleCamera();
    }
    #endregion


    protected virtual void HandleCamera()
    {
        if (!m_Target)
        {
            return;
        }

        //World position
        Vector3 worldPosition = (Vector3.forward * -m_Distance) + (Vector3.up * m_Height);
        Debug.DrawLine(m_Target.position, worldPosition, Color.red);

        //rotation positon
        Vector3 rotateVector = Quaternion.AngleAxis(m_Angle, Vector3.up) * worldPosition;
        Debug.DrawLine(m_Target.position, rotateVector, Color.green);

        //Move to our Position
        Vector3 flatTargetPosition = m_Target.position;
        flatTargetPosition.y = 0f;
        Vector3 finalPosition = flatTargetPosition + rotateVector;
        Debug.DrawLine(m_Target.position, finalPosition, Color.blue);

        transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref refVelocity, m_SmoothSpeed);
        transform.LookAt(flatTargetPosition);









    }


}






