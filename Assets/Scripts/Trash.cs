using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour {

    public int fullitude;
    private Vector2 basePosition;
    private Vector2 position;

    private float speed;
    private float amount;
    private Board board;

    public ProgressManager progress;

    readonly int maxCapacity = 10;

    // Use this for initialization
    void Start()
    {
        board = FindObjectOfType<Board>();
        basePosition = transform.position;
        position = transform.position;
        progress = FindObjectOfType<ProgressManager>();

        this.fullitude = 0;
        this.speed = 40;
        this.amount = 0.06F;
    }

    void Update()
    {
        if (this.isFull())
        {
            position.x = basePosition.x + Mathf.Sin(Time.time * speed) * amount;
            position.y = basePosition.y + Mathf.Cos(Time.time * speed) * amount;
            transform.position = position;
        } else {
            transform.position = basePosition;
        }

    }

    public bool isFull()
    {
        return (fullitude >= maxCapacity);
    }

    public void addToCan(int quantity)
    {
        if (!this.isFull())
        {
            fullitude += quantity;
        }
    }

    private void OnMouseDown()
    {
        if (this.isFull()) {
            fullitude = 0;
            if (this.gameObject.tag == "Yellow")
            {
                progress.GetComponent<ProgressManager>().UpdateScore(maxCapacity);
                board.compostCounter = 0;
                for (int i = 0; i < board.toRecycle.Length; i++)
                {
                    if (board.toRecycle[i]) {
                        board.toRecycle[i].GetComponent<Dot>().Recycle();
                        //Destroy(board.toCompost[i]);
                        board.toRecycle[i] = null;
                    }
                }
            }
            else if (this.gameObject.tag == "Brown")
            {
                progress.GetComponent<ProgressManager>().UpdateScore(maxCapacity);
                board.compostCounter = 0;
                for (int i = 0; i < board.toCompost.Length; i++)
                {
                    if (board.toCompost[i]) {
                        board.toCompost[i].GetComponent<Dot>().Recycle();
                        //Destroy(board.toCompost[i]);
                        board.toCompost[i] = null;
                    }
                }
            } else if (this.gameObject.tag == "White") {
                progress.GetComponent<ProgressManager>().UpdateScore(maxCapacity);
                board.glassCounter = 0;
                for (int i = 0; i < board.toGlass.Length; i++)
                {
                    if (board.toGlass[i]) {
                        board.toGlass[i].GetComponent<Dot>().Recycle();
                        //Destroy(board.toCompost[i]);
                        board.toGlass[i] = null;
                    }
                }
            } else if (this.gameObject.tag == "Blue")
            {
                progress.GetComponent<ProgressManager>().UpdateScore(maxCapacity);
                board.electronicCounter = 0;
                for (int i = 0; i < board.toElectronic.Length; i++)
                {
                    if (board.toElectronic[i]){
                        board.toElectronic[i].GetComponent<Dot>().Recycle();
                        //Destroy(board.toCompost[i]);
                        board.toElectronic[i] = null;
                    }
                }
            }
            //empty can 
            Debug.Log(fullitude);
        }
    }
}
