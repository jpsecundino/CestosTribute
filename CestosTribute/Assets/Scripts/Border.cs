using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("entrei");    
        if(other.CompareTag("Sphere")){
            other.gameObject.GetComponent<Effects>().Dissolve();
            other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
        }
    }
}
