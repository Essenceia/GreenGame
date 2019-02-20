using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour {

    [Header("Trash can")]
    public GameObject[] trashPrefab;
    public GameObject[] trash;
    

	// Use this for initialization
	void Start () {

	}

	public void Init(int[] trashes)
	{

        float xCan;
        int nbCan;

        //add cans radomly when beegining game 
        nbCan = trashes.Length;
        trash = new GameObject[nbCan];
        for (int i = 0; i < nbCan; i++)
        {
            xCan = (float)(1.5 * i);
            Vector2 vector2 = new Vector2(xCan, -2.5F);
            trash[i] = Instantiate(trashPrefab[i], vector2, Quaternion.identity) as GameObject;
        }
	}

	// Update is called once per frame
	void Update () {}
}
