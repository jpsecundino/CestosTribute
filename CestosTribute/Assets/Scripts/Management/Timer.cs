using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    
    public static Action OnTimerRestart;
    public bool isTimerActive = false;
    public string startTime = "00:00:00";
    public bool increasing = false;
    public static string milliFormat;
    private float countdownTime = 3f;
    public bool isCountdownOn = true;

    public TimerClass curTime;
    
    private void Start()
    {
        curTime = new TimerClass(startTime); 
        countdownTime += 1;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isCountdownOn == true)
        {
            if(countdownTime < 1)
            {
                isCountdownOn = false;
            }
            else
            {
                countdownTime -= Time.deltaTime;

            }
        }
        else if(isTimerActive)
        {
            if (increasing) increaseClock();
            else decrease();
            
            if (TimerClass.isZeroed(curTime) && isTimerActive) {
                isTimerActive = false;
            }

        }
        Debug.Log(curTime +"----"+ isTimerActive);
    }

    private void increaseClock()
    {
        curTime.MilliCount += Time.deltaTime * 10;


        if (curTime.MilliCount > 9)
        {
            curTime.MilliCount = 0;
            curTime.SecondCount++;
        }         

        if (curTime.SecondCount > 59)
        {
            curTime.SecondCount = 0;
            curTime.MinuteCount++;
        }

        SetTime(curTime.MinuteCount, curTime.SecondCount, curTime.MilliCount);

    }

    private void decrease()
    {
        curTime.MilliCount -= Time.deltaTime * 10;

        if (curTime.MilliCount < 0)
        {
            curTime.MilliCount = 9;
            curTime.SecondCount--;
        }

        if (curTime.SecondCount < 0)
        {
            curTime.SecondCount = 59;
            curTime.MinuteCount--;
        }

        SetTime(curTime.MinuteCount, curTime.SecondCount, curTime.MilliCount);
        
    }

    public TimerClass GetCurTime()
    {
        return curTime;
    }

    public void RestartTimer()
    {
        curTime = new TimerClass(startTime);
        OnTimerRestart();
    }
     
    public void TimerPause(){
        isTimerActive = false;
    }
    public void TimerStart(){
        //Debug.Log("Timer has started");
        isTimerActive = true;
    }
    public void SetTime(TimerClass time)
    {
        curTime = time;
    }

    public void SetTime(int _min, int _sec, float _milli)
    {
        curTime = new TimerClass(_min, _sec, _milli);
    }

}