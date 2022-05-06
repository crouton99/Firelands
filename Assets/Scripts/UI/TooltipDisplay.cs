using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipDisplay : MonoBehaviour
{
    [SerializeField] private SceneLoader UIManager;
    [SerializeField] private String tooltipText;
    private BoxCollider2D tooltipCol;
    

    private void Start()
    {
        tooltipCol = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        
        if (player.gameObject.tag == "Player" || player.gameObject.tag == "Phoenix")
        {
            UIManager.showTooltip();
            UIManager.tooltipTextChanger(tooltipText);
        }
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player" || player.gameObject.tag == "Phoenix")
        {
            Destroy(gameObject);
        }
    }
}
