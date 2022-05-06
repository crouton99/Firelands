using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{

    public GameObject player;
    private float livesUINum;
    [SerializeField] private List<Image> HealthPool;

    private List<Image> healthPoolBackup;
    
    // Start is called before the first frame update
    void Start()
    {
        
        healthPoolBackup = HealthPool;
    }

    // Update is called once per frame
    void Update()
    {
         //Retrieves information about player lives

        int playerLives = (int)player.GetComponent<PlayerController>().lives;
         
        /*for (int i = playerLives; i >= 0; i--)
        {
            HealthPool[playerLives].gameObject.SetActive(false);
        }

        for (int i = 0; i < playerLives; i++)
        {
            
        }*/
            
           
        
        /*
         PlayerController infoAccess = player.GetComponent<PlayerController>();*/
         if (playerLives < 3 && playerLives >= 0)
        {
            HealthPool[playerLives].gameObject.SetActive(false);
           
        }
    }

    public void livesReset()
    {
        HealthPool = healthPoolBackup;
        foreach (Image image in HealthPool)
        {
            image.gameObject.SetActive(true);
        }
    }
}
