using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SumoManager : MonoBehaviour
{
    public Player[] players;
    public Timer timer;

    private bool roundRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        Timer.OnTimerEnd += ReleasePlayersBalls;
        //Debug.Log("SumoManager - Start");
        StartMatch();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Debug.Log(roundRunning);
        
    }

    public bool AllBallsStopped(){
        foreach(Player p in players){
            if(p.isBallsMoving())
                return false;
        }
        return true;
    }

    public void ReleasePlayersBalls(){
        roundRunning = true;
       // Debug.Log("Entrei no ReleasePlayerBalls");
        foreach (Player p in players){
            p.ReleaseBalls();
        }
    }
    public void StartMatch(){
        StartPreparationRound();

    }
    public void StartPreparationRound(){
        //Debug.Log("SumoManager - StartPreparationRound");
        roundRunning = false;
        timer.RestartTimer();
        timer.TimerStart();
        foreach (Player p in players){
            p.EnableControls();
        }
    }
}
