using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pond : MonoBehaviour
{
    private const int RAY_DISTANCE = 10; // distance to camera
    private const int BIOME_LAYER = 1 << 8; // need bitmask from int

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
            // mouseClick hit biome
            Debug.Log("mouse click hit pond biome, scene switching commence!");

            SceneManager.LoadScene("PondLevelSelect");
            Debug.Log("scene switched to PondLevelSelect");
        } else
        {
            // mouseClick hit nothing
            Debug.Log("mouse click hit no biome");
        }
    }
}
