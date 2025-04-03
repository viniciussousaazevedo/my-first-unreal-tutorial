using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;
    private CapsuleCollider2D playerCollider;
    private float moveX;
    public float speed;
    public int addJumps;
    public bool isGrounded;
    public float jumpForce;
    public int life;
    public TextMeshProUGUI textLife;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal"); // returns 1, 0 or -1
        textLife.text = life.ToString();

        if (life <= 0) {
            this.enabled = false;
            playerCollider.enabled = false;
            rigidBody.gravityScale = 0;
            animator.Play("Death", -1);
        }
    }

    void FixedUpdate() { // better for updates that involves physics
        Move();
        Attack();
        
        if (isGrounded) {
            addJumps = 1;
            if (Input.GetButtonDown("Jump")) { // unity default key for jump: space bar
                Jump();
            }
        } else {
            if (Input.GetButtonDown("Jump") && addJumps > 0) {
                addJumps--;
                Jump();
            }
        }
    }

    void Move() {
        rigidBody.linearVelocity = new Vector2(moveX * speed, rigidBody.linearVelocityY);

        if (moveX > 0) { // == going right
            transform // selects the 'transform' unity component
                .eulerAngles = new Vector3(0f, 0f, 0f); // rotates the skin to the right
                animator.SetBool("isRunning", true);
        }
        if (moveX < 0) { // == going left
            transform // selects the 'transform' unity component
            .eulerAngles = new Vector3(0f, 180f, 0f); // rotates the skin to the left
            animator.SetBool("isRunning", true);
        }
        if (moveX == 0) {
            animator.SetBool("isRunning", false);
        }
    }

    void Jump() {
        rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocityX, jumpForce);
    }

    void Attack() {
        if (Input.GetButtonDown("Fire1")) {
            animator.Play("Attack", -1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
    }
}
