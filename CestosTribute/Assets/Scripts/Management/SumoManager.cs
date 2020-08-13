using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SumoManager : MonoBehaviour
{
    public Player[] players;
    public Timer timer;

    private bool roundRunning;
    // Start is called before the first frame update
    void Start()
    {
        roundRunning = false;
        Timer.OnTimerEnd += ReleasePlayersBalls;
        StartMatch();
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log(roundRunning);
        
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
        timer.RestartTimer();
        timer.TimerStart();
        foreach (Player p in players){
            p.EnableControls();
        }
    }
}
