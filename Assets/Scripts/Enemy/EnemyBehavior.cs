using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float currentSpeed = 0;
    public Vector2 movementInput { get; set; }
    public GameObject overCanvas;
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;
    private bool isFade;
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
        if(movementInput.magnitude > 0.1f && currentSpeed >= 0)
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
        anim.SetBool("isWalk", movementInput.magnitude > 0);
    }
    public void GameOver()
    {
        if (!isFade)
            StartCoroutine(ShowToOverCanvas());
    }
    private IEnumerator ShowToOverCanvas()
    {
        yield return Fade(1);
        overCanvas.SetActive(true);
        Time.timeScale = 0f;
        yield return Fade(0);
    }
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;
        fadeCanvasGroup.blocksRaycasts = true;
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;
        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }
        fadeCanvasGroup.blocksRaycasts = false;
        isFade = false;
    }
}
