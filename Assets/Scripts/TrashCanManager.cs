using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanManager : MonoBehaviour
{

    public int fullitude;
    private Vector3 maxScale;
    private Vector3 baseScale;
    private Vector3 position;

    private float speed;
    private float amount;

    readonly int maxCapacity = 20;

    // Use this for initialization
    void Start()
    {
        maxScale = new Vector3(3F, 3F, 3F);
        baseScale = new Vector3(2F, 2F, 1F);
        position = transform.position;

        this.fullitude = 0;
        this.speed = 40;
        this.amount = 0.06F;
    }

    void Update()
    {
        if (this.isFull())
        {
            position.x = Mathf.Sin(Time.time * speed) * amount;
            position.z = Mathf.Cos(Time.time * speed) * amount;
            transform.position = position;
            print("Is full");

        }

    }

    public bool isFull()
    {
        return (fullitude >= maxCapacity);
    }

    public void addToCan(int nTrash)
    {
        if (nTrash < 1)
        {
            print("Error tash is negative " + nTrash);
        }
        else
        {
            fullitude += nTrash;
        }
    }


    private void OnMouseOver()
    {
        if (transform.localScale.x < maxScale.x)
        {
            //set grow animation
            transform.localScale += new Vector3(0.2F, 0.2F, 0.2F);
        }


    }
    void OnMouseExit()
    {
        //set default size
        transform.localScale = baseScale;
    }
    private void OnMouseDown()
    {
        //empty can 
        print("CAN :: emptying ");
        fullitude = 0;

    }


}