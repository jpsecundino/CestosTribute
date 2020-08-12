using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Arrow : MonoBehaviour
{
    
    public Vector3 startPoint;
    public Vector3 endPoint;
    public GameObject clipPoint;
    public GameObject ball;
    public DragAndThrow dragAndThrow;
    [Range(-10f, 10f)]
    public float maxSize = 2f;
    public float minSize = 0.5f;
    
    private Ball ballScript;
    private bool validClick = false;
    private bool canDrawArrow = false;

    [SerializeField]
    private SpriteRenderer arrowBodyRenderer;
    
    [SerializeField]
    private SpriteRenderer arrowTopRenderer;
    
    private Vector3 bodyInitialScale;


    void Start()
    {
        ballScript = ball.GetComponent<Ball>();  
        dragAndThrow = ball.GetComponent<DragAndThrow>();
        arrowBodyRenderer.enabled = false;
        arrowTopRenderer.enabled = false;
        bodyInitialScale = arrowBodyRenderer.transform.localScale;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && ballScript.ClickOnBall(Camera.main.ScreenToWorldPoint(Input.mousePosition)) && canDrawArrow)
        {
            validClick = true;
            arrowBodyRenderer.enabled = true;
            arrowTopRenderer.enabled = true;
        
        }
        if (Input.GetMouseButton(0) && validClick && canDrawArrow)
        {
            startPoint = ball.transform.position;
            transform.position = startPoint;
            endPoint = dragAndThrow.endPos;
            // ClipDistance(endPoint, startPoint);
            ChangeAngle();
            UpdateArrowBodySize();
            UpdateArrowTop();
        }
        
        if (Input.GetMouseButtonUp(0) && validClick && canDrawArrow)
        {          
            validClick = false;
        }
    }
    
    private void UpdateArrowTop(){
        arrowTopRenderer.transform.position = clipPoint.transform.position;

        if(startPoint == endPoint){
            arrowTopRenderer.enabled = false;
        }else{
            arrowTopRenderer.enabled = true;
        }
    }

    private void UpdateArrowBodySize(){
        float distance = Vector2.Distance(startPoint, endPoint);
        float multiplier = dragAndThrow.forceMultiplier * maxSize;
        arrowBodyRenderer.transform.localScale = new Vector3(bodyInitialScale.x * multiplier, bodyInitialScale.y, bodyInitialScale.z);
    }

    private void ChangeAngle(){
        Vector2 dir = new Vector2( endPoint.x, endPoint.y ) - new Vector2(startPoint.x, startPoint.y);
        float angle = Vector2.SignedAngle(dir, new Vector2(1,0));
        transform.rotation = Quaternion.AngleAxis(- angle, Vector3.forward);
    }
    
    public void DisableArrow(){
        arrowBodyRenderer.enabled = false;
        arrowTopRenderer.enabled = false;
        ArrowDrawingEnabled(false);
    }
    public void ArrowDrawingEnabled(bool b){
        canDrawArrow = b;
    }
}
