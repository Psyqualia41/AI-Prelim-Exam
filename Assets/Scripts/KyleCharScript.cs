using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyleCharScript : MonoBehaviour
{
    protected CharacterController controller;
    protected Animator animator;
    public Vector3 moveDirection = new Vector3(0, 0, 0); //Direction in which character is going to moove
    private Vector3 noDirection = new Vector3(0, 0, 0); //No direction variable
    public Vector3 playerPosition;
    public Vector3 impulse = Vector3.zero;
    public float momentum;
    public float playerVelocity; //Velocity of player
    public Vector3 hitNormal; //Orientation of the slope
    public float hitAngle;
    public float clampedHitAngle;
    public bool isGrounded = false; //Becomes true if character is on floor
    public bool jumpedUsed = false; //Becomes true when character is out of jumps
    private float slideFriction = 20f; //Strength of momentum alteration on slopes
    RaycastHit hit;

/* Editable variables*/
    private float maxNormalSpeed = 45.0F; //Max speed character can move while running normally
    private float gravity = 50.0F; //Strenght of Gravity
    private float accelValue = 0.9f; //How fast does character accelerate?
    private float jumpPower = 30f; //How high the character can jump?
    public float abilityPower = 25f; //How strong is the ability?
    private float airFriction = 0.05f; //How much resistance does the character have in the air?
    private float airSpeed = 15f; //How different is movement in the air? (May be redudant, compare to airspeed)

/*Character sound effects*/
    public AudioClip jumpSFX;
    public AudioClip abilitySFX;
    AudioSource audioSource;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        controller.detectCollisions = true;

        Cursor.lockState = CursorLockMode.Locked; //Lock the cursor - MOVE THIS IN THE FUTURE
    }

    float maxValueInOneVector(Vector3 vectorToCompare)
    {
        if (vectorToCompare.x > vectorToCompare.y && vectorToCompare.x > vectorToCompare.z)
            return vectorToCompare.x;
        if (vectorToCompare.y > vectorToCompare.x && vectorToCompare.y > vectorToCompare.z)
            return vectorToCompare.y;
        if (vectorToCompare.z > vectorToCompare.y && vectorToCompare.z > vectorToCompare.x)
            return vectorToCompare.z;
        else
            return 0;
    }

    void doubleJumpAbility()
    {
            impulse = moveDirection * abilityPower;
            audioSource.PlayOneShot(abilitySFX, 0.7f);
            Debug.Log("Ability used");
    }

    void FixedUpdate()
    {
        /*
        * Hits are raycasts
        * Normal is a vector that is perpendicular to its underlying surface, Basically points on the mesh that sorta act as common points for shaders and in our case, physics?
        */
        Physics.SphereCast(playerPosition, 0.5f, Vector3.down, out hit, 20);
        hitNormal = hit.normal;
        isGrounded = (Vector3.Angle(Vector3.up, hitNormal) <= controller.slopeLimit);
        hitAngle = Vector3.Angle(Vector3.up, hitNormal);
        if (hitAngle < 30) 
        {
            clampedHitAngle = 0;
        }
        else
        {
            clampedHitAngle = hitAngle;
        }

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //User inputs direction of movement
        moveDirection = transform.TransformDirection(moveDirection);    //Converts user input to something we can use

        if (controller.isGrounded == false)//Movement speed in the air
        {
            moveDirection *= (airSpeed + momentum);
            momentum -= airFriction;
            impulse.y -= gravity * Time.deltaTime; //Gravity

            if (Input.GetKeyDown(KeyCode.LeftShift)) doubleJumpAbility();
        }
        if (controller.isGrounded == true)
        {
            jumpedUsed = false;
            impulse = noDirection;
            momentum -= clampedHitAngle * 0.02f;

            if (moveDirection == noDirection)
            {
                momentum -= 3;
            }
            else//Gradually speed up character acceleration to maxNormalSpeed
            {
                momentum += accelValue;
                moveDirection *= momentum;
            }
            momentum = Mathf.Clamp(momentum, 0f, maxNormalSpeed);

            if (Input.GetAxis("Jump") > 0)
            {
                Debug.Log("Jump!");
                jumpedUsed = true;
                impulse.y = jumpPower;
                audioSource.PlayOneShot(jumpSFX, 0.7f);
            }
            if(clampedHitAngle>0)
            {
                impulse.x += ((1f - hitNormal.y) * hitNormal.x) * slideFriction; //Add slope physics in X direction
                impulse.z += ((1f - hitNormal.y) * hitNormal.z) * slideFriction; //Add slope physics Z direction
            }
        }
        controller.Move((moveDirection + impulse) * Time.deltaTime); //Factor in all forces, and finally move the character
        //animator.Play("running", 0, float.NegativeInfinity);//Play running animation

    /*Stored variables for keepsakes*/
        playerVelocity = maxValueInOneVector(controller.velocity);
        playerPosition = controller.transform.position;
    }
}