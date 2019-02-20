using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

   public bool isOver;
    //private bool win;

    private CountDown timer;
	// Use this for initialization
	void Start () {

        timer = FindObjectOfType<CountDown>();
	}
	
	// Update is called once per frame
	void Update () {

		//check if timer expried
        if (timer.timeLeft < 1){
            //stop game
            isOver = true;
            //win = false;
        }
        //check for point progress 

        //...

        //switch scean 
        if (isOver) {
            SceneManager.LoadScene("WinLoseScreen");
        }
	}
}
