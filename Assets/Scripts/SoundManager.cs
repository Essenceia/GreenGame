using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource trashDance;
    public AudioSource swipe;
    public AudioSource match;
    public AudioSource trashClick;


    public void PlayTrashClick(){
        if(trashClick!=null) trashClick.Play();
    }
    public void Playswipe()
    {
        if(swipe!=null) swipe.Play();
    }
    public void Playmatch()
    {
        if(match!=null) match.Play();
    }
    public void Playtotrash()
    {
        if (trashDance!= null) trashDance.Play();
    }
}
