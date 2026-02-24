using System.Collections.Generic;
using UnityEngine;

public class LeftFishSpawnerScript : MonoBehaviour
{
    public GameObject leftFish1;
    public GameObject leftFish2;
    public GameObject leftFish3;
    public GameObject leftFish4;
    public List<GameObject> fishes;
    [SerializeField] private float spawnRate = 2;
    [SerializeField] private float avgSpawnRate;
    private float timer = 0;
    [SerializeField] private float lowestPoint = -3.4f;
    [SerializeField] private float highestPoint = -1.0f;

    private int level;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fishes.Add(leftFish1);
        fishes.Add(leftFish2);
        fishes.Add(leftFish3);
        fishes.Add(leftFish4);
        Debug.Log("ADDED FISHES");
        Debug.Log(fishes.Count);
        spawnLeftFish();
        level = LogicScript.getLevel();
        avgSpawnRate = 14 - (level * level); // just for now, we can make better later
        // bc spawn rate atm is like how much time between spawn, not fish per time 
    }

    // Update is called once per frame
    void Update()
    {

        if (!(LogicScript.getDead()))
        {
            if (timer < spawnRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                spawnLeftFish();
                timer = 0;
                spawnRate = Random.Range(avgSpawnRate - 1, avgSpawnRate + 1);
            }

            
        }
        
        
    }
    void spawnLeftFish()
    {
        int fishSpawnIndex = Random.Range(0, (level + 1)); 
        
        Instantiate(fishes[fishSpawnIndex], 
        new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), transform.position.z), 
        transform.rotation);
    }
}
