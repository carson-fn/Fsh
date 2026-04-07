using System;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    // NOTE: include LogicScript.cs
    private LogicScript LogicScript;

    // -------------------------------------------------------

    // Ray Macros
    private const int RAY_DISTANCE = 10; // distance to camera
    private const int BIOME_LAYER = 1 << 12; // need bitmask from int

    // Biome Names, TODO: make this into an enum?
    private const string POND_NAME = "Pond";
    private const string BEACH_NAME = "Beach";
    private const string ARCTIC_NAME = "Arctic";

    // NOTE: since biomes not buttons, call setBiome() within here
    // NOTE: hrm, doesn't work, call within level select
    // Biome Numbers
    private const int POND_NUM = 1;
    private const int BEACH_NUM = 3;
    private const int ARCTIC_NUM = 2;

    // ------------------------------------------------------
    void Start()
    {
        getLogicScript();
    }

    private void getLogicScript()
    {
        LogicScript = GameObject.Find("Biome Manager").GetComponent<LogicScript>(); // get script from manager game object

        if (LogicScript != null)
            Debug.Log("LogicScript found on this game object!");
        else
            Debug.LogWarning("LogicScript not found on this GameObject D:");
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TrySelectBiome();
        }
    }

    private void TrySelectBiome()
    {
        Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition); // generate ray from mouse click
        RaycastHit2D hit = Physics2D.GetRayIntersection(clickRay, RAY_DISTANCE, BIOME_LAYER);

        if (hit.collider != null)
        {
            // mouseClick hit a biome
            Debug.Log("mouse click hit a biome");

            DifferentiateBiome(hit);
        } else
        {
            // mouseClick hit nothing
            Debug.Log("mouse click hit no biome");
        }
    }

    private void DifferentiateBiome(RaycastHit2D hit)
    {
        string hitBiomeName = hit.collider.name;

        switch (hitBiomeName)
        {
            case POND_NAME:
                Debug.Log("mouse click hit pond biome, scene switching commence!");

                SceneManager.LoadScene("PondLevelSelect");
                Debug.Log("scene switched to PondLevelSelect");

                LogicScript.setBiome(POND_NUM);
                Debug.Log("set biome in LogicScript.cs to " + POND_NUM);

                break;
                
            case BEACH_NAME:
                Debug.Log("mouse click hit beach biome, scene switching commence!");

                SceneManager.LoadScene("BeachLevelSelect");
                Debug.Log("scene switched to BeachLevelSelect");

                LogicScript.setBiome(BEACH_NUM);
                Debug.Log("set biome in LogicScript.cs to " + BEACH_NUM);

                break;

            case ARCTIC_NAME:
                Debug.Log("mouse click hit arctic biome, scene switching commence!");

                SceneManager.LoadScene("ArcticLevelSelect");
                Debug.Log("scene switched to ArcticLevelSelect");

                LogicScript.setBiome(ARCTIC_NUM);
                Debug.Log("set biome in LogicScript.cs to " + ARCTIC_NUM);

                break;

            default:
                Debug.Log("hit biome matches no game biome name");
                break;
        }
    }
}
