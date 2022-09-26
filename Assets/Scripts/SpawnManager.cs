using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;
    int total;
    int randomNum;
    public float frequency;
    float currentTime;
    public bool GameOver = false;
    // Minimum and maximum height for spawning, mess around with in case items spawn that can kill the enemy when not moving. Very bad thing.
    float minH = -3;
    float maxH = 3;
    // Start is called before the first frame update
    void Start()
    {
        // Thanks to the maxExclusive nature of Random.Range, we don't need to worry about overreaching and calling an empty array.
        // As it stands, this isn't really doing anything, but once powerups are implemented, then this will actually be relevant.
        total = enemyPrefabs.Length + powerupPrefabs.Length;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (GameOver)
        {
            Destroy(GameObject.Find("Player"));
            Wait(3);
            SceneManager.LoadScene("SampleScene");
        }
        else if (currentTime >= frequency)
        {
            currentTime = 0;
            randomNum = Random.Range(0, total);
            spawnPrefab(randomNum);
        }
        
    }

    void spawnPrefab(int index)
    {
        // If our generated number is too large for the enemy array, it must be for a powerup, which is yet to be implemented.
        // This if statement assumes that our number is, in fact, small enough.
        if (index < enemyPrefabs.Length)
        {
            float leftRight = Random.Range(0, 1.0f);
            Vector3 newpos;
            // I promise this looks weird but I really don't want to allocate space for another float.
            if (leftRight >= 0.5){ // Going left, starting negative
                leftRight = 1;
                newpos = new Vector2(3, Random.Range(minH, maxH));
            }
            else // Going right, starting positive
            {
                leftRight = -1;
                newpos = new Vector2(-3, Random.Range(minH, maxH));
            }
            GameObject newThing = Instantiate(enemyPrefabs[index], newpos, enemyPrefabs[index].transform.rotation);
            PrefabMove scrip = newThing.GetComponent<PrefabMove>();
            scrip.WhichSide = ((int)leftRight);
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
