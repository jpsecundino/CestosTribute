using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    public string playerName;
    public int lvl;

    public Ball[] playerBalls; 


    public bool isBallsMoving(){
        foreach(Ball b in playerBalls){
            if(b.rb != null && b.rb.velocity.magnitude != 0f){
                return true;
            }
        }
        return false;
    }

    public void ReleaseBalls(){
        foreach (Ball b in playerBalls){
            b.ReleaseBall();
        }
    }

    public void EnableControls(){
        foreach (Ball b in playerBalls){
            if(b.isActive) b.EnableBallControl();
        }
    }
}
