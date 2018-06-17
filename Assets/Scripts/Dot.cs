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

    //board 
    private Board board;

    public int targetX;
    public int targetY;
    public bool isMatched = false;

    public bool isTrashed = false;
    public bool isComposted = false;

    private FindMatches findMatches;

    [Header("Swipe")]
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;

    public float swipeAngle = 0;
    public float swipeResist = 1f;
    public GameObject trash;
    public GameObject eco;

    public SoundManager Sound;

    void Start()
    {
        board = FindObjectOfType<Board>();
        findMatches = FindObjectOfType<FindMatches>();
        trash = GameObject.FindGameObjectWithTag(TrashMap());
        eco = GameObject.FindGameObjectWithTag("ECO");
    }

    string TrashMap()
    {
        string id = this.gameObject.tag.Substring(0, 1);
        switch (id)
        {
            case "T":
                return "Blue";
            case "C":
                return "Brown";
            case "G":
                return "Green";
            case "W":
                return "White";
            case "Y":
                return "Yellow";
            default:
                return "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTrashed)
        {
            if (isMatched)
            {
                SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
                mySprite.color = new Color(1f, 1f, 1f, .2f);
            }
            targetX = column;
            targetY = row;
            if (Mathf.Abs(targetX - transform.position.x) > .1)
            {
                // Move toward the target
                tempPosition = new Vector2(targetX, transform.position.y);
                transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
                if (column >= 0 && row >= 0 && board.allDots[column, row] != this.gameObject)
                {
                    board.allDots[column, row] = this.gameObject;
                }
                findMatches.FindAllMatches();
            }
            else
            {
                // Directly set the position
                tempPosition = new Vector2(targetX, transform.position.y);
                transform.position = tempPosition;
            }
            if (Mathf.Abs(targetY - transform.position.y) > .1)
            {
                // Move toward the target
                tempPosition = new Vector2(transform.position.x, targetY);
                transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
                if (column >= 0 && row >= 0 && board.allDots[column, row] != this.gameObject)
                {
                    board.allDots[column, row] = this.gameObject;
                }
                findMatches.FindAllMatches();
            }
            else
            {
                // Directly set the position
                tempPosition = new Vector2(transform.position.x, targetY);
                transform.position = tempPosition;
            }
        }
        else
        {
            if (isComposted) {
                transform.position = Vector2.Lerp(transform.position, eco.transform.position, 0.05f);
            } else {
                transform.position = Vector2.Lerp(transform.position, trash.gameObject.transform.GetChild(0).gameObject.transform.position, 0.05f);
            }
        }
    }

    public IEnumerator CheckMoveCo()
    {
        yield return new WaitForSeconds(.5f);
        if (otherDot != null)
        {
            if (!isMatched && !otherDot.GetComponent<Dot>().isMatched)
            {
                otherDot.GetComponent<Dot>().row = row;
                otherDot.GetComponent<Dot>().column = column;
                row = previousRow;
                column = previousColumn;
                yield return new WaitForSeconds(.5f);
                board.currentState = GameState.move;
            }
            else
            {
                board.DestroyMatches();
            }
            otherDot = null;
        }
    }

    private void OnMouseDown()
    {
        //if mouse pressed
        if (board.currentState == GameState.move)
        {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseUp()
    {
        if (board.currentState == GameState.move)
        {
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
            //Sound.Playswipe();

        }
    }

    void CalculateAngle()
    {
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MovePieces();
            board.currentState = GameState.wait;
        }
        else
        {
            board.currentState = GameState.move;
        }
    }

    void MovePieces()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width - 1)
        {
            // Right Swipe
            otherDot = board.allDots[column + 1, row];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().column -= 1;
            column += 1;
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1)
        {
            // Up Swipe
            otherDot = board.allDots[column, row + 1];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().row -= 1;
            row += 1;
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)
        {
            // Left Swipe
            otherDot = board.allDots[column - 1, row];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().column += 1;
            column -= 1;
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            // Down Swipe
            otherDot = board.allDots[column, row - 1];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().row += 1;
            row -= 1;
        }
        StartCoroutine(CheckMoveCo());
    }

    void FindMatches()
    {
        if (column > 0 && column < board.width - 1)
        {
            GameObject leftDot1 = board.allDots[column - 1, row];
            GameObject rightDot1 = board.allDots[column + 1, row];
            if (leftDot1 != null && rightDot1 != null)
            {
                if (leftDot1.tag == this.gameObject.tag && rightDot1.tag == this.gameObject.tag)
                {
                    leftDot1.GetComponent<Dot>().isMatched = true;
                    rightDot1.GetComponent<Dot>().isMatched = true;
                    isMatched = true;
                }
            }
        }
        if (row > 0 && row < board.height - 1)
        {
            GameObject UpDot1 = board.allDots[column, row + 1];
            GameObject DownDot1 = board.allDots[column, row - 1];
            if (UpDot1 != null && DownDot1 != null)
            {
                if (UpDot1.tag == this.gameObject.tag && DownDot1.tag == this.gameObject.tag)
                {
                    UpDot1.GetComponent<Dot>().isMatched = true;
                    DownDot1.GetComponent<Dot>().isMatched = true;
                    isMatched = true;
                }
            }
        }
    }

    public void Trash()
    {
        StartCoroutine("TrashGarbage");
    }

    public IEnumerator TrashGarbage()
    {
        yield return new WaitForSeconds(Random.value/4);
        isTrashed = true;
        trash.GetComponent<Trash>().addToCan(1);

    }

    public void Recycle()
    {
        StartCoroutine("RecycleGarbage");
    }

    public IEnumerator RecycleGarbage()
    {
        yield return new WaitForSeconds(Random.value / 4);
        isComposted = true;
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);

    }

}