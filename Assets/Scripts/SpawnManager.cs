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
    public float currentTime = -3.0f;
    public bool GameOver = false;
    bool Unbroken = false;
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
            StartCoroutine(Wait(3));
        }
        else if (currentTime >= frequency)
        {
            Unbroken = true;
            currentTime = 0;
            randomNum = Random.Range(0, total);
            spawnPrefab(randomNum);
        }
        
    }

    void spawnPrefab(int index)
    {
        while (Unbroken){

            // If our generated number is too large for the enemy array, it must be for a powerup.
            // This if statement assumes that the combined totals are, in fact, small enough to not overflow the int data type, which I bleeping hope so.
            if (index < enemyPrefabs.Length)
            {
                float leftRight = Random.Range(0, 1.0f);
                Vector3 newpos;
                // I promise this looks weird but I really don't want to allocate space for another float.
                if (leftRight >= 0.5) { // Going left, starting negative
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
                Unbroken = false;
            }
            else
            {
                if (Random.Range(1, 10) <= 3) // Just in case powerups get broken, when they are called they have a 70% chance to instead become a random enemy item.
                {
                    float leftRight = Random.Range(0, 1.0f);
                    Vector3 newpos;
                    // I promise this looks weird but I really don't want to allocate space for another float.
                    if (leftRight >= 0.5)
                    { // Going left, starting negative
                        leftRight = 1;
                        newpos = new Vector2(3, Random.Range(minH, maxH));
                    }
                    else // Going right, starting positive
                    {
                        leftRight = -1;
                        newpos = new Vector2(-3, Random.Range(minH, maxH));
                    }
                    GameObject newThing = Instantiate(powerupPrefabs[index - enemyPrefabs.Length], newpos, powerupPrefabs[index - enemyPrefabs.Length].transform.rotation);
                    PrefabMove scrip = newThing.GetComponent<PrefabMove>();
                    scrip.WhichSide = ((int)leftRight);
                    Unbroken = false;
                }
                else
                {
                    Unbroken = true;
                }
            }
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("SampleScene");
    }

}
