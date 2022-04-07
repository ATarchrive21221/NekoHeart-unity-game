using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    private Vector3 velocity;
    private SpriteRenderer rend;
    private bool canFire;
    private bool status;
    //private Rigidbody rb;
    public static int count;
    public GameObject FireBall = null;
    public float speed = 2.0f;
    public Text message;
    public Text winText;
    public bool canMove;
    public int life = 3;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        velocity = new Vector3(1f, 0f, 0f);
        rend = GetComponent<SpriteRenderer>();
        canFire = true;
        status = true;
        canMove = true;
        message.text = "";
        winText.text = "";
        count = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            int change = Random.Range(0, 150);
            
            if (change == 0)
            {
                velocity = new Vector3(-velocity.x, 0f, 0f);
            }
            else if (change == 50)
            {
                velocity = new Vector3(velocity.x, 0f, 0f);
            }

            if (!transform.hasChanged && velocity.x < 0f)
            {
                velocity = new Vector3(-velocity.x, 0f, 0f);
            }
            if (!transform.hasChanged && velocity.x > 0f)
            {
                velocity = new Vector3(-velocity.x, 0f, 0f);
            }
            transform.position = transform.position + velocity * Time.deltaTime * speed;
        }

        if (life == 0)
        {
            canMove = false;
            //transform.position = transform.position;
            
            StartCoroutine(waitEnd());
            Destroy(gameObject);
        }

        if (canFire)
        {
            //the offset 
            Vector3 offset = new Vector3(0f, -2f, 0f);
            anim.Play("BossAttack");
            //creates a bullet that is pointing in the opposite direction... pick the one you need  
            GameObject b = Instantiate(FireBall, new Vector3(0f, 0f, 0f), Quaternion.identity);

            b.GetComponent<FireBallController>().InitPosition(transform.position + offset, new Vector3(0f, -2f, 0f));
            canFire = false;

            //this starts a coroutine... a non-blocking function
            StartCoroutine(PlayerCanFireAgain());
            StartCoroutine(waitwalk());

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnerFire")
        {
            Debug.Log("Hit Boss");
            life--;
            if(life != 0)
            {
                anim.Play("BossHit");
            }
            else
            {
                anim.Play("BossDead");
            }
        }
        //
    }

    IEnumerator PlayerCanFireAgain()
    {
        //this will pause the execution of this method for 2 seconds without blocking
        yield return new WaitForSecondsRealtime(2);
        canFire = true;
    }

    IEnumerator waitEnd()
    {
        //this will pause the execution of this method for 3 seconds without blocking
        yield return new WaitForSecondsRealtime(3);
    }

    IEnumerator waitwalk() { 
        yield return new WaitForSecondsRealtime(1);
        anim.Play("BossWalk");
    }
}
