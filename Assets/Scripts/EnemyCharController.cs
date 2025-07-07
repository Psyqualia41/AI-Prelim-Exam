using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCharController : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    public float speed = 2.0f;
    private Vector3 velocity;
    public float gravityValue = -9.81f;
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    private CharController playerScript;

    bool hasNotExploded = true;
    bool hasPlayedChase = false;
    bool hasPlayedExplode = false;
    bool hasPlayedReturn = false;

    public float sfxVolume = 0.7f;
    public AudioClip chaseSFX;
    public AudioClip explodeSFX;
    public AudioClip detachSFX;
    public AudioClip returnSFX;
    public AudioSource enemyAudio;
    public AudioSource extraAudio;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("CharacterController").transform;
        agent = GetComponent<NavMeshAgent>();
        playerScript = player.GetComponent<CharController>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        if (dist >= 12.0 || playerScript.isDead) {
            if (!hasNotExploded && !hasPlayedReturn) {
                enemyAudio.PlayOneShot(returnSFX, sfxVolume);
                hasPlayedReturn = true;
            }
            setAnimation("isIdle"); 
            agent.isStopped = false; 
        } else if (dist < 10.0 && dist > 2.1 && hasNotExploded) {
            ChasePlayer();
        } else if (dist <= 2.1) {
            agent.isStopped = true;
            setAnimation("isExploding");
        }
    }

    void ChasePlayer ()
    {
        setAnimation("isChasing");
        agent.SetDestination(player.position);
    }

    void setAnimation (string s)
    {
        if (s.Equals("isChasing")) {
            hasPlayedReturn = false;
            if (!hasPlayedChase) {
                enemyAudio.PlayOneShot(detachSFX, sfxVolume);
                enemyAudio.loop = true;
                enemyAudio.clip = chaseSFX;
                enemyAudio.Play();
                hasPlayedChase = true;
            }

            animator.SetBool("isChasing", true);
            animator.SetBool("isIdle", false);
            animator.SetBool("isExploding", false);
        } else if (s.Equals("isIdle")) {
            hasPlayedExplode = false;
            hasPlayedChase = false;
            enemyAudio.loop = false;

            animator.SetBool("isChasing", false);
            animator.SetBool("isIdle", true);
            animator.SetBool("isExploding", false);
            hasNotExploded = true;      
        } else if (s.Equals("isExploding")) {
            enemyAudio.Stop();
            if (!hasPlayedExplode) {
                extraAudio.PlayOneShot(explodeSFX, sfxVolume);
                hasPlayedExplode = true;
                enemyAudio.loop = false;

                float lives = playerScript.lives;
                if (lives > 0) playerScript.takeDamage(1);
            }
            animator.SetBool("isChasing", false);
            animator.SetBool("isIdle", false);
            animator.SetBool("isExploding", true);
            hasNotExploded = false;
        } else {
            Debug.Log("Invalid input to 'setAnimation' method");
        }
    }
}
