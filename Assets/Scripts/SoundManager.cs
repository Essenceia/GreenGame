using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource moveBell;
    public AudioSource swipe;
    public AudioSource match;
    public AudioSource totrash;


    public void PlayMoveBell(){
        moveBell.Play();
    }
    public void Playswipe()
    {
        swipe.Play();
    }
    public void Playmatch()
    {
        match.Play();
    }
    public void Playtotrash()
    {
        totrash.Play();
    }
}
