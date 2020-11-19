using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region camera_variables
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private Vector3 currentVelocity = Vector3.zero;
    public float yPosition = 17.72189f;
    public float leftBorder;
    public float rightBorder;
    public bool horizontalLock;
    private Vector3 desiredPosition;
    private Vector3 smoothedPosition;
    #endregion

    #region Unity_functions
    void LateUpdate()
    {
        if (target != null)
        {
            desiredPosition = target.position + offset;
            smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed);
            if (horizontalLock)
            {
                smoothedPosition.y = yPosition;
            }
            if (smoothedPosition.x < leftBorder)
            {
                smoothedPosition.x = leftBorder;
            }
            if (smoothedPosition.x > rightBorder)
            {
                smoothedPosition.x = rightBorder;
            }
            transform.position = smoothedPosition;
        }
    }
    #endregion

}
