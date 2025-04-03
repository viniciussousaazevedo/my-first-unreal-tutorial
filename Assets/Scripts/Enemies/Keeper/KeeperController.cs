using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class KeeperController : MonoBehaviour
{
    private Animator animator;
    private bool goRight;
    public int life;
    public float speed;

    public Transform a;
    public Transform b;
    private CapsuleCollider2D keeperCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
        keeperCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Move();

        if (life <= 0) {
            this.enabled = false;
            keeperCollider.enabled = false;
            animator.Play("Death", -1);
        }
    }

    void Move() {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) {
            return;
        }

        if (goRight) {
            if (Vector2.Distance(transform.position, b.position) < 0.1f) {
                goRight = false;
            }
            transform.eulerAngles = new Vector3(0f, 0f, 0f); // the sprite default is going right, so we want to keep it in the 'default' way
            transform.position = Vector2.MoveTowards(transform.position, b.position, speed * Time.deltaTime); // delta time is important in transform position manipulation
        } else {
            if (Vector2.Distance(transform.position, a.position) < 0.1f) {
                goRight = true;
            }
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            transform.position = Vector2.MoveTowards(transform.position, a.position, speed * Time.deltaTime);
        }
    }
}
