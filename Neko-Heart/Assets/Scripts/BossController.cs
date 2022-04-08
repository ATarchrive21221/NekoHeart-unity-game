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
    public Text lifeRemain;
    public Text lostlife;
    public Text winText;
    public bool canMove;
    public int life = 3;
    public int attackMode = 0;
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
        lifeRemain.text = "O\tO\tO";
        lostlife.text = "";
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

        if(attackMode > 5)
        {
            attackMode = 0;
        }
        if (life == 0)
        {
            canMove = false;
            //transform.position = transform.position;
            anim.Play("BossDead");
            StartCoroutine(waitEnd());
            winText.text = "The cat finally defeated the demon guard and get the dog potion\n now he can become a dog and marry the dog princess\n But will the dog princess still love him?";
        }

        if (canMove && canFire)
        {
            
            Vector3 offset = new Vector3(0f, -2f, 0f);
            anim.Play("BossAttack");
            
            GameObject b = Instantiate(FireBall, new Vector3(0f, 0f, 0f), Quaternion.identity);

            b.GetComponent<FireBallController>().InitPosition(transform.position + offset, new Vector3(0f, -2f, 0f));
            canFire = false;
            attackMode++;
            StartCoroutine(PlayerCanFireAgain());
            StartCoroutine(waitwalk());

        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnerFire")
        {
            lostlife.text = lostlife.text + "X\t";
            Debug.Log("Hit Boss");
            life--;
            if(life != 0)
            {
                anim.Play("BossHit");
            }
        }
    }

    IEnumerator PlayerCanFireAgain()
    {
        if(attackMode < 4)
        {
            yield return new WaitForSecondsRealtime(2);
        }
        else
        {
            yield return new WaitForSecondsRealtime(1);
        }
        canFire = true;
    }

    IEnumerator waitEnd()
    {
        yield return new WaitForSecondsRealtime(2);
        Destroy(gameObject);
    }

    IEnumerator waitwalk() { 
        yield return new WaitForSeconds(0.4f);
        anim.Play("BossWalk");
    }
}
