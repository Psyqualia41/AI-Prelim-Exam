using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private Vector3 playerVelocity;
    public bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float alteredSpeed = 1;
    public float jumpHeight = 1.0f;
    public float alteredJump;
    public float gravityValue = -9.81f;
    public int lives = 3;

    private bool hasPlayedDead = false;
    public bool isDead = false;

    public float sfxVolume = 0.7f;
    public AudioClip jumpSFX;
    public AudioClip deathSFX;
    private AudioSource audio;

    public GameObject livesImgs;
    public GameObject gameOverText;
    public Timer timer;

    private string scene;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        controller.minMoveDistance = 0;
        if (SceneManager.GetActiveScene().name == "Level 1")
            timer = GameObject.Find("TimerText").GetComponent<Timer>();

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lives <= 0) {
            killPlayer();
        } else {
            groundedPlayer = controller.isGrounded;
            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));      // Gets vector of movement from keyboard input
            moveDirection = transform.TransformDirection(moveDirection);                                         // Converts user input to something we can use
            controller.Move(moveDirection * Time.deltaTime * playerSpeed * alteredSpeed);                                       // Move the controller
            
            // Gravity continously decreases player velocity, so much reset to 0 anytime it goes negative
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                animator.SetBool("isIdle", false);
                animator.SetBool("isMoving", false);
                animator.SetBool("isSplodge", true);
            }
            else if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
            {
                animator.SetBool("isIdle", false);
                animator.SetBool("isMoving", true);
                animator.SetBool("isSplodge", false);
            }
            else
            {
                animator.SetBool("isIdle", true);
                animator.SetBool("isMoving", false);
                animator.SetBool("isSplodge", false);
            }
            
            // Changes height poistion of bean based on gravity and jump height
            if (Input.GetAxis("Jump") > 0 && groundedPlayer)
            {
                audio.PlayOneShot(jumpSFX, sfxVolume);
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue* alteredJump);      // If jump, increase velocity in the y direction
            }

            playerVelocity.y += gravityValue * Time.deltaTime;          // Calculate gravity's current impact on bean
            controller.Move(playerVelocity * Time.deltaTime);           // Move bean
        }
    }

    public void takeDamage (int damage)
        {
            for (int i = 0; i < damage; i++) {
                removeUIHeart(lives);
                lives--;
            }
        }

    public void removeUIHeart (int heartNum)
    {
        Image img = livesImgs.transform.GetChild(heartNum-1).gameObject.GetComponent<Image>();
        var tempColor = img.color;
        tempColor.a = 0f;
        img.color = tempColor;
    }

    public void killPlayer ()
    {
        animator.SetBool("isDead", true);
        isDead = true;
        timer.playerDead = true;
        if (!hasPlayedDead) {
            audio.PlayOneShot(deathSFX, 2.0f);
            hasPlayedDead = true;
        }

        TextMeshProUGUI text = gameOverText.GetComponent<TextMeshProUGUI>();
        var tempColor = text.color;
        tempColor.a = 1f;
        text.color = tempColor;
    }
}
