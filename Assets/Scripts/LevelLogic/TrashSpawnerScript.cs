using System.Collections.Generic;
using UnityEngine;

public class TrashSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject Trash1;
    [SerializeField] private GameObject Trash2;
    [SerializeField] private GameObject Trash3;
    [SerializeField] private GameObject Trash4;
    [SerializeField] private static float avgSpawnRate = 3;

    private float trashCountPerSpawn = 1;

    private int NUM_TRASH_TYPES = 4;
    private List<GameObject> trashList = new List<GameObject>();
    private static float totalNumTrashSpawned = 0;
    private static float numTrashCollected = 0;

    private bool firstBossSpawn = false;

    private int BOSS_SPAWN1_NUM_TRASH = 8;

    private float TRASH_GROUP_RADIUS = 0.3f;

    [SerializeField] private float lowestPointY = -4f;
    [SerializeField] private float highestPointY = 0.0f;
    [SerializeField] private float lowestPointX = -6.5f;
    [SerializeField] private float highestPointX = 6.5f;
    [SerializeField] private float spawnRate = 2;

    private int level;
    private float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trashList.Add(Trash1);
        trashList.Add(Trash2);
        trashList.Add(Trash3);
        trashList.Add(Trash4);

        level = LogicScript.getLevel();
        if (LogicScript.getLevel() != LevelScreenLogic.getBOSS_LVL())
        {
            avgSpawnRate = 6 - (level * 1.8f);
        }

        
    }

    public static void setAvgSpawnRate(float rate)
    {
        avgSpawnRate = rate;
    }
    public static float getTotalNumTrashSpawned()
    {
        return totalNumTrashSpawned;
    }
    public static float getNumTrashCollected()
    {
        return numTrashCollected;
    }
    public static void increaseNumTrashCollected(float num)
    {
        numTrashCollected += num;
    }
    public static void decreaseNumTrashCollected(float num)
    {
        numTrashCollected -= num;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(LogicScript.getDead()) && LogicScript.getStartGame())
        {
             if (timer < spawnRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                spawnTrash();
                timer = 0;
                spawnRate = Random.Range(avgSpawnRate - 0.5f, avgSpawnRate + 0.5f);
            }
            
        }
       
        
    }

    void spawnTrash()
    {
        bool bossLvl = LogicScript.getLevel() == LevelScreenLogic.getBOSS_LVL();

        if (bossLvl && !firstBossSpawn)
        {
            firstBossSpawn = true;
            for (int i = 0; i < BOSS_SPAWN1_NUM_TRASH; i++)
            {
                totalNumTrashSpawned++;

                float spawnX = Random.Range(lowestPointX + 1, highestPointX -1);
                float spawnY = Random.Range(lowestPointY + 1, highestPointY -1);

                int spawnIndex = Random.Range(0, (NUM_TRASH_TYPES)); 
                Instantiate(trashList[spawnIndex], 
                new Vector3(Random.Range(spawnX - TRASH_GROUP_RADIUS, spawnX + TRASH_GROUP_RADIUS), 
                Random.Range(spawnY - TRASH_GROUP_RADIUS, spawnY + TRASH_GROUP_RADIUS), transform.position.z), 
                transform.rotation);
            }
            
        }

        totalNumTrashSpawned++;

        int trashSpawnIndex = Random.Range(0, (NUM_TRASH_TYPES)); 
        Instantiate(trashList[trashSpawnIndex], 
        new Vector3(Random.Range(lowestPointX, highestPointX), 
        Random.Range(lowestPointY, highestPointY), transform.position.z), 
        transform.rotation);
        
    }

    
}
