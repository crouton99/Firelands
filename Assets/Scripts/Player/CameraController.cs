using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float bottomLimit;
    [SerializeField] private float topLimit;
    
    private void Update()
    {

        transform.position = new Vector3(player.position.x, player.position.y, this.transform.position.z);
         
        transform.position = new Vector3(   Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit, topLimit), 
             transform.position.z
             );
         
    }
    
}
