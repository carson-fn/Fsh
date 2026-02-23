using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScreenLogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void goToLevel(int lvl)
    {
        SceneManager.LoadScene("Game");
        LogicScript.setLevel(lvl);
    }

    
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
