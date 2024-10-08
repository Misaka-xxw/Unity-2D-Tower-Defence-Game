using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catmove : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public static Transform player;
    private Animator anim;
    private SpriteRenderer sr;
    private bool isGround = true;
    public bool initStatus = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        float dirY = Input.GetAxisRaw("Vertical");
        Vector2 moveDirection = new Vector2(dirX, dirY).normalized;
        if (moveDirection.x > 0)
        {
            sr.flipX = initStatus;
        }
        if (moveDirection.x < 0)
        {
            sr.flipX = !initStatus;
        }
        anim.SetBool("isWalk",moveDirection.magnitude > 0);
        rb.velocity = moveDirection * speed;
        if (Input.GetButtonDown("Jump"))
        {
            //rb.AddForce(Vector2.up*150);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag=="Floor")
        {
            isGround = true;
            anim.SetBool("isJump", false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag=="Floor")
        {
            isGround = false;
            anim.SetBool("isJump", true);
        }
    }
}
