using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenTrash : MonoBehaviour {
    
    public int fullitude;
    private Vector2 basePosition;
    private Vector2 position;

    private float speed;
    private float amount;

    public ProgressManager progress;


    readonly int maxCapacity = 10;

    public BurnScript burPref;
    //private BurnScript sacrifice;

    // Use this for initialization
    void Start()
    {
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
        }
        else
        {
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
        if (this.isFull())
        {
           // sacrifice = Instantiate(burPref, transform.position, Quaternion.identity) ;
            //empty can 
            EmptyCan();
        }
    }

    private void EmptyCan()
    {
        fullitude = 0;
    }
}
