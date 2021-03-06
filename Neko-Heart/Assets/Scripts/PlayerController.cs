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
    public Text gameOver;
    public Text energyBall;

    public bool isGround, isJumpping, isFalling;
    public bool jumpPressed = false;
    public bool canFire = true;
    int jumpCount = 2;

    private static int totalScore = 0;
    private static int fruit = 0;
    private int energyGain = 0;
    public GameObject enerFire = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        UpdateScoreText();
        gameOver.text = "";
        energyBall.text = "Energy Ball: 0";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }

        if (Input.GetButtonDown("Vertical") && energyGain > 0 && canFire)
        {
            //the offset 
            Vector3 offset = new Vector3(0f, 2f, 0f);

            //create a bullet pointing in its natural direction 
            GameObject b = Instantiate(enerFire, new Vector3(0f, 0f, 0f), Quaternion.identity);

            b.GetComponent<EnerFireController>().InitPosition(transform.position + offset, new Vector3(0f, 2f, 0f));
            energyGain--;
            energyBall.text = "Energy Ball: " + energyGain;
            canFire = false;
            StartCoroutine(PlayerCanFireAgain());
        }

        
    }

    IEnumerator PlayerCanFireAgain()
    {
        yield return new WaitForSecondsRealtime(1);
        canFire = true;
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
            SetFruitToZero();
            GotoLevel2();
        }

        if (collision.tag == "Trap")
        {
            Debug.Log("Dead, retry");
            totalScore = 0;
            Restart();
        }

        if (collision.tag == "Fruit")
        {
            fruit++;
        }

        if (collision.tag == "Energy")
        {
            energyGain++;
            energyBall.text = "Energy Ball: " + energyGain;
        }

        if (collision.tag == "FireBall")
        {
            gameOver.text = "GameOver!\nPress 'R' to restart";
            Destroy(gameObject);
        }
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GotoLevel2()
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

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + totalScore.ToString();
    }

    public int GetFruit()
    {
        return fruit;
    }

    public void SetFruitToZero()
    {
        fruit = 0;
    }
}
