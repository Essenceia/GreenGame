using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMatches : MonoBehaviour {

    private Board board;
    AudioSource playerTrashSound;
   // public AudioClip trashSound; 
    
    public List<GameObject> currentMatches = new List<GameObject>();

    private bool playMusic; 

	// Use this for initialization
	void Start () {
        playerTrashSound = GetComponent<AudioSource>(); 
        board = FindObjectOfType<Board>();
        
	}


    //Start cooroutine to seatch for all matches in board 
    public void FindAllMatches() {

        StartCoroutine(FindAllMatchesCo());
    }

    private IEnumerator FindAllMatchesCo(){
        playMusic = false;
        yield return new WaitForSeconds(.2f);
        //iterate all the board
        for (int i = 0; i < board.width; i++) {
            for (int j = 0; j < board.height; j++) {
                GameObject currentDot = board.allDots[i, j];
                //chack if the board has trash 
                if (currentDot != null) {
                    if (i > 0 && i < board.width - 1) {
                        //check if we have a line
                        GameObject leftDot = board.allDots[i - 1, j];
                        GameObject rightDot = board.allDots[i + 1, j];
                        if (leftDot != null && rightDot != null) {
                            if (leftDot.tag == currentDot.tag && rightDot.tag == currentDot.tag) {
                                //collone found add to match list to be counted in and remove
                                if (!currentMatches.Contains(leftDot)) {
                                    currentMatches.Add(leftDot);
                                }
                                leftDot.GetComponent<Dot>().isMatched = true;
                                if (!currentMatches.Contains(rightDot))
                                {
                                    currentMatches.Add(rightDot);
                                }
                                rightDot.GetComponent<Dot>().isMatched = true;
                                if (!currentMatches.Contains(currentDot))
                                {
                                    currentMatches.Add(currentDot);
                                }
                                currentDot.GetComponent<Dot>().isMatched = true;
                                playMusic = true; 
                            }
                        }
                    }
                    //check if we have a collone
                    if (j > 0 && j < board.height - 1)
                    {
                        GameObject upDot = board.allDots[i, j + 1];
                        GameObject downDot = board.allDots[i, j - 1];
                        if (upDot != null && downDot != null)
                        {
                            if (upDot.tag == currentDot.tag && downDot.tag == currentDot.tag)
                            {
                                //line found add to match list to be counted in and remove
                                if (!currentMatches.Contains(upDot))
                                {
                                    currentMatches.Add(upDot);
                                }
                                upDot.GetComponent<Dot>().isMatched = true;
                                if (!currentMatches.Contains(downDot))
                                {
                                    currentMatches.Add(downDot);
                                }
                                downDot.GetComponent<Dot>().isMatched = true;
                                if (!currentMatches.Contains(currentDot))
                                {
                                    currentMatches.Add(currentDot);
                                }
                                currentDot.GetComponent<Dot>().isMatched = true;
                                playMusic = true; 
                            }
                        }
                    }
                }
            }
        }
        //at least one match found play music
        if(playMusic == true)
        {
            playerTrashSound.Play();
            print("playing swipe sound once"); 
            
        }
    }
	
}
