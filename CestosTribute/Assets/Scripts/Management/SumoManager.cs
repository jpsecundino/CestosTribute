using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerInfo
{
    public Vector3[] lastBallsPosition;
    public bool[] lastBallsAlive;
    public PlayerInfo(){
        lastBallsPosition = new Vector3[3];
        lastBallsAlive = new bool[3];
    }
}

public class SumoManager : MonoBehaviour
{
    public Player[] players;
    public Timer timer;

    private bool roundRunning;

    private bool gameIsOn;

    private List<PlayerInfo> lastRoundInfo;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("SumoManager - Start");
        gameIsOn = true;
        lastRoundInfo = new List<PlayerInfo>();
        foreach(Player p in players){
            lastRoundInfo.Add(new PlayerInfo());
        }
        StoreInitialPositions();
        StartMatch();
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        if (roundRunning){
            if(AllBallsStopped()){
                roundRunning = false;
                //Debug.Log("As bolas pararam");
                VerifyRoundResults();
                return;
            }
            //Debug.Log("Esperando as bolas pararem...");
        }
        //verifica se o timer chegou ao zero
        if( gameIsOn && TimerClass.isZeroed(timer.curTime) && !roundRunning){
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
        //Debug.Log("As bolas foram liberadas!");
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
        timer.TimerStart();
        foreach (Player p in players){
            p.EnableControls();
        }
    }

    public void StoreInitialPositions(){
        for (int i = 0; i < players.Length; i++){
            Ball[] _playerBalls = players[i].playerBalls; 
            
            for (int j = 0; j < _playerBalls.Length; j++){
                lastRoundInfo[i].lastBallsPosition[j] = players[i].playerBalls[j].transform.position;
            }
        }
    }

    public void StoreLastState(){
        for (int i = 0; i < players.Length; i++){
            Ball[] _playerBalls = players[i].playerBalls; 
            
            for (int j = 0; j < _playerBalls.Length; j++){
                lastRoundInfo[i].lastBallsAlive[j] = players[i].playerBalls[j].isActive;
            }
        }
    }

    public void ReplaceBalls(){
        for (int i = 0; i < players.Length; i++){
            
            Ball[] _playerBalls = players[i].playerBalls; 

            for (int j = 0; j < _playerBalls.Length; j++){
                if(lastRoundInfo[i].lastBallsAlive[j] == true){
                    Vector3 _position = new Vector3(lastRoundInfo[i].lastBallsPosition[j].x,
                                                    lastRoundInfo[i].lastBallsPosition[j].y,
                                                    lastRoundInfo[i].lastBallsPosition[j].z);
                    _playerBalls[j].gameObject.transform.position = _position;
                    _playerBalls[j].CreateBall();
                    _playerBalls[j].isActive = true;
                }
            }
        }
    }

    void VerifyRoundResults(){
        
        int _playersDead = 0;
        
        //verify for dead players
        foreach(Player p in players){
            if(p.AllBallsDestroyed()){
                _playersDead += 1;
            }
        }

        //check if we have a draw
        if(_playersDead == players.Length){
            ReplaceBalls();
            timer.RestartTimer();
            StartPreparationRound();
        }else if(_playersDead == players.Length - 1){//check if someone won
            foreach(Player p in players){
                if(!p.AllBallsDestroyed()){
                    Debug.Log(p.name + "Won!");
                    gameIsOn = false;
                    //call end of game function
                }
            }
        }else{//continue for the next round
            StoreLastState();
            timer.RestartTimer();
            StartPreparationRound();
        }
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
