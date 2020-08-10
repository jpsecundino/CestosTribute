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
    private Effects ballEffects;
    private DragAndThrow dragAndThrow;
    public float radius = 0.52f;

    public GameObject glow;
    public bool isActive = false;

    void Start(){
        

        ballEffects = GetComponent<Effects>();
        dragAndThrow = GetComponent<DragAndThrow>();
        
        cc = GetComponent<CircleCollider2D>();
        radius = cc.radius;
        
        CreateBall();
    }

    public bool ClickOnBall(Vector3 mousePosition){
        
        if(!isActive)
            return false;
    
        Vector2 objPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 mousePos = new Vector2(mousePosition.x, mousePosition.y);
        
        float distance = Vector2.Distance(objPos, mousePos);
        
        return distance < radius;
        
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
}
