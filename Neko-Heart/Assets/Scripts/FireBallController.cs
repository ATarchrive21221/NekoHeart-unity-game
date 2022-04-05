using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour
{
    public float speed = 2f;

    public Vector3 velocity;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (velocity != null)
            transform.Translate(velocity * Time.deltaTime * speed);

        // this will make more sense after we discuss vectors and 3D
        float dist = (transform.position - Camera.main.transform.position).z;
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;


        //destroy bullet when it leaves the screen
        if (transform.position.y < bottomBorder)
        {
            Destroy(gameObject);
        }

    }

    public void InitPosition(Vector3 p, Vector3 v)
    {
        transform.position = p;
        velocity = v;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Hit player");
            Destroy(gameObject);
        }
        //
    }
}
