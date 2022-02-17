using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    enum direction
    {
        idle,
        walk,
        jump,
        falling,
        climb_idle,
        climb_down,
        climb_up
    };
    public float speed;
    public float jumpForce;
    public Vector2 offset_right;
    public Vector2 offset_left;
    public GameObject dynamite;
    bool isJumping;
    bool isClimbing;
    bool nearRope;
    bool nearDynamite;
    bool nearWall;
    bool picked;
    bool pressedSpace;
    static public bool used;
    Vector2 input;
    Rigidbody2D rb;
    Collider2D col;
    Animator anim;
    SpriteRenderer sprite;

    direction dir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Rope")
        {
            offset_left.x = col.gameObject.transform.position.x + this.col.bounds.extents.x / 2;
            offset_right.x = col.gameObject.transform.position.x - this.col.bounds.extents.x / 2 - 0.3f;
            nearRope = true;
        }
        else if (col.tag == "Dynamite")
        {
            nearDynamite = true;
        }
        else if (col.tag == "Lava")
        {
            SceneManager.LoadScene(2);
        }
        else if (col.tag == "WallFromPlayer")
        { nearWall = true; }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Rope")
        {
            nearRope = true;
        }
        else if (col.tag == "Dynamite")
        {
            nearDynamite = true;
            if (picked == true)
            {
                Destroy(col.gameObject);
                nearDynamite = false;
            }
        }

    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Rope")
        {
            nearRope = false;
        }
        else if (col.tag == "Dynamite")
        {
            nearDynamite = false;
        }
        else if (col.tag == "WallFromPlayer")
        { nearWall = false; }

    }
    void Update()
    {
        KeyboardInput();
        UpdateAnimation();

    }
    void FixedUpdate()
    {
        Movement();


    }
    void KeyboardInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        isJumping = !isGrounded();
        if (Input.GetKeyDown("space") && (!isJumping || isClimbing))
        {
            pressedSpace = true;
        }

        else
        {
            if (Input.GetKeyDown("e"))
            {
                if (nearRope)
                {
                    if (isClimbing)
                    {
                        isClimbing = false;
                        isJumping = true;
                        rb.gravityScale = 4;
                    }
                    else
                    {
                        isClimbing = true;
                        if (sprite.flipX == false)
                        {
                            offset_left.y = this.transform.position.y;
                            this.transform.position = offset_left;
                        }
                        else
                        {
                            offset_right.y = this.transform.position.y;
                            this.transform.position = offset_right;
                        }
                        rb.gravityScale = 0;
                    }
                }
                else if (nearDynamite && !used)
                {
                    picked = true;
                }
                else if (picked && !used && nearWall)
                {
                    //  Vector2 pos = new Vector2(col.bounds.center.x, col.bounds.center.y - col.bounds.extents.y + 0.1f);
                    Vector2 pos = this.transform.position;
                    Quaternion rot = this.transform.rotation;
                    GameObject dyn;
                    dyn = Instantiate(dynamite, pos, rot) as GameObject;
                    Dynamite dyna = dyn.GetComponent<Dynamite>();
                    dyna.toExplode = true;
                    picked = false;
                    used = true;
                }
            }
        }
    }
    void Movement()
    {
        if (isClimbing == false)
        {
            Vector2 movement = rb.velocity;
            movement.x = input.x * speed;
            rb.velocity = movement;

            if (pressedSpace && isJumping == false)
            {
                isJumping = true;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                pressedSpace = false;

            }
        }
        else
        {
            if (pressedSpace)
            {
                Vector2 movement = rb.velocity;
                movement.x = input.x * speed;
                rb.velocity = movement;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isClimbing = false;
                rb.gravityScale = 4;
                pressedSpace = false;

            }
            else
            {
                Vector2 movement = Vector2.zero;
                movement.y = input.y * speed;
                rb.velocity = movement;
            }
        }
    }
    void UpdateAnimation()
    {
        FlipSprite();
        if (isClimbing == true)
        {
            if (rb.velocity.y > 0)
            {
                dir = direction.climb_up;
            }
            else if (rb.velocity.y < 0)
            {
                dir = direction.climb_down;
            }
            else
            {
                dir = direction.climb_idle;
            }
        }
        else
        {
            if(rb.velocity.y<0) dir = direction.falling;
            if (isJumping == true)
            {
                if (rb.velocity.y > 0)
                {
                    dir = direction.jump;
                }

            }
            else
            {
                if (input.x == 0)
                {
                    dir = direction.idle;
                }
                else
                {
                    dir = direction.walk;
                }
            }
        }

        anim.SetInteger("dir", (int)dir);
    }
    void FlipSprite()
    {
        if (!isClimbing)
        {
            if (input.x < 0)
            {
                sprite.flipX = false;
            }
            else if (input.x > 0)
            {
                sprite.flipX = true;
            }
        }
        else
        {
            if (input.x < 0)
            {
                sprite.flipX = true;
                offset_right.y = this.transform.position.y;
                this.transform.position = offset_right;
            }
            else if (input.x > 0)
            {
                sprite.flipX = false;
                offset_left.y = this.transform.position.y;

                this.transform.position = offset_left;
            }
        }
    }
    bool isGrounded()
    {

        return Physics2D.Raycast(new Vector2(col.bounds.center.x - col.bounds.extents.x, col.bounds.center.y), -transform.up, col.bounds.extents.y + 0.1f) || Physics2D.Raycast(new Vector2(col.bounds.center.x + col.bounds.extents.x, col.bounds.center.y), -transform.up, col.bounds.extents.y + 0.1f);
    }
}
