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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Collider2D>();
        IncrementScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch tap = Input.GetTouch(0);
            if (tap.phase == TouchPhase.Began && moving == false) 
            {
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
        //Debug.Log("Ayo collision.");
        moving = false;
        // Run if this is one of the bases that we need to actually land on.
        if (thing.gameObject.CompareTag("Base"))
        {
            Debug.Log("Ayo based?.");
            IncrementScore();
        }
        else if (thing.gameObject.CompareTag("Hazard"))
        {
            scoreText.text = "Game Over! Score: " + score.ToString();
            GameObject.Find("SpawnManager").GetComponent<SpawnManager>().GameOver = true;
        }
    }


    private void IncrementScore(int amount = 1)
    {
        score += (amount);
        scoreText.text = score.ToString();
    }
}
