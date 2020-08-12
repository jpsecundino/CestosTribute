using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    public string playerName;
    public int lvl;

    public Ball[] playerBalls; 


    public void ReleaseBalls(){

        foreach (Ball b in playerBalls){
            b.ReleaseBall();
        }
    }
    public void EnableControls(){
        foreach (Ball b in playerBalls){
            b.EnableBallControl();
        }
    }
}
