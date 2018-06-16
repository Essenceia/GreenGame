using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//scrap on board prefabe for all detritus ( franglish )
public class Dot : MonoBehaviour
{
    [Header("Board Vars")]
    //current values
    public int column;
    public int row;
    private GameObject otherDot;

    //old values
    public int previousColumn;
    public int previousRow;

    public bool isSelected = false;

    //board 
    private Board board;

    public int targetX;
    public int targetY;
    public bool isMatched = false;

    [Header("Swipe")]
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;

    public float swipeAngle = 0;
    public float swipeResist = 1f;

    public int typeID;
    // Use this for initialization
    void Start()
    {
        board = FindObjectOfType<Board>();
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        row = targetY;
        column = targetX;
        previousRow = row;
        previousColumn = column;
    }

    // Update is called once per frame
    void Update()
    {
        findMatches();
        if (isMatched) {
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.color = new Color(0f, 0f, 0f, .2f);
        }
        targetX = column;
        targetY = row;
        if (Mathf.Abs(targetX - transform.position.x) > .1) {
            // Move toward the target
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        } else {
            // Directly set the position
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
            board.allDots[column, row] = this.gameObject;
        }
        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            // Move toward the target
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else
        {
            // Directly set the position
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
            board.allDots[column, row] = this.gameObject;
        }
    }

    public IEnumerator CheckMoveCo(){
        yield return new WaitForSeconds(.2f);
        if (otherDot != null) {
            if (!isMatched && !otherDot.GetComponent<Dot>().isMatched) {                otherDot.GetComponent<Dot>().row = row;
                otherDot.GetComponent<Dot>().column = column;
                row = previousRow;
                column = previousColumn;
            }
            otherDot = null;
        }
    }

    private void OnMouseDown()
    {
        //if mouse pressed
        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
    }

    void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
        //Debug.Log(swipeAngle);
        MovePieces();
    }

    void MovePieces() {
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width - 1) {
            // Right Swipe
            otherDot = board.allDots[column + 1, row];
            otherDot.GetComponent<Dot>().column -= 1;
            column += 1;
        } else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1)
        {
            // Up Swipe
            otherDot = board.allDots[column, row + 1];
            otherDot.GetComponent<Dot>().row -= 1;
            row += 1;
        } else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)
        {
            // Left Swipe
            otherDot = board.allDots[column - 1, row];
            otherDot.GetComponent<Dot>().column += 1;
            column -= 1;
        } else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            // Down Swipe
            otherDot = board.allDots[column, row - 1];
            otherDot.GetComponent<Dot>().row += 1;
            row -= 1;
        }
        StartCoroutine(CheckMoveCo());
    }

    void findMatches(){
        if (column > 0 && column < board.width - 1) {
            GameObject leftDot1 = board.allDots[column - 1, row];
            GameObject rightDot1 = board.allDots[column + 1, row];
            if (leftDot1.tag == this.gameObject.tag && rightDot1.tag == this.gameObject.tag) {
                leftDot1.GetComponent<Dot>().isMatched = true;
                rightDot1.GetComponent<Dot>().isMatched = true;
                isMatched = true;
            }
        }
        if (row > 0 && row < board.height - 1)
        {
            GameObject UpDot1 = board.allDots[column, row + 1];
            GameObject DownDot1 = board.allDots[column, row - 1];
            if (UpDot1.tag == this.gameObject.tag && DownDot1.tag == this.gameObject.tag)
            {
                UpDot1.GetComponent<Dot>().isMatched = true;
                DownDot1.GetComponent<Dot>().isMatched = true;
                isMatched = true;
            }
        }
    }


}