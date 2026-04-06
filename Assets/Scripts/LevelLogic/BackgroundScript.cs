using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{

    private int TEST_BIOME = 2;

    public static SpriteRenderer backgroundRenderer;  
    [SerializeField] private Sprite biome1;
    [SerializeField] private Sprite biome2;
    [SerializeField] private Sprite biome3;

    [SerializeField] private static List<Sprite> backgrounds = new List<Sprite>();


    
    private static void changeBackground(Sprite newSprite)
    {
        Debug.Log($"TRYING TO RENDER NEW BACKGROUND SPRITE!!! \n");
        backgroundRenderer.sprite = newSprite;
    }
    public static void changeBiome(int biome)
    {
        changeBackground(backgrounds[biome - 1]); // 1 - 3
    }

    public BackgroundScript getInstance()
    {
        return this;
    }
    void Awake()
    {
        backgroundRenderer = GetComponent<SpriteRenderer>();
        backgrounds.Clear();
        backgrounds.Add(biome1);
        backgrounds.Add(biome2);
        backgrounds.Add(biome3);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgrounds.Add(biome1);
        backgrounds.Add(biome2);
        backgrounds.Add(biome3);
        
        //testing loading diff backgrounds
        Debug.Log($"CHANGING BACKGROUND RN for biome {LogicScript.getBiome()}\n");
        changeBiome(LogicScript.getBiome());
    }

    // Update is called once per frame
    void Update()
    {
        //changeBiome(LogicScript.getBiome());
    }
}
