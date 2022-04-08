using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundController : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip clip1;
    private AudioClip clip2;
    private AudioClip clip3;
    Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = clip1;
        audioSource.Play();
    }

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        /*
        scene = SceneManager.GetActiveScene();
        if (scene.name == "Lv1-1")
        {
            audioSource.Stop();
            audioSource.clip = clip2;
            audioSource.Play();
        }
        */
    }
}
