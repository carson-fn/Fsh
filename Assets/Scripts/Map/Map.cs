using System;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    // Ray Macros
    private const int RAY_DISTANCE = 10; // distance to camera
    private const int BIOME_LAYER = 1 << 12; // need bitmask from int

    // Biome Names, TODO: make this into an enum?
    private const string POND_NAME = "Pond";
    private const string BEACH_NAME = "Beach";
    private const string ARCTIC_NAME = "Arctic";

    // ------------------------------------------------------

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
                break;
                
            case BEACH_NAME:
                Debug.Log("mouse click hit beach biome, scene switching commence!");

                SceneManager.LoadScene("BeachLevelSelect");
                Debug.Log("scene switched to BeachLevelSelect");
                break;

            case ARCTIC_NAME:
                Debug.Log("mouse click hit arctic biome, scene switching commence!");

                SceneManager.LoadScene("ArcticLevelSelect");
                Debug.Log("scene switched to ArcticLevelSelect");
                break;

            default:
                Debug.Log("hit biome matches no game biome name");
                break;
        }
    }
}
