using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    public float radius = 0.52f;

    public bool ClickOnBall(Vector3 mousePosition){
        Vector2 objPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 mousePos = new Vector2(mousePosition.x, mousePosition.y);
        
        float distance = Vector2.Distance(objPos, mousePos);
        
        //Debug.Log("Distancia: " + distance + " mouse: " + mousePosition + " objeto: "+ movedObject.transform.position);
        
        return distance < radius;
    }

}
