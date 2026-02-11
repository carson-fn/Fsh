using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScreenLogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void goToLevel1()
    {
        SceneManager.LoadScene("Game");
        LogicScript.setLevel(1);
    }
    public void goToLevel2()
    {
        SceneManager.LoadScene("Game");
        LogicScript.setLevel(5); // to test fishies for now 
    }
    
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
