using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    public Collider2D coll;
    public float speed = 5;
    public float jumpforce = 5;

    public Transform groundCheck;
    public LayerMask ground;

    public Text scoreText;

    public bool isGround, isJumpping, isFalling;
    public bool jumpPressed = false;
    int jumpCount = 2;

    private static int totalScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        Move();
        Jump();
        Anim();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Door")
        {
            totalScore = 0;
            GotoLevel2();
            
        }

        if (collision.tag == "Trap")
        {
            Debug.Log("Dead, retry");
            totalScore = 0;
            Restart();
        }
    }


    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GotoLevel2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Move()
    {
        float horizontalmove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalmove * speed, rb.velocity.y);

        if(horizontalmove != 0)
        {
            transform.localScale = new Vector3(horizontalmove, 1, 1);
        }
    }

    void Jump()
    {
        if (isGround)
        {
            jumpCount = 2;
            isJumpping = false;
            isFalling = false;
        }

        if (jumpPressed && jumpCount > 0)
        {
            isJumpping = true;
            isFalling = false;
            jumpPressed = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            jumpCount--;
        }
        else if (!isGround && rb.velocity.y > 0.05)
        {
            isJumpping = true;
            isFalling = false;
        }
        else if (!isGround && rb.velocity.y < 0)
        {
            isFalling = true;
            isJumpping = false;
        }
    }

    void Anim()
    {
        animator.SetFloat("running", Mathf.Abs(rb.velocity.x));
        animator.SetBool("jumpping", isJumpping);
        animator.SetBool("falling", isFalling);
    }

    public void AddScore(int score)
    {
        totalScore += score;
        UpdateScoreText();
    }

    public int GetScore()
    {
        return totalScore;
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + totalScore.ToString();
    }
}
