using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour {

    public int fullitude;
    public int[] contenu = { 0, 0, 0, 0, 0 };
    private Vector2 basePosition;
    private Vector2 position;

    private float speed;
    private float amount;

    readonly int maxCapacity = 10;

    // Use this for initialization
    void Start()
    {
        basePosition = transform.position;
        position = transform.position;

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

    public void addToCan(int quantity, int index)
    {
        if (!this.isFull())
        {
            fullitude += quantity;
            contenu[index]++;
        }
    }

    private void OnMouseDown()
    {
        if (this.isFull()) {
            //empty can 
            EmptyCan();
        }
    }

    private void EmptyCan() {
        fullitude = 0;
    }
}
