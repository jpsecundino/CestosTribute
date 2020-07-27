using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragAndThrow : MonoBehaviour
{
    Vector3 startPos, endPos, direction;

    public GameObject movedObject;
    public bool validClick = false;
    
    public float throwForce = 10000;

    [Range (0f,1f)]
    public float multiplier = 0.5f;

    public Ball ball;
    //a força depende do tamanho do tamanho do vetor direcao
                //limitar o tamanho do vetor direcao
    void Update(){
        if (Input.GetMouseButtonDown(0) && ball.ClickOnBall(Camera.main.ScreenToWorldPoint(Input.mousePosition))) {
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            validClick = true;
        }
        if (Input.GetMouseButtonUp(0) && validClick) {
            validClick = false;                              
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = startPos - endPos;
            // ApplyForce();
        }
    }

    public void ApplyForce(){
        GetComponent<Rigidbody2D>().AddForce(-direction * throwForce * multiplier);
    }

}