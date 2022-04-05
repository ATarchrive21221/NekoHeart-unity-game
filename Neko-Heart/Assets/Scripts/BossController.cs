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
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(1f, 0f, 0f);
        rend = GetComponent<SpriteRenderer>();
        canFire = true;
        status = true;
        message.text = "";
        winText.text = "";
        count = 0;
    }

    // Update is called once per frame
    void Update()
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

        if (canFire)
        {
            //the offset 
            Vector3 offset = new Vector3(0f, -2f, 0f);

            //creates a bullet that is pointing in the opposite direction... pick the one you need  
            GameObject b = Instantiate(FireBall, new Vector3(0f, 0f, 0f), Quaternion.identity);

            b.GetComponent<FireBallController>().InitPosition(transform.position + offset, new Vector3(0f, -2f, 0f));
            canFire = false;

            //this starts a coroutine... a non-blocking function
            StartCoroutine(PlayerCanFireAgain());
        }
    }

    IEnumerator PlayerCanFireAgain()
    {
        //this will pause the execution of this method for 3 seconds without blocking
        yield return new WaitForSecondsRealtime(2);
        canFire = true;
    }
}
