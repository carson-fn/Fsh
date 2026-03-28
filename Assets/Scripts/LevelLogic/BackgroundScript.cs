using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{

    private int TEST_BIOME = 1;

    public SpriteRenderer backgroundRenderer;  
    [SerializeField] private Sprite biome1;
    [SerializeField] private Sprite biome2;
    [SerializeField] private Sprite biome3;

    [SerializeField] private static List<Sprite> backgrounds = new List<Sprite>();

    
    private void changeBackground(Sprite newSprite)
    {
        Debug.Log($"TRYING TO RENDER NEW BACKGROUND SPRITE!!! \n");
        backgroundRenderer.sprite = newSprite;
    }
    public void changeBiome(int biome)
    {
        changeBackground(backgrounds[biome]);
    }

    public BackgroundScript getInstance()
    {
        return this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgrounds.Add(biome1);
        backgrounds.Add(biome2);
        backgrounds.Add(biome3);
        
        //testing loading diff backgrounds
        changeBiome(TEST_BIOME);
    }

    // Update is called once per frame
    void Update()
    {
        //changeBiome(TEST_BIOME);
    }
}
