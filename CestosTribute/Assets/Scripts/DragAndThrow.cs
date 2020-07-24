using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragAndThrow : MonoBehaviour
{
    Vector3 startPos, endPos, direction;

    public GameObject movedObject;
    public bool validClick = false;
    public float radius = 0.52f;

    public float throwForce = 10000;

    [Range (0f,1f)]
    public float multiplier = 0.5f;

    //a força depende do tamanho do tamanho do vetor direcao
                //limitar o tamanho do vetor direcao
    void Update(){
        if (Input.GetMouseButtonDown(0) && ClickOnBall(Camera.main.ScreenToWorldPoint(Input.mousePosition))) {
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            validClick = true;
        }
        if (Input.GetMouseButtonUp(0) && validClick) {
            validClick = false;                              
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = startPos - endPos;
            GetComponent<Rigidbody2D>().AddForce(-direction * throwForce * multiplier);
        }
    }

    private bool ClickOnBall(Vector3 mousePosition){
        Vector2 objPos = new Vector2(movedObject.transform.position.x, movedObject.transform.position.y);
        Vector2 mousePos = new Vector2(mousePosition.x, mousePosition.y);
        
        float distance = Vector2.Distance(objPos, mousePos);
        
        //Debug.Log("Distancia: " + distance + " mouse: " + mousePosition + " objeto: "+ movedObject.transform.position);
        
        return distance < radius;
    }
}