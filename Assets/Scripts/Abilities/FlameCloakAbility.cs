using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu] //Create a scriptable object.
public class FlameCloakAbility : Ability
{
    //Changes the invuln variable in the playercontroller to prevent losing lives when going through fire. 
    public override void Activate(GameObject parent, CameraController cam, HealthDisplay livesBar)
    {
        //fire immunity variable in player controller that you activate here. 
        PlayerController playerStats = parent.GetComponent<PlayerController>();
        playerStats.invuln = true; 
    }

    public override void Deactivate(GameObject parent, CameraController cam, HealthDisplay livesBar)
    {
        PlayerController playerStats = parent.GetComponent<PlayerController>();
        playerStats.invuln = false; 
    }
}
