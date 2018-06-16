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

        //add cans
        nbCan = trashes.Length;
        trash = new GameObject[nbCan];//boardCreat
        for (int i = 0; i < nbCan; i++)
        {
            xCan = (((7 / 2) / (float)nbCan) * (i + 1));
            print("Can loc " + xCan);
            Vector2 vector2 = new Vector3(xCan, -1.5F);
            trash[i] = Instantiate(trashPrefab[i], vector2, Quaternion.identity) as GameObject;

        }
	}

	// Update is called once per frame
	void Update () {
		
	}
}
