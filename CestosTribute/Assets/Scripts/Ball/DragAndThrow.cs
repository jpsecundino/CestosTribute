using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragAndThrow : MonoBehaviour
{
    public Vector3 startPos, endPos, forceDir;

    public Ball ball;
    public GameObject movedObject;
    
    public bool validClick = false;
    //private bool canApplyForce = false;
    public float throwForce = 10000;
    public float forceMultiplier;
    public float maxDistance;
    public float minDistance;

    void Update(){
        
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && ball.ClickOnBall(mousePosition)) {
            validClick = true;
        }
        
        if (Input.GetMouseButton(0) && validClick)
        {
            startPos = ball.transform.position;
            ClipDistance(mousePosition, startPos);
        }

        if (Input.GetMouseButtonUp(0) && validClick) {
            validClick = false;                              
        }
    }

    public void ApplyForce(){

        GetComponent<Rigidbody2D>().AddForce(forceDir * forceMultiplier * throwForce);
        //Debug.Log("Força aplicada: " + forceDir * forceMultiplier * throwForce);
    }

    public void ResetForces(){
        forceMultiplier = 0f;
    }


    private void  ClipDistance(Vector3 mousePosition, Vector3 startPos){
    
        Vector2 objPos = new Vector2(startPos.x, startPos.y);
        Vector2 mousePos = new Vector2(mousePosition.x, mousePosition.y);
        
        forceDir = (mousePos - objPos).normalized;    

        float distance = Vector2.Distance(objPos, mousePos);
        if (distance <= maxDistance && distance >= minDistance){
            endPos = mousePosition;
        }else{
            if(distance > maxDistance){
                endPos = ((Vector3)forceDir * maxDistance) + startPos;
                distance = maxDistance;
            }else{
                endPos = startPos;
                distance = 0;
            }
        }
        
        forceMultiplier = distance/maxDistance;
        //Debug.Log(forceDir + " " + forceMultiplier + " " + throwForce);
        }

}