using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerScript : MonoBehaviour
{
    void Update()
    {
        if (gameObject.transform.position.y < 347)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D ground)
    {
        if (ground.tag == "Ground")
        {
            gameObject.SetActive(false);
        }
    }
}
