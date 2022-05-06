using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Start() variables.
    private Rigidbody2D rb; //Handles character physics.
    private Animator anim; //handles animation transitions.
    private BoxCollider2D coll; //handles the box colliders. 
    [SerializeField] private LayerMask groundMask;
    
    //Variable to hold players next respawn point. 
    private Vector3 currentRespawnPoint;
    
    //Some player stats.
    [SerializeField]private float speed = 5f; //Affects the speed that the player moves at.
    [SerializeField]private float jumpHeight = 10f; //Affects the jump height of the character.
    private bool isMoving;

    //Variables that relate to player damage.
    [SerializeField] public float lives;
    [SerializeField] private HealthDisplay healthUI;
    private bool isHurt; //To trigger the hurt animation when in contact with a trap.
    public bool invuln; //Allows the player to walk through FireTrap colliders.
    public bool isPhoenix = false; //is the player in phoenix?
    DateTime damageBuffer = DateTime.Now;

    //Player Pausing, modularize into UI manager class. 
    private bool isPaused; //Contains the paused state
    [SerializeField]private SceneLoader UIControl; //List to contain all the objects with Pause tag. 
    
    //Audio Functions
    [SerializeField] private AudioSource footsteps;
    [SerializeField] private AudioSource jump; 

    //Enumerator that holds the various states the user can be in.
    private enum State
    {
        idle,
        running,
        jumping,
        hurt
    };
    
    private State state = State.idle; //Initializes the player state to Idle.

    private void Start() //Initializes the components.
    {
        rb = transform.GetComponent<Rigidbody2D>(); //Physics.
        anim = GetComponent<Animator>(); //Animations.
        coll = GetComponent<BoxCollider2D>(); //Collisions.
    }
    
    private void Update() //Unity method that updates every frame.
    {
        MoveManager(); //Calls the method handling character movements.
        VStateSwitcher(); //Calls method that handles character states.
        anim.SetInteger("state", (int)state); //Sets the player state.
        
    }
    
    public void MoveManager()
    {
        if (Input.GetButton("Horizontal"))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        if (isMoving)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;

            if (!footsteps.isPlaying && !isPaused && isGrounded() && rb.velocity.y < 0.05f)
            {
                footsteps.Play();
            }
            else if(!isGrounded() || isPaused)
            {
                footsteps.Stop();
            }
            
            float xDirection = Input.GetAxis("Horizontal"); //Stores the direction where the character is travelling as a float. 
            if(xDirection < 0) 
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                transform.localScale = new Vector2(-1, 1); //Adds a negative velocity, moves the character left.
            }
            else if(xDirection > 0)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.localScale = new Vector2(1, 1); //Adds a positive velocity, moves the character right.
            }
        }
        else if(!isMoving && isGrounded() && rb.velocity.y <= 0.1f) //Slope sliding prevention. 
        {
            rb.bodyType = RigidbodyType2D.Static;
            footsteps.Stop();
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            footsteps.Stop();
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }

        if (Input.GetKeyDown("escape")) //Pause commands. 
        {
            if (isPaused) //Resumes the game 
            {
                ResumeGame();
                isPaused = false;
            } 
            else if (!isPaused) //Pauses the game
            {
                PauseGame();
                isPaused = true; 
            }
        }

        if (Time.timeScale == 1)
        {
            isPaused = false; 
        }
        
        if(Input.GetButtonDown("Jump")) //Allows the character to jump.
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            Jump();
        }
    }

    private void PauseGame() //Pauses the game. 
    {
        UIControl.showPause();
    }

    private void ResumeGame() //Unpauses the game. 
    {
        UIControl.completeUIDisable();
        UIControl.resetTime();
    }
    
    private void OnTriggerEnter2D(Collider2D trap) //Enemy interactions, very basic. Jumping on an enemy destroys them, also touching them knocks the player back.
    {
        if (trap.gameObject.tag == "FireTrap" && invuln) //Blank to say nothing should happen if fire cloak is up and the trap is fire. 
        {
        }
        else if(trap.gameObject.tag == "Trap" || trap.gameObject.tag == "FireTrap" || trap.gameObject.tag == "SpikeTrap")
        {
            Hurt();
            if (trap.gameObject.tag == "SpikeTrap")
            {
                trap.gameObject.tag = "Untagged";
            }
            
        }
        else if (trap.gameObject.tag == "Respawn")//Sets the players respawn point. 
        {
            currentRespawnPoint = new Vector3(trap.gameObject.transform.position.x,
                trap.gameObject.transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerExit2D(Collider2D trap)
    {
        if (trap.gameObject.tag == "Untagged" || trap.gameObject.tag == "Trap" || trap.gameObject.tag == "FireTrap")
        {
            isHurt = false; 
        }
    }

    private void Hurt()
    {
        if (!isPhoenix)
        {
            if (lives > 0)
            {
                if (IsInvincible())
                {
                    isHurt = true;
                    lives -= 1;
                    bufferReset();
                }
            }
            else
            {
                if (currentRespawnPoint.x != 0 && currentRespawnPoint.y != 0) //If the player has reached a respawn point, respawn there. 
                {
                    transform.position = new Vector3(currentRespawnPoint.x, currentRespawnPoint.y, currentRespawnPoint.z);
                    lives = 3;
                    healthUI.livesReset();
                }
                else //If no respawn point has been reached, restart the full level. 
                {
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                }
            }
        }

        if (isPhoenix) //A separate health loop for the phoenix object. 
        {
            if (lives > 0)
            {
                if (IsInvincible())
                {
                    isHurt = true;
                    lives -= 1;
                    bufferReset();
                }
            }
            else
            {
                lives = 3;
                gameObject.SetActive(false);
            }
        }
    }

    private void bufferReset()
    {
        damageBuffer = DateTime.Now.AddSeconds(1);
    }

    private bool IsInvincible()
    {
        return damageBuffer <= DateTime.Now;
    }

    private void Jump() //Makes the character jump. 
    {
        if (isGrounded())
        {
            jump.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }

    private bool isGrounded() //Checks if the player is touching the ground for jumping. 
    {
        float extraHeightBuffer = .25f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f,Vector2.down, extraHeightBuffer, groundMask);
        return raycastHit.collider != null;
    }
    
    private void VStateSwitcher() //Switches the state of the character.
    {
        if (rb.velocity.y > 0)
        {
            state = State.jumping;
        }
        else if (isHurt)
        {
            state = State.hurt;
        }
        else if (Input.GetButton("Horizontal")) //Changes to the running state.
        {
            //Moving to the right.
            state = State.running;
        }
        else //Default state for the character, when no actions are being performed they will be idle. 
        {
            state = State.idle;
        }
    }
}
