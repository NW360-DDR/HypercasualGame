using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabMove : MonoBehaviour
{
    float moveSpeed = 2.5f;
    // Will be 1 if going left, will be -1 if going right.
    public int WhichSide;

    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime * WhichSide, Space.World);
        // if the game is in fact over, commit dead.
        if (GameObject.Find("SpawnManager").GetComponent<SpawnManager>().GameOver || Mathf.Abs(transform.position.x) > 4)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D thing)
    {
        if (thing.gameObject.CompareTag("Player")){
            Destroy(gameObject);
        }
    }


}
