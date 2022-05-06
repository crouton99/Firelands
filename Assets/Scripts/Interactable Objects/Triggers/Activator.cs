using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{

    public SpriteRenderer lever; //Allows levers to be assigned for activation here. 
    public Sprite leverActive; //The sprite that will change the appearance to look active.
    public Sprite leverInactive;
    private bool activated = false;
    public GameObject door;

    private bool doorIsActive = false; 
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
        //Debug.Log("Collision Made"); //Some testing stuff
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Phoenix") //If the collision is made by a player, the lever is activated. 
        {
            activated = true; //Activates the switch to change the switch icon. 
            //door.SetActive(!door.activeSelf);
            
            if (activated)
            {
                lever.sprite = leverActive;
                if (doorIsActive == false) //A boolean to activate the door once, and only once. 
                {
                    door.SetActive(!door.activeSelf);
                    doorIsActive = true; 
                }
                 
            }
            else
            {
                lever.sprite = leverInactive; //Changes the sprite of the activator to active.
            }
            
             
            
            
        }
    }
}
