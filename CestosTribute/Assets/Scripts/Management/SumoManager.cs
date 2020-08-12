using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SumoManager : MonoBehaviour
{
    public Player[] players;
    public Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        Timer.OnTimerEnd += ReleasePlayersBalls;
        StartMatch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReleasePlayersBalls(){
        Debug.Log("Entrei no ReleasePlayerBalls");
        foreach (Player p in players){
            p.ReleaseBalls();
        }
    }
    public void StartMatch(){
        StartRound();

    }
    public void StartRound(){
        timer.RestartTimer();
        timer.TimerStart();
        foreach (Player p in players){
            p.EnableControls();
        }
    }
}
