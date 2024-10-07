using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float currentSpeed = 0;
    public Vector2 movementInput { get; set; }
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Move();
        SetAnimation();
    }
    private void Move()
    {
        if(movementInput.magnitude>0.1f&&currentSpeed>=0)
        {
            rb.velocity=movementInput*currentSpeed;
            if(movementInput.x < 0)
            {
                sr.flipX = false;
            }
            if(movementInput.x > 0)
            {
                sr.flipX = true;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    private void SetAnimation()
    {
        //anim.SetBool("isWalk", movementInput.magnitude > 0);
    }
}
