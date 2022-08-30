using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 10; // Pelaajan nopeus
    public float jumpForce = 500;  // Pelaajan hypyn voima
    public float groundSensorDist = 2f;
    public LayerMask groundMask;
    public AudioClip onJumpAudio;
    public float onJumpAudioVolume;
    private bool isGrounded = false;  // Pelaajan ground check

    private Rigidbody2D rb;  // Rigidbody referenssi 
    private Vector2 playerInput;  // Pelaajan input
    private bool turnedRight = true;  // Onko pelaaja k‰‰ntynyt oikealle, jos False niin on k‰‰ntynyt vasemmalle
    private AudioSource AS;  // Audiosource referenssi

    [HideInInspector] public bool isDead = false;

    private BaseInteractable isInteractingWith;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // Haetaan rigidbody ja Audiosource komponentti talteen
        rb = GetComponent<Rigidbody2D>();
        AS = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            playerInput = Vector2.zero;
            return;
        }

        // Pelaajan liikkumis input
        playerInput.x = Input.GetAxis("Horizontal") * speed;

        playerInput.y = rb.velocity.y;

        // Tarkistus mihin suuntaan pelaaja on liikkumassa
        if (playerInput.x > 0)
        {
            // JOs hahmo liikkuu oikealle, niin asetetaan skaalaksi 1,1,1 ja turnedRight = true
            transform.localScale = new Vector3(1, 1, 1);
            turnedRight = true;
        }
        else if (playerInput.x < 0)
        {
            // Jos hahmo liikkuu vasemmalle, flipataan hahmo muuttamalla skaalaa -1, 1, 1 ja turnedRight = false
            transform.localScale = new Vector3(-1, 1, 1);
            turnedRight = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInteractingWith != null)
            {
                isInteractingWith.Interact();
            }
        }

        // Kutsutaan Groundcheck metodia
        GroundCheck();
        // Kutsutaan Jump metodia
        Jump();

        anim.SetFloat("velocity", Mathf.Abs(rb.velocity.x));
    }

    private void FixedUpdate()
    {
        // Liikutetaan pelaajaa
        rb.velocity = playerInput;

        //agege
    }

    void GroundCheck()
    {
        Debug.DrawRay(this.transform.position, Vector2.down * groundSensorDist, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, groundSensorDist, groundMask);

        // Jos pelaajan rigidbodyn velocity.y arvo on 0.01 ja -0.01 v‰lill‰, ollaan "Grounded" eli maassa
        if (hit.collider != null)
        {
            if (isGrounded == false)
                anim.SetBool("isGrounded", true);

            isGrounded = true;
        }
        else
        {
            if (isGrounded == true)
                anim.SetBool("isGrounded", false);

            isGrounded = false;
        }
    }

    void Jump()
    {
        // Kun ollaan "grounded" ja painetaan Space, hyp‰t‰‰n
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce);
            anim.PlayInFixedTime("Jump");
            AS.PlayOneShot(onJumpAudio, onJumpAudioVolume);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BaseInteractable>())
        {
            BaseInteractable interactable = collision.gameObject.GetComponent<BaseInteractable>();
            isInteractingWith = interactable;

            if (interactable.isOnEnterInteraction)
            {
                interactable.Interact();
            }
        }

        if (collision.gameObject.GetComponent<JumpPad>())
        {
            collision.gameObject.GetComponent<JumpPad>().Activate(rb);
            anim.PlayInFixedTime("Jump");
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BaseInteractable>())
        {
            isInteractingWith = null;
        }

    }
}
