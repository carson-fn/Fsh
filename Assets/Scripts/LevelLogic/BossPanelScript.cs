using UnityEngine;

public class BossPanelScript : MonoBehaviour
{
    public GameObject BossStartScreen;
    public GameObject BossEndScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void closeStartScreen()
    {
        Debug.Log($"START SCREEN: {BossStartScreen}");
        BossStartScreen.SetActive(false); 
    }

    public void closeEndScreen()
    {
        BossEndScreen.SetActive(false); 
    }
}
