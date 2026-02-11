using System.Collections.Generic;
using UnityEngine;

public class TrashSpawnerScript : MonoBehaviour
{
    public GameObject Trash1;

    [SerializeField] private float lowestPointY = -4f;
    [SerializeField] private float highestPointY = 0.5f;
    [SerializeField] private float lowestPointX = -6.5f;
    [SerializeField] private float highestPointX = 6.5f;
    [SerializeField] private float spawnRate = 2;

    private int level;
    private float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        level = LogicScript.getLevel(); // not rly doing anything w this for now ... 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnTrash();
            timer = 0;
            spawnRate = Random.Range(3, 6);
        }
        
    }

    void spawnTrash()
    {
        
        Instantiate(Trash1, 
        new Vector3(Random.Range(lowestPointX, highestPointX), 
        Random.Range(lowestPointY, highestPointY), transform.position.z), 
        transform.rotation);
    }
}
