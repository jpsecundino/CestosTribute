using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.CompareTag("Sphere")){
            other.gameObject.GetComponent<Ball>().DestroyBall();
            
        }
    }
}
