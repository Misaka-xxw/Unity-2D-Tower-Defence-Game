using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParentMove : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpHigh = 1f;
    public float aSpeed = -0.98f;
    private Vector2 direction;
    private bool isJump = false;
    private Animator anim;
    private Rigidbody2D rb;
    private float velocity_Y;
    public Transform childTransform;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical") * 0.7f;
        if(Input.GetKeyDown(KeyCode.Space)&&!isJump)
        {
            isJump = true;
            ReadyJump();
            rb.AddForce(Vector2.up * jumpHigh);
        }
        Anima();
    }
    private void FixedUpdate()
    {
        Move();
        Jump();//×ÓÎïÌå
    }
    private void ReadyJump()
    {
        velocity_Y = Mathf.Sqrt(jumpHigh * -2f * aSpeed);
    }
    private void Anima()
    {
        anim.SetBool("isJump", isJump);
        if(!isJump )
        {
            if(Mathf.Abs(rb.velocity.x)>=0.05||Mathf.Abs(rb.velocity.y)>=0.05)
            {
                anim.SetBool("isWalk", true);
            }
            else
            {
                anim.SetBool("isWalk", false);
            }
        }
        else
        {
            anim.SetBool("isWalk", false);
        }
    }
    private void Move()
    {
        rb.velocity = direction * moveSpeed * 50 * Time.fixedDeltaTime;
        if(rb.velocity.x>=0.05)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if(rb.velocity.x<=-0.05)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }
    private void Jump()
    {
        velocity_Y += aSpeed * Time.fixedDeltaTime;
        if(childTransform.position.y <= transform.position.y + 0.05f && velocity_Y < 0)
        {
            velocity_Y = 0;
            childTransform.position = transform.position;
            if(childTransform.position == transform.position)
            {
                isJump = false;
            }
        }
        childTransform.Translate(new Vector3(0, velocity_Y) * Time.fixedDeltaTime);
    }
}
