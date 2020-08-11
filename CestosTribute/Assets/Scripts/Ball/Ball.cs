using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Effects))]
[RequireComponent(typeof(CircleCollider2D))]
public class Ball : MonoBehaviour
{
    private const int V = 0;
    private Rigidbody rb;
    private CircleCollider2D cc;
    private CircleCollider2D ballCenterCollider;
    private Effects ballEffects;
    private DragAndThrow dragAndThrow;
    public float touchRadius = 0.52f;

    public GameObject glow;
    public GameObject ballCenter;
    public bool isActive = false;


    void Start(){
        

        ballEffects = GetComponent<Effects>();
        dragAndThrow = GetComponent<DragAndThrow>();
        
        cc = GetComponent<CircleCollider2D>();
        CreateBall();
        InstantiateCenter();
    }

    public bool ClickOnBall(Vector3 mousePosition){
        
        if(!isActive)
            return false;

        Vector2 objPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 mousePos = new Vector2(mousePosition.x, mousePosition.y);
        
        
        float distance = Vector2.Distance(objPos, mousePos);
        Debug.Log(distance);
        return distance <= touchRadius;
        
    }

    public void CreateBall(){
        isActive = true;
        ballEffects.Consolidate();
    }

    public void DestroyBall(){
        ballEffects.Dissolve();
        cc.enabled = false;
        glow.SetActive(false);
        isActive = false;
    }


    private void InstantiateCenter(){
        ballCenter = new GameObject("ballCenter");        
        ballCenterCollider = ballCenter.AddComponent<CircleCollider2D>();
        ballCenterCollider.radius = 0.09f;
        ballCenterCollider.transform.position = transform.position;
        ballCenterCollider.transform.parent = gameObject.transform;
        ballCenter.tag = "ballCenter";
    }
}
