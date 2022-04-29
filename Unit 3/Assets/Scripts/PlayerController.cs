using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpForce = 8f;
    [SerializeField] float doubbleJumpForce = 4f;
    [SerializeField] float gravityModifier = 1;

    StartGame startGame;

    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;

    public ParticleSystem deathExplosion;
    public ParticleSystem runningParticle;

    Rigidbody rb;
    Animator animator;

    public bool isGrounded = true;
    public bool gameOver = false;
    public bool running = false;

    private bool canDoubbleJump;
    private float minHigthDoubbleJump;

    private float startSpeed = 0.3f;
    private float normalSpeed = 1.0f;
    private float runningSpeed = 1.5f;

    void Awake()
    {
        startGame = FindObjectOfType<StartGame>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(!startGame.hasStartGame)
        {
            animator.SetFloat("Speed_f", startSpeed);
            runningParticle.Stop();
        }
        else
        {
            animator.SetFloat("Speed_f", normalSpeed);

            minHigthDoubbleJump = transform.position.y;
            JumpAndDoubbleJump();
            Run();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && startGame.hasStartGame)
        {
            animator.SetBool("Jump_b", false);
            isGrounded = true;
            runningParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            animator.SetBool("Death_b", true);
            animator.SetInteger("DeathType_int", 1);

            gameOver = true;

            runningParticle.Stop();
            deathExplosion.Play();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }

    void JumpAndDoubbleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !gameOver)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            animator.SetBool("Jump_b", true);
            playerAudio.PlayOneShot(jumpSound, 1.0f);

            isGrounded = false;
            canDoubbleJump = true;
            runningParticle.Stop(); 
        }
        else if (Input.GetKeyDown(KeyCode.Space) && canDoubbleJump && minHigthDoubbleJump > 2.5f)
        {
            rb.AddForce(Vector3.up * doubbleJumpForce, ForceMode.VelocityChange);
            canDoubbleJump = false;

            StartCoroutine(Rotate(1f));
        }
    }

    IEnumerator Rotate(float duration)
    {
        float startRotation = transform.eulerAngles.y;
        float endRotation;

        if (Random.value <= 0.5)
        {
            endRotation = startRotation - 360.0f;
        }
        else
        {
            endRotation = startRotation + 360.0f;
        }

        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
            yield return null;
        }
    }

    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            animator.SetFloat("Speed_f", runningSpeed);

            running = true;
        }
        else
        {
            running = false;
        }
    }
}



