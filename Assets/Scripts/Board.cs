using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    wait, move
}

public class Board : MonoBehaviour {

    public GameState currentState = GameState.move;
    public int width;
    public int height;
    public int offset;
    public GameObject tilePrefab;
    public GameObject[] dots;
    public GameObject[,] allDots;
    private FindMatches findMatches;
    private TrashManager trashManager;
    public GameObject[] toCompost;
    public int compostCounter = 0;
    public GameObject[] toRecycle;
    public int recycleCounter = 0;
    public GameObject[] toGlass;
    public int glassCounter = 0;
    public GameObject[] toElectronic;
    public int electronicCounter = 0;
    public GameObject[] toWaste;
    public int wasteCounter = 0;
    public int level = 3; 
    //public UnityEngine.UI.Text hello;

    //public BurnScript burnScript;

    [Header("Level Manager")]
    private LevelManager levelManager;

    //public SoundManager Sound;



    // Use this for initialization
    void Start()
    {
        //Identify other managers by name
        findMatches = FindObjectOfType<FindMatches>();
        levelManager = FindObjectOfType<LevelManager>();
        trashManager = FindObjectOfType<TrashManager>();
        //burnScript = FindObjectOfType<BurnScript>();

        //Sound = FindObjectOfType<SoundManager>();

    
        allDots = new GameObject[width, height];

        //TODO : get current level
        levelManager.Load(level);

        SetUp();
        // TODO : add real trashes
        int[] canIdList = { 0, 1, 2, 3, 4 };
        trashManager.Init(canIdList);

        toCompost = new GameObject[30];
        toRecycle = new GameObject[30];
        toGlass = new GameObject[30];
        toElectronic = new GameObject[30];
        toWaste = new GameObject[30];
        // hello = GameObject.FindGameObjectWithTag("UUUIII");
	}
	
    private void SetUp() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                Debug.Log(i + " / " + j);

                Vector2 tempPosition = new Vector2(i, j + offset);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, tilePrefab.transform.rotation) as GameObject;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "(" + i + ", " + j + ")";
                int dotToUse = Random.Range(0, dots.Length);
                int maxIterations = 0;
                Debug.Log(i + " / " + j);
                while(MatchesAt(i, j, dots[dotToUse]) && maxIterations < 100) {
                    dotToUse = Random.Range(0, dots.Length);
                    maxIterations++;
                }
                maxIterations = 0;

                GameObject dot = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
                dot.GetComponent<Dot>().row = j;
                dot.GetComponent<Dot>().column = i;

                dot.transform.parent = this.transform;
                dot.name = "(" + i + ", " + j + ")";
                allDots[i, j] = dot;
                Debug.Log(i + " / " + j);
            }
        } 
    }

    private bool MatchesAt(int column, int row, GameObject piece){
        if (column > 1 && row > 1) {
            if (allDots[column - 1, row].tag == piece.tag && allDots[column - 2, row].tag == piece.tag) {
                return true;
            }
            if (allDots[column, row - 1].tag == piece.tag && allDots[column, row - 2].tag == piece.tag)
            {
                return true;
            }
        } else if (column <= 1 || row <= 1) {
            if (row > 1) {
                if (allDots[column, row - 1].tag == piece.tag && allDots[column, row - 2].tag == piece.tag) {
                    return true;
                }
            }
            if (column > 1)
            {
                if (allDots[column - 1, row].tag == piece.tag && allDots[column - 2, row].tag == piece.tag)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void DestroyMatchesAt(int column, int row) {
        if (allDots[column, row].GetComponent<Dot>().isMatched){
            findMatches.currentMatches.Remove(allDots[column, row]);
            allDots[column, row].GetComponent<Dot>().Trash();

            if (allDots[column,row].tag.Substring(0, 1) == "Y") {
                
            }

            switch (allDots[column, row].tag.Substring(0, 1))
            {
                case "Y":
                    toRecycle[recycleCounter] = allDots[column, row];
                    recycleCounter++;
                    break;
                case "C":
                    toCompost[compostCounter] = allDots[column, row];
                    compostCounter++;
                    break;
                case "W":
                    toGlass[glassCounter] = allDots[column, row];
                    glassCounter++;
                    break;
                case "T":
                    toElectronic[electronicCounter] = allDots[column, row];
                    electronicCounter++;
                    break;
                case "G":
                    toWaste[wasteCounter] = allDots[column, row];
                    wasteCounter++;
                    break;
                default:
                    print("Incorrect intelligence level.");
                    break;
            }

            //Destroy(allDots[column, row]);
            allDots[column, row] = null;
        }
    }

    public void DestroyMatches() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++){
                if (allDots[i, j] != null) {
                    DestroyMatchesAt(i, j);
                }
            }
        }

        //Sound.PlayMoveBell();


        StartCoroutine(DecreaseRowCo());
    }

    private IEnumerator DecreaseRowCo() {
        int nullCount = 0;
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (allDots[i, j] == null){
                    nullCount++;
                } else if (nullCount > 0) {
                    allDots[i, j].GetComponent<Dot>().row -= nullCount;
                    allDots[i, j] = null;
                }
            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(.2f);
        StartCoroutine(FillBoardCo());
    }

    private void RefillBoard() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (allDots[i, j] == null) {
                    Vector2 tempPosition = new Vector2(i, j + offset);
                    int dotToUse = Random.Range(0, dots.Length);
                    GameObject piece = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
                    allDots[i, j] = piece;
                    piece.GetComponent<Dot>().row = j;
                    piece.GetComponent<Dot>().column = i;
                    piece.transform.parent = this.transform;
                    piece.name = "(" + i + ", " + j + ")";
                }
            }
        }
    }

    private bool MatchesOnBoard() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (allDots[i, j] != null) {
                    if (allDots[i, j].GetComponent<Dot>().isMatched) {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private IEnumerator FillBoardCo() {
        RefillBoard();
        yield return new WaitForSeconds(.2f);

        while (MatchesOnBoard()) {
            yield return new WaitForSeconds(.3f);
            DestroyMatches();
        }
        yield return new WaitForSeconds(.2f);
        currentState = GameState.move;
    }
}
