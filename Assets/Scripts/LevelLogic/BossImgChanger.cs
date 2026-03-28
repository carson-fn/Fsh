using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossImgChanger : MonoBehaviour
{
    [SerializeField] private Sprite frogSprite;
    [SerializeField] private Sprite crabSprite;
    [SerializeField] private GameObject img1;
    [SerializeField] private GameObject img2;
    [SerializeField] private GameObject img3;

    [SerializeField] private SpriteRenderer backgroundSpriteRenderer;  
    private static SpriteRenderer spriteRenderer; 
    private static int NUM_IMGS = 3;

    private static List<Image> images = new List<Image>();
    private static List<Sprite> bossSprites = new List<Sprite>();
    private Image biomeImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Debug.Log($"frogSprite: {frogSprite}");
        Debug.Log($"crabSprite: {crabSprite}");
        Debug.Log($"img1: {img1}");
        Debug.Log($"img2: {img2}");
        Debug.Log($"img3: {img3}");
        
        if (img1 != null) Debug.Log($"img1 Image component: {img1.GetComponent<Image>()}");
        if (img2 != null) Debug.Log($"img2 Image component: {img2.GetComponent<Image>()}");
        if (img3 != null) Debug.Log($"img3 Image component: {img3.GetComponent<Image>()}");

        images.Clear();      // ADD THIS
        bossSprites.Clear();

        spriteRenderer = backgroundSpriteRenderer;
        bossSprites.Add(frogSprite);
        bossSprites.Add(crabSprite);
        biomeImage = GetComponent<Image>();
        images.Add(img1.GetComponent<Image>());
        images.Add(img2.GetComponent<Image>());
        images.Add(img3.GetComponent<Image>());

    }
    void Start()
    {
        
        //BossImgChanger.changeBossImage(2);
        
    }
    public static void changeBossImage(int biome)
    {
        Debug.Log($"changeBossImage called with biome: {biome}, list size: {bossSprites.Count}");
        Debug.Log($"biome: {biome}, index: {biome - 1}, bossSprites count: {bossSprites.Count}");
        Debug.Log($"sprite at index: {bossSprites[biome - 1]}");


        for (int i = 0; i < NUM_IMGS; i++)
        {
            images[i].sprite = bossSprites[biome - 1];//assuming biomes start at 0, not 1
        }   
        //spriteRenderer.sprite = bossSprites[biome - 1]; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
