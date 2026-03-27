using System.Collections.Generic;
using UnityEngine;

public class LeftFishSpawnerScript : MonoBehaviour
{
    public GameObject leftFish1;
    public GameObject leftFish2;
    public GameObject leftFish3;
    public GameObject leftFish4;

    private int NUM_TYPES_FISH = 3;
    private List<GameObject> fishes = new List<GameObject>();
    [SerializeField] private float spawnRate = 8;
    [SerializeField] private static float avgSpawnRate = 8;
    private float timer = 0;
    [SerializeField] private float lowestPoint = -3.4f;
    [SerializeField] private float highestPoint = -2.0f;

    private static LeftFishSpawnerScript Instance;
    private int level;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Instance = this;

        fishes.Add(leftFish1);
        fishes.Add(leftFish2);
        fishes.Add(leftFish3);
        fishes.Add(leftFish4);
        
        Debug.Log("ADDED FISHES");
        Debug.Log(fishes.Count);
        //spawnLeftFish();
         // just for now, we can make better later
        // bc spawn rate atm is like how much time between spawn, not fish per time 
    }

    public static LeftFishSpawnerScript getInstance()
    {
        return Instance;
    }

    public static void setAvgSpawnRate(int rate)
    {
        Debug.Log($"setAvgSpawnRate called with: {rate}");
        Debug.Log($"SET AVG SPAWN RATE TO {rate}\n");
        avgSpawnRate = rate;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"level: {level}, BOSS_LVL: {LevelScreenLogic.getBOSS_LVL()}, match: {level == LevelScreenLogic.getBOSS_LVL()}");

        level = LogicScript.getLevel();
        if (LogicScript.getLevel() != LevelScreenLogic.getBOSS_LVL())
        {
            avgSpawnRate = 10 - (level * 2f);
            Debug.Log($"NOT BOSS LVL AVG SPAWN RATE IS {avgSpawnRate}");
        }

        if (!(LogicScript.getDead()) && LogicScript.getStartGame())
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
                Debug.Log($"SPAWN RATE IS  {spawnRate}\n");
            }

            
        }
        
        
    }
    void spawnLeftFish()
    {
        int fishSpawnIndex = Random.Range(0, level); 
        
        Instantiate(fishes[fishSpawnIndex], 
        new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), transform.position.z), 
        transform.rotation);
    }
}
