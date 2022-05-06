using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{

    public Transform player; //Players location currently. 
    public Transform teleportLocation; //Place for player to teleport to.
    
    private void OnTriggerEnter2D(Collider2D chara)
    {
        if (chara.gameObject.tag == "Player" || chara.gameObject.tag == "Phoenix")
        {
            //player.transform.position = new Vector3(teleportLocation.transform.position.x, teleportLocation.transform.position.y, player.position.z);

            chara.transform.position = new Vector3(teleportLocation.transform.position.x,
                teleportLocation.transform.position.y, player.position.z);
        }
    }
}
