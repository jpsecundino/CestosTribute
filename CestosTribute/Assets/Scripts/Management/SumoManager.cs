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
        //Debug.Log("SumoManager - Start");
        StartMatch();
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        if (roundRunning){
            if(AllBallsStopped()){
                roundRunning = false;
                Debug.Log("As bolas pararam");
                StartPreparationRound();
            }
            Debug.Log("Esperando as bolas pararem...");
        }

        if(TimerClass.isZeroed(timer.curTime) && !roundRunning){
            ReleasePlayersBalls();
        }
    }

    public bool AllBallsStopped(){
        foreach(Player p in players){
            if(p.isBallsMoving())
                return false;
        }
        return true;
    }

    public void ReleasePlayersBalls(){
        Debug.Log("As bolas foram liberadas!");
        foreach (Player p in players){
            p.ReleaseBalls();
        }
        
        roundRunning = true;
    }
    public void StartMatch(){
        StartPreparationRound();

    }
    public void StartPreparationRound(){
        Debug.Log("Preparação (Pré-round)");
        roundRunning = false;
        timer.RestartTimer();
        timer.TimerStart();
        foreach (Player p in players){
            p.EnableControls();
        }
    }
}
