using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockVisual : MonoBehaviour
{
    public Timer timer;

    private float elapsedTime;
    public Sprite[] clockSprites;

    public int curSprite;
    public Image currentImage; 

    private float previousTime;

    private TimerClass initialTime;
    private int nextTransition;
    private int step;

    // Start is called before the first frame update
    void Start()
    {
        initialTime = new TimerClass(timer.startTime);
        step = initialTime.SecondCount/(clockSprites.Length - 1);
        nextTransition = initialTime.SecondCount - step - 1;
        curSprite = 0;
        currentImage.sprite= clockSprites[curSprite];

        Timer.OnTimerRestart += ResetTimerVisual;
    }

    // Update is called once per frame
    void Update()
    {
        if(nextTransition > 0 && timer.curTime.SecondCount == nextTransition){
            
            nextTransition -= step;

            if(nextTransition < 0) nextTransition = 0;
            
            curSprite = (curSprite + 1) % 9;

            currentImage.sprite = clockSprites[curSprite];
        }else if(timer.isTimerActive == false && nextTransition == 0){
            currentImage.sprite = clockSprites[clockSprites.Length - 1];
        }

    }

    public void ResetTimerVisual(){
        initialTime = new TimerClass(timer.startTime);
        step = initialTime.SecondCount/(clockSprites.Length - 1);
        nextTransition = initialTime.SecondCount - step -1;
        curSprite = 0;
        currentImage.sprite= clockSprites[curSprite];
    }
}
