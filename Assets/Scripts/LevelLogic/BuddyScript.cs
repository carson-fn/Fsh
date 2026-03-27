using System.Collections.Generic;
using UnityEngine;

public class BuddyScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public SpriteRenderer buddyRenderer;  
    public Sprite liamBuddy;
    public Sprite haroldBuddy;
    public Sprite carsonBuddy;

    public List<Sprite> buddies = new List<Sprite>();

    public void changeBuddy(Sprite newSprite)
    {
        buddyRenderer.sprite = newSprite;
    }
    void Start()
    {
        buddies.Add(liamBuddy);
        buddies.Add(haroldBuddy);
        buddies.Add(carsonBuddy);

        
    }

    // Update is called once per frame
    void Update()
    {
        changeBuddy(buddies[LogicScript.getLevel() - 1]);
        
    }
}
