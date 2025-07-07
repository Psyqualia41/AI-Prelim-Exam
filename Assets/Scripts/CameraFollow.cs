using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;

    private float xSens = 250.0f; //Speed the user move the mouse L and R
    private float ySens = 120.0f; //Speed the user moves the mouse up and down
    private float sensitivity = 5f;

    public float yMinLimit = -20;
    public float yMaxLimit = 80;

    float x = 0.0f;
    float y = 0.0f;
    float prevDistance;

    void Start()
    {
       var angles = transform.eulerAngles;
       x = angles.y;
       y = angles.x;

       offset = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        //if (offset < 2) offset = 2;
        //offset -= Input.GetAxis("Mouse ScrollWheel") * 2;

        x += Input.GetAxis("Mouse X") * xSens * 0.02f;
        y -= Input.GetAxis("Mouse Y") * ySens * 0.02f;

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        var position = rotation * new Vector3(0, 0, -10) + target.transform.position;

        target.transform.eulerAngles += (Vector3.up * Input.GetAxis("Mouse X") * sensitivity);

        transform.rotation = rotation;
        transform.position = position;
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}