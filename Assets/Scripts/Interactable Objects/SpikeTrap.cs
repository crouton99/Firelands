using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private BoxCollider2D spikeCol; 

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        spikeCol = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Phoenix")
        {
            anim.SetTrigger("Detection");
            
        }
    }

    private void selfDestruct()
    {
        Destroy(gameObject);
    }
}
