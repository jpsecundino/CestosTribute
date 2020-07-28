using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Arrow : MonoBehaviour
{

    private Vector3 startPos;
    private Vector3 endPos;
    private Camera myCamera;
    public GameObject ball;

    private bool validClick = false;
    public LineRenderer lr;
    private Ball ballScript;
    public float maxSize = 2f;
    public float minSize = 0.5f;
    Vector3 camOffset = new Vector3(0, 0, 10);
 
    [SerializeField] public AnimationCurve ac;
 
    void Start()
    {
        myCamera = Camera.main;
        ballScript = ball.GetComponent<Ball>();
    }
 
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && ballScript.ClickOnBall(myCamera.ScreenToWorldPoint(Input.mousePosition)))
        {
            validClick = true;
            if (lr == null)
            {
                lr = gameObject.AddComponent<LineRenderer>();
            }
            
            lr.enabled = true;
            lr.positionCount = 2;
            startPos = ball.transform.position;
            lr.SetPosition(0, startPos);
            lr.useWorldSpace = true;
            lr.widthCurve = ac;
            lr.numCapVertices = 10;
        
        }
        if (Input.GetMouseButton(0) && validClick)
        {
            allowedDragDistance(myCamera.ScreenToWorldPoint(Input.mousePosition), startPos);
            lr.SetPosition(1, endPos);
        }
        if (Input.GetMouseButtonUp(0) && validClick)
        {
            lr.enabled = false;
            validClick = false;
        }

    }

    private void  allowedDragDistance(Vector3 mousePosition, Vector3 startPos){
        
        Vector2 objPos = new Vector2(startPos.x, startPos.y);
        Vector2 mousePos = new Vector2(mousePosition.x, mousePosition.y);
        Vector2 direction = (mousePos - objPos).normalized;

        float distance = Vector2.Distance(objPos, mousePos);

        if (distance <= maxSize && distance >= minSize){
            endPos = mousePosition + camOffset;
        }else{
            if(distance > maxSize){
                endPos = ((Vector3)direction * maxSize) + startPos;
            }else{
                endPos = startPos;
            }
        }
    }
}