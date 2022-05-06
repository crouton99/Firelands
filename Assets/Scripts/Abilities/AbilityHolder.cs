using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public CameraController mainCam;
    public GameObject abilityButton;
    public HealthDisplay livesBar;
    private AbilityCooldowns abilityUI;
    
    public Ability ability;
    float cdTime;
    float duration;

    enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }
    
    AbilityState state = AbilityState.Ready;
    public KeyCode key;

    private void Start()
    {
        abilityUI = abilityButton.GetComponent<AbilityCooldowns>();
    }

    void Update()
    {
        switch (state)
        {
            case AbilityState.Ready: //Activates the ability if it is ready. 
                
                if(Input.GetKeyDown(key))
                {
                    ability.Activate(gameObject, mainCam, livesBar);
                    state = AbilityState.Active;
                    duration = ability.duration;
                    abilityUI.inUse(duration);
                }
                break;   
             
            case AbilityState.Active: //If the ability is active, the program will do nothing.
                if(duration > 0) //Checks how much active time is left for the ability. 
                {
                    duration -= Time.deltaTime; //Will decrease the active time period. 
                }
                else
                {
                    ability.Deactivate(gameObject, mainCam, livesBar);
                    state = AbilityState.Cooldown;
                    cdTime = ability.cdTime;
                    abilityUI.useAbility(cdTime);
                }
                break;
                
            case AbilityState.Cooldown: //If the ability is on cooldown, the program will do nothing.
                if(cdTime > 0)
                {
                    cdTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.Ready;
                }
                break;
                
              
        }
        
        
    }
}
