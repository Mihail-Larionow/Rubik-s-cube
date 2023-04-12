using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StopWatch : MonoBehaviour
{

    public Text textView;
    public bool isWorking = false;
    public bool downCounting = false;

    public void StopTimeFlow(){
        isWorking = false;
        StopAllCoroutines();
        textView.text = "00:00";
    }

    public void StartTimeFlow(Cube cube){
        isWorking = true;
        StartCoroutine(TimeFlow(cube));
    }

    private IEnumerator TimeFlow(Cube cube){
        int seconds = -1;
        int minutes = 0;
        int hours = 0;
        textView.color = Color.white;
        downCounting = true;
        yield return cube.Shuffle();
        for(int countDown = 10; countDown >= 0; countDown--){
            textView.text = countDown.ToString();
            if(countDown <= 3) textView.color = Color.red;
            if(countDown == 0) textView.text = "START!";
            yield return new WaitForSeconds(1);
        }
        downCounting = false;

        textView.color = Color.white;
        cube.isDisabled = false;
        while(true){
            if(cube.isComplete){
                isWorking = false;
                yield break;
            }
            if(hours == 24){
                textView.text = "SLOW";
                textView.color = Color.red;
                yield break;
            }
            if(seconds == 59){
                if(minutes == 59){
                    hours++;
                    minutes = -1;
                }
                minutes++;
                seconds = -1;
            }
            seconds++;
            if(hours == 0) textView.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
            else textView.text = hours.ToString() + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2");
            yield return new WaitForSeconds(1);
        }
    }

}
