using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ArrowSprite2 : MonoBehaviour
{
    
    public Vector3 startPoint;
    public Vector3 endPoint;
    public GameObject clipPoint;
    public GameObject ball;
    private Ball ballScript;
    private bool validClick = false;
    
    [SerializeField]
   
    private SpriteRenderer arrowBodyRenderer;
    
    [SerializeField]
    private SpriteRenderer arrowTopRenderer;
    
    private Vector3 bodyInitialScale;

    [Range(-10f, 10f)]
    public float offset = 0.1f;

    public float maxSize = 2f;
    public float minSize = 0.5f;

    void Start()
    {
        ballScript = ball.GetComponent<Ball>();      
        arrowBodyRenderer.enabled = false;
        arrowTopRenderer.enabled = false;
        bodyInitialScale = arrowBodyRenderer.transform.localScale;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && ballScript.ClickOnBall(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            validClick = true;
            arrowBodyRenderer.enabled = true;
            arrowTopRenderer.enabled = true;
        }
        if (Input.GetMouseButton(0) && validClick)
        {
            startPoint = ball.transform.position;
            transform.position = startPoint;
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ClipDistance(endPoint, startPoint);
            ChangeAngle();
            UpdateArrowBodySize();
            UpdateArrowTopPosition();
        }
        
        if (Input.GetMouseButtonUp(0) && validClick)
        {
            arrowBodyRenderer.enabled = false;
            arrowTopRenderer.enabled = false;
            validClick = false;
        }
    }
    
    private void UpdateArrowTopPosition(){
        arrowTopRenderer.transform.position = clipPoint.transform.position;
    }

    private void UpdateArrowBodySize(){
        float distance = Vector2.Distance(startPoint, endPoint);
        arrowBodyRenderer.transform.localScale = new Vector3(bodyInitialScale.x * distance, bodyInitialScale.y, bodyInitialScale.z);
    }

    private void ChangeAngle(){
        Vector2 dir = new Vector2( endPoint.x, endPoint.y ) - new Vector2(startPoint.x, startPoint.y);
        float angle = Vector2.SignedAngle(dir, new Vector2(1,0));
        transform.rotation = Quaternion.AngleAxis(- angle, Vector3.forward);
    }
    
    private void  ClipDistance(Vector3 mousePosition, Vector3 startPos){
        
        Vector2 objPos = new Vector2(startPos.x, startPos.y);
        Vector2 mousePos = new Vector2(mousePosition.x, mousePosition.y);
        Vector2 direction = (mousePos - objPos).normalized;

        float distance = Vector2.Distance(objPos, mousePos);

        if (distance <= maxSize && distance >= minSize){
            endPoint = mousePosition;
        }else{
            if(distance > maxSize){
                endPoint = ((Vector3)direction * maxSize) + startPos;
            }else{
                endPoint = startPos;
            }
        }

    }
}
