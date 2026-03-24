using UnityEngine;

public class BuddyScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public SpriteRenderer buddyRenderer;  
    public Sprite liamBuddy;
    public Sprite haroldBuddy;
    public Sprite carsonBuddy;

    public void changeBuddy(Sprite newSprite)
    {
        buddyRenderer.sprite = newSprite;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // just testing out based on lvls for now, can change later !!!
        switch(LogicScript.getLevel())
        {
            case 1:
                changeBuddy(liamBuddy);
                break;
            case 2:
                changeBuddy(haroldBuddy);
                break;
            case 3:
                changeBuddy(carsonBuddy);
                break;
        }
        
    }
}
