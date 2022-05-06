using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu] //For scriptable Objects
public class PhoenixAbility : Ability
{

    private Vector3 playerPosition;
    private GameObject phoenixClone;
    private GameObject player;

    private SpriteRenderer playerImage;
    private BoxCollider2D playerCollider;
    private PlayerController playerControls;
    private AudioSource playerSounds;
    private PlayerController phoenixControls;
    private Animator playerAnim;
    

    public override void Activate(GameObject parent, CameraController cam, HealthDisplay livesBar)
    {
        
        player = parent;
        playerPosition = player.transform.position;

        phoenixClone = ObjectPool.Instance.SpawnFromPool("Phoenix",player.transform.position, player.transform.rotation);
        cam.player = phoenixClone.transform;

        //The following gets, and then disables components of the main player object. 
        playerImage = player.GetComponent<SpriteRenderer>();
        playerCollider = player.GetComponent<BoxCollider2D>();
        playerControls = player.GetComponent<PlayerController>();
        playerAnim = player.GetComponent<Animator>();
        playerSounds = player.GetComponent<AudioSource>();
        phoenixControls = phoenixClone.GetComponent<PlayerController>();

        playerImage.enabled = !playerImage.enabled;
        playerCollider.enabled = !playerCollider.enabled;
        playerControls.enabled = !playerControls.enabled;
        playerAnim.enabled = !playerAnim.enabled;
        playerSounds.enabled = !playerSounds.enabled;
        phoenixControls.isPhoenix = true;
        
    }

    public override void Deactivate(GameObject parent, CameraController cam, HealthDisplay livesBar)
    {
 
        cam.player = parent.transform;

        playerImage.enabled = !playerImage.enabled;
        playerCollider.enabled = !playerCollider.enabled;
        playerControls.enabled = !playerControls.enabled;
        playerAnim.enabled = !playerAnim.enabled;
        playerSounds.enabled = !playerSounds.enabled;
        phoenixControls.isPhoenix = false;
        
        parent.transform.position = playerPosition;
            
        phoenixClone.SetActive(false);
    }
}

//INSERT CODE TO DISABLE MAIN PLAYER OBJECT AND TO USE AN OBJECT FROM THE POOL
//https://learn.unity.com/tutorial/introduction-to-object-pooling?signup=true
//https://stackoverflow.com/questions/69693347/getting-nullreferenceexception-object-reference-not-set-to-an-instance-of-an-ob
//When talking about errors, refer to this for the fix on object references. 
