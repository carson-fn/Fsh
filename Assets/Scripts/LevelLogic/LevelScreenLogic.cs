using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScreenLogic : MonoBehaviour
{
    private static int BOSS_LVL = 3;

    public static int getBOSS_LVL()
    {
        return BOSS_LVL;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void goToLevel(int lvl)
    {

        Debug.Log($"PICKING LVL {lvl}\n\n");
        LogicScript.setLevel(lvl);
        //LogicScript.setBiome(1); // FOR TESTING 
        if(lvl == BOSS_LVL)
        {
            SceneManager.LoadScene("Biome1BossLvl");            
            
        }
        else
        {
            LogicScript.setStartGame(true);
            SceneManager.LoadScene("Biome1GameLvl");
        }
        
    }

    
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
