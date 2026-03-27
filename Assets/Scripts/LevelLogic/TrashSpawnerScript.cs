using System.Collections.Generic;
using UnityEngine;

public class TrashSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject Trash1;
    [SerializeField] private GameObject Trash2;
    [SerializeField] private GameObject Trash3;
    [SerializeField] private GameObject Trash4;
    [SerializeField] private float avgSpawnRate = 3;

    private int NUM_TRASH_TYPES = 4;
    private List<GameObject> trashList = new List<GameObject>();
    private static int totalNumTrashSpawned = 0;
    private static int numTrashCollected = 0;

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
        level = LogicScript.getLevel(); // not rly doing anything w this for now ... 
        trashList.Add(Trash1);
        trashList.Add(Trash2);
        trashList.Add(Trash3);
        trashList.Add(Trash4);

        level = LogicScript.getLevel();
        if (LogicScript.getLevel() != LevelScreenLogic.getBOSS_LVL())
        {
            avgSpawnRate = 14 - (level * level);
        }
        
    }
    public static int getTotalNumTrashSpawned()
    {
        return totalNumTrashSpawned;
    }
    public static int getNumTrashCollected()
    {
        return numTrashCollected;
    }
    public static void increaseNumTrashCollected(int num)
    {
        numTrashCollected += num;
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
                spawnRate = Random.Range(avgSpawnRate - 1, avgSpawnRate + 1);
            }
            
        }
       
        
    }

    void spawnTrash()
    {
        totalNumTrashSpawned++;

        int trashSpawnIndex = Random.Range(0, (NUM_TRASH_TYPES)); 
        Instantiate(trashList[trashSpawnIndex], 
        new Vector3(Random.Range(lowestPointX, highestPointX), 
        Random.Range(lowestPointY, highestPointY), transform.position.z), 
        transform.rotation);
    }

    
}
