using UnityEngine;
using System.Collections;
public class PlayerControls : MonoBehaviour
{
    //[Header]
    [SerializeField] float movespeed;

    //[Header]
    [SerializeField] float jumpForce;
    [SerializeField] Transform SensorGround;
    [SerializeField] Vector3 sensorSize;
    [SerializeField] LayerMask layerGround;
    [SerializeField] LayerMask layerFruit;
    [SerializeField] float currentJumpTime;
    [SerializeField] float jumpTimeDuration;

    private Vector2 direction;
    private SpriteRenderer spriterenderer;
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private float localGravity = 1f;
    private bool colisionFruit = false;
    private float tempo = 2f;


    void Awake()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        rigidbody2d.gravityScale = localGravity;
    }


    void Update()
    {
        Move();
        Jump();
    }

    void FixedUpdate()
    {
        OnMove();
        OnJump();
    }

    void Move()
    {
        direction = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * movespeed;

        if (direction.x < 0)
        {
            spriterenderer.flipX = true;
        }
        else if (direction.x > 0) 
        {
            spriterenderer.flipX = false;
        }
    }

    void OnMove()
    {
        rigidbody2d.linearVelocity = new Vector2(direction.x, rigidbody2d.linearVelocityY);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && Grounded() == true)
        {
            currentJumpTime = jumpTimeDuration;
        }

        else if (Input.GetButton("Jump") && currentJumpTime > 0) 
        {
            currentJumpTime -= jumpTimeDuration;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            currentJumpTime = 0;
        }
    }

    void OnJump()
    {
        if (currentJumpTime > 0) 
        {
            rigidbody2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            currentJumpTime = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fruit"))
        {
            colisionFruit = true;
            print("colisão feita");
        }
    }

    

    public bool ColisionFruit()
    {
        return colisionFruit == true;
    }


    public int MoveValueX()
    {
        return(int)direction.x;
    }

    public int MoveValueY()
    {
        return (int)direction.y;
    }

    public int JumpValue()
    {
        return (int)rigidbody2d.linearVelocityY;
    }

    public bool Grounded()
    {
        return Physics2D.OverlapBox(SensorGround.position, sensorSize, 0, layerGround);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(SensorGround.position, sensorSize);
    }

}
