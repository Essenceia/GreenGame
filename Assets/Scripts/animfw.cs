using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animfw : MonoBehaviour {

    public Animator anim1;
	// Use this for initialization
	void Start () {
        anim1 = FindObjectOfType<Animator>();
        StartCoroutine("launch");
	}
	
    private IEnumerator launch(){
        yield return new WaitForSeconds(Random.value * 2);
        anim1.Play("anim");
    }
}
