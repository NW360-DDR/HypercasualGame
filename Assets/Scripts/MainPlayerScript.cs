using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MainPlayerScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    Collider2D rb;
    bool moving = false;
    int goingUp = -1;
    public TMP_Text scoreText;
    int score;
    public SpawnManager difficulty;
    public AudioClip[] sounds;
    AudioSource beeps;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Collider2D>();
        IncrementScore(0);
        beeps = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch tap = Input.GetTouch(0);
            if (tap.phase == TouchPhase.Began && moving == false) 
            {
                beeps.PlayOneShot(sounds[0]);
                //Debug.Log("Screen Touched while player is still. Should start moving now.");
                moving = true;
                goingUp *= -1;
            }
        }


        if (moving)
        {
            transform.Translate(Vector2.up * Time.deltaTime * moveSpeed * goingUp);
        }
    }

    private void OnCollisionEnter2D(Collision2D thing)
    {
        string tag = thing.gameObject.tag;

        switch (tag)
        {
            case "Base":
                IncrementScore();
                moving = false;
                beeps.PlayOneShot(sounds[1]);
                break;
            case "Hazard":
                beeps.PlayOneShot(sounds[2]);
                scoreText.text = "Game Over!\nScore: " + score.ToString();
                GameObject.Find("SpawnManager").GetComponent<SpawnManager>().GameOver = true;
                moving = false;
                break;
            case "Plus5":
                beeps.PlayOneShot(sounds[3]);
                IncrementScore(5);
                Destroy(thing.gameObject);
                
                break;
            case "Nuke":
                beeps.PlayOneShot(sounds[3]);
                GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Hazard");
                foreach (GameObject jimmy in taggedObjects) {
                    Destroy(jimmy);
                }
                Destroy(thing.gameObject);
                difficulty.currentTime = -4.0f;
                break;
        }
    }


    private void IncrementScore(int amount = 1)
    {
        score += (amount);
        scoreText.text = score.ToString();
        difficulty.frequency -= 0.02f;
    }
}
