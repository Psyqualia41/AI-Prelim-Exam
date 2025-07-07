using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    protected Camera gameCam;
    public GameObject target;
    private Vector3 offset;

    private float xMouseMove; //Speed the user move the mouse L and R
    private float yMouseMove; //Speed the user moves the mouse up and down

    public Vector3 yawOrbit;
    public Vector3 pitchOrbit;
    public Vector3 totalOrbit;
    public Vector3 cameraPosition;
    public float distanceFromTarget;
    public float yMouseCurrentAngle;


    /*Camera Options*/
    public bool followCameraOn;
    public float sensitivity = 5f;
    public bool mLookX = true;
    public bool mLookY = true;
    public float minPitch;
    public float maxPitch;






 









    void Start()
    {
        gameCam = GetComponent<Camera>();
        offset = transform.position - target.transform.position;//dist between gamCam obj and target obj
        yawOrbit = offset;
        pitchOrbit = offset;


    }


    void LateUpdate()
    {
        if (followCameraOn == true)
        {
            xMouseMove = Input.GetAxis("Mouse X");
            yMouseMove = Input.GetAxis("Mouse Y");

            /*
             * Quaternion.Angleaxis rotates set degrees from a specific point. 
             * For example, Quaternion.Angleaxis(30, Vector3.up) will set a transform rotation of 30 degrees around the y-axis
             */

            //clamp xMouseMove to a smaller value the more y the camera is 

            //xMouseMove = Mathf.Clamp();



            yMouseCurrentAngle = Vector3.Angle(gameCam.transform.position, target.transform.position);

            yMouseMove = Mathf.Clamp(yMouseMove, float.NegativeInfinity, yMouseCurrentAngle);

            pitchOrbit = Quaternion.AngleAxis(yMouseMove * sensitivity, Vector3.right) * pitchOrbit; //move camera around pitch

            //The higher the mouse is, the smaller the X/Z offset should be

            yawOrbit = Quaternion.AngleAxis(xMouseMove * sensitivity, Vector3.up) * yawOrbit; //Always be behind player
            target.transform.eulerAngles += (Vector3.up * xMouseMove * sensitivity); //Alters yaw of char

            gameCam.transform.position = target.transform.position; //Moves with player
            //totalOrbit = (pitchOrbit + yawOrbit);

            totalOrbit.x = (pitchOrbit.x+yawOrbit.x);
            totalOrbit.y = (pitchOrbit.y+yawOrbit.y);
            totalOrbit.z = pitchOrbit.z;





            if (mLookX == true && mLookY == true)
            {

                gameCam.transform.position += totalOrbit;
            }
            else if (mLookX == false && mLookY == true)
            {
                gameCam.transform.position += pitchOrbit;
            }
            else if (mLookX == true && mLookY == false)
            {
                gameCam.transform.position += yawOrbit;
            }
            gameCam.transform.LookAt(target.transform.position); //Always look at target


            cameraPosition = gameCam.transform.position;
            distanceFromTarget = Vector3.Distance(gameCam.transform.position, target.transform.position);

 



        }
    }
}