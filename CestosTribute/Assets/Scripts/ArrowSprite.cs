using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSprite : MonoBehaviour
{
    
    public Vector3 startPoint;
    public Vector3 endPoint;
    public GameObject ball;
    private Ball ballScript;
    private bool validClick = false;
    private Renderer arrowSprite;

    void Start()
    {
        ballScript = ball.GetComponent<Ball>();      
        arrowSprite = GetComponent<Renderer>();
        arrowSprite.enabled = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && ballScript.ClickOnBall(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            validClick = true;
            arrowSprite.enabled = true;
            startPoint = ball.transform.position;
            transform.position = startPoint;

        }
        if (Input.GetMouseButton(0) && validClick)
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Vector2 dir = new Vector2( endPoint.x, endPoint.y ) - new Vector2(startPoint.x, startPoint.y);
           
            float angle = Mathf.Atan2(dir.y + startPoint.y, dir.x + startPoint.x) * Mathf.Rad2Deg;
            
            Debug.Log(dir);
            

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if (Input.GetMouseButtonUp(0) && validClick)
        {
            arrowSprite.enabled = false;
            validClick = false;
        }
    }

    // private void  allowedDragDistance(Vector3 mousePosition, Vector3 startPos){
        
    //     Vector2 objPos = new Vector2(startPos.x, startPos.y);
    //     Vector2 mousePos = new Vector2(mousePosition.x, mousePosition.y);
    //     Vector2 direction = (mousePos - objPos).normalized;

    //     float distance = Vector2.Distance(objPos, mousePos);

    //     if (distance <= maxSize && distance >= minSize){
    //         endPos = mousePosition + camOffset;
    //     }else{
    //         if(distance > maxSize){
    //             endPos = ((Vector3)direction * maxSize) + startPos;
    //         }else{
    //             endPos = startPos;
    //         }
    //     }
    // }
}
