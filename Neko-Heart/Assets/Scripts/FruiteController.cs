using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruiteController : MonoBehaviour
{
    public int score = 0;
    public AudioSource pickupSound;
    private int apples = 0; // 10 apples
    private int bananas = 0; // 9 bananas


    // Start is called before the first frame update
    void Start()
    {
        pickupSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (gameObject.tag == "Apple")
        {
            apples++;
            FindObjectOfType<GameController>().AddApples(this.apples);
        }
        if (gameObject.tag == "Banana")
        {
            bananas++;
            FindObjectOfType<GameController>().AddBananas(this.bananas);
        }
        FindObjectOfType<PlayerController>().AddScore(this.score);

        Destroy(this.gameObject);

        pickupSound.Play();
    }

    public int getApples()
    {
        return apples;
    }

    public int getBananas()
    {
        return bananas;
    }
}
