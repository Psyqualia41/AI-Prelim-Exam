using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyleCamera : MonoBehaviour
{
    protected Camera Camera;
    public GameObject target;
    public Vector3 offset;

    public float xAxisMovement = 0.0f;
    public float yAxisMovement = 0.0f;

    public Vector3 cameraPosition;

 /*Camera Options*/
    public float xSens = 250.0f;
    public float ySens = 120.0f;

    public bool mLookX = true;
    public bool mLookY = true;
    public float minY = -20;
    public float maxY = 80;

    void Start()
    {
        Camera = GetComponent<Camera>();
        var angles = Camera.transform.eulerAngles;
        xAxisMovement = angles.y;
        yAxisMovement = angles.x;
        offset = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        xAxisMovement += Input.GetAxis("Mouse X") * xSens * 0.2f;
        yAxisMovement -= Input.GetAxis("Mouse Y") * ySens * 0.2f;

        yAxisMovement = clampYAngle(yAxisMovement, minY, maxY);

        Quaternion rotation = Quaternion.Euler(yAxisMovement, xAxisMovement, 0);

        var position = rotation * offset + target.transform.position;

        Camera.transform.rotation = rotation;
        Camera.transform.position = position;

        target.transform.eulerAngles += (Vector3.up * Input.GetAxis("Mouse X") * xSens*0.2f);
        cameraPosition = Camera.transform.position;
    }
    static float clampYAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}