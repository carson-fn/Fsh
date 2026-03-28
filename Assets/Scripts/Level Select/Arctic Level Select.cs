using System;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcticLevelSelect : MonoBehaviour
{
    // Ray Macros
    private const int RAY_DISTANCE = 10; // distance to camera
    private const int LEVEL_SELECT_LAYER = 1 << 7; // need bitmask from int

    // Level Names, TODO: make this into an enum?
    private const string LEVEL_1_NAME = "Level 1";
    private const string LEVEL_2_NAME = "Level 2";
    private const string LEVEL_3_NAME = "Level 3";

    // ------------------------------------------------------

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TrySelectLevel();
        }
    }

    private void TrySelectLevel()
    {
        Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition); // generate ray from mouse click
        RaycastHit2D hit = Physics2D.GetRayIntersection(clickRay, RAY_DISTANCE, LEVEL_SELECT_LAYER);

        if (hit.collider != null)
        {
            // mouseClick hit a level
            Debug.Log("mouse click hit a level");

            DifferentiateLevel(hit);
        } else
        {
            // mouseClick hit nothing
            Debug.Log("mouse click hit no level");
        }
    }

    private void DifferentiateLevel(RaycastHit2D hit)
    {
        string hitLevelName = hit.collider.name;

        switch (hitLevelName)
        {
            case LEVEL_1_NAME:
                Debug.Log("mouse click hit level 1, scene switching commence!");

                SceneManager.LoadScene("lvl1name");
                Debug.Log("scene switched to lvl1name");
                break;

            case LEVEL_2_NAME:
                Debug.Log("mouse click hit level 2, scene switching commence!");

                SceneManager.LoadScene("lvl2name");
                Debug.Log("scene switched to lvl2name");
                break;

            case LEVEL_3_NAME:
                Debug.Log("mouse click hit level 3, scene switching commence!");

                SceneManager.LoadScene("lvl3name");
                Debug.Log("scene switched to lvl3name");
                break;

            default:
                Debug.Log("hit biome matches no game biome name");
                break;
        }
    }
}
