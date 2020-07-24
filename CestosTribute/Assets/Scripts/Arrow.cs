using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
        
    //This doesn’t need to be public, but it helps when you’re initially trying to find the right size
    public float arrowheadSize;
    public Vector3 startPosition, endPosition, ballPosition;
    GameObject arrow;
    LineRenderer arrowLine;

    void Start(){
        arrowLine = GetComponent<LineRenderer>();
        arrowheadSize = 0.02f;
    }
    private void Update() {
        startPosition = transform.position;
    }

    void OnMouseDrag(){
        
        //Turn on the arrow
        arrowLine.enabled = true;
        DrawArrow ();

    }

    void DrawArrow(){

        endPosition = Camera.main.ScreenToWorldPoint (
        new Vector3 (Input.mousePosition.x,
        Input.mousePosition.y,
        Camera.main.nearClipPlane
        ));

        //The longer the line gets, the smaller relative to the entire line the arrowhead should be
        float percentSize = (float) (arrowheadSize / Vector3.Distance (startPosition, endPosition));
        //h/t ShawnFeatherly (http://answers.unity.com/answers/1330338/view.html)
        arrowLine.SetPosition (0, startPosition);
        arrowLine.SetPosition (1, Vector3.Lerp(startPosition, endPosition,1));
        arrowLine.SetPosition (2, Vector3.Lerp (startPosition, endPosition,1));
        arrowLine.SetPosition (3, endPosition);
        arrowLine.widthCurve = new AnimationCurve (
        new Keyframe (0, 0.4f),
        new Keyframe (0.999f - percentSize, 0.4f),
        new Keyframe (1 - percentSize, 1f),
        new Keyframe (1 - percentSize, 1f),
        new Keyframe (1, 0f));

    }

    void OnMouseUp(){
        //Turn off the arrow
        arrowLine.enabled = false;

    }

}
