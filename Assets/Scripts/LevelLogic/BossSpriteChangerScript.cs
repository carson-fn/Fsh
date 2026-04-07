using UnityEngine;
using System.Collections.Generic;

public class BossSpriteChangerScript : MonoBehaviour
{

    [SerializeField] private Sprite frogSprite;
    [SerializeField] private Sprite crabSprite;
    [SerializeField] private Sprite penguinSprite;

    private SpriteRenderer sr;
    private Vector3 originalScale;
    private List<Sprite> biomeSprites = new List<Sprite>(); 

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
        // add the boss sprites here !!!
        biomeSprites.Add(frogSprite);
        biomeSprites.Add(penguinSprite);
        biomeSprites.Add(crabSprite);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        changeSprite(LogicScript.getBiome());
    }
    public void changeSprite(int biome)
    {
        Sprite oldSprite = sr.sprite;
        Sprite newSprite = biomeSprites[biome - 1]; // biome 1 - 3
        sr.sprite = newSprite;

        // try to fix size of sprite so all same 
        if (oldSprite != null && newSprite != null)
        {
            float scaleX = oldSprite.bounds.size.x / newSprite.bounds.size.x;
            float scaleY = oldSprite.bounds.size.y / newSprite.bounds.size.y;
            transform.localScale = new Vector3(
                originalScale.x * scaleX,
                originalScale.y * scaleY,
                originalScale.z
            );
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
