using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour {

    //board size 
    //readonly int colum = 7;
    //readonly int row = 9;

    readonly int[] nbDechet = { 4, 4, 5, 6, 7 };

    //objectifs - todo set correct
    readonly int[] objectifPrefabIndex = { 0, 1, 2, 3, 4, 5, 6, 7 };
    //matches index of ellements on board
    readonly int[] objectifDechetPrefabIndex = { 0, 1, 2, 3, 4, 5, 6, 7 };

    //nb in objectif
    readonly int[] nbObjectif = { 4, 5, 6, 5, 7 };

    public int loadLvl;
    public int[] LvlDechetsPrefabIndex;
    public int LvlObjectifPrefabIndex;
    public int LvlObjectifNb;


    // Use this for initialization
    void Start()
    {
        loadLvl = 0;
    }

    /*public int getNbCan()
    {
        return 2;
    }*/
    /*public int getDechetPrefabIndex()
    {
        if (loadLvl < 1)
        {
            print("Error loading level num " + loadLvl);
        }
        int type = Random.Range(0, LvlDechetsPrefabIndex.Length);
        return LvlDechetsPrefabIndex[type];
    }*/

    public void Load(int newlvl)
    {
        this.loadLvl = newlvl;
        this.getDechets();
        // return generateNewBoard();
    }

    /*private generateNewBoard()
    {
        //randomly fill board
        int type;
        string [,] newBoard = new string[this.colum,this.row];
        for (int i = 0; i < colum; i++)
        {
            for (int j = 0; j < row; j++)
            {
                type = Random.Range(0, LvlDechets.Length);
                newBoard[i,j] = LvlDechets[type];
            }
        }
        return newBoard;

    }*/

    private void getDechets()
    {
        int arrL = getNbDechets();
        //init array
        this.LvlDechetsPrefabIndex = new int[arrL];
        //get objectif index nub
        int objIndex = getObjectif();
        this.LvlObjectifPrefabIndex = this.objectifPrefabIndex[objIndex];
        //set 1 ell to match objectif
        this.LvlDechetsPrefabIndex[0] = this.objectifDechetPrefabIndex[objIndex];
        //set objectif num
        this.LvlObjectifNb = this.getNbObjectif();

        //fill array with random other dechets
        for (int i = 1; i < this.LvlDechetsPrefabIndex.Length; i++)
        {
            this.LvlDechetsPrefabIndex[i] = objectifDechetPrefabIndex[Random.Range(0, objectifDechetPrefabIndex.Length)];
        }
    }

    private int getNbDechets()
    {
        if (this.loadLvl < nbDechet.Length)
        {
            return nbDechet[this.loadLvl];
        }
        else
        {
            int retVal = Random.Range(0, this.loadLvl / 3);
            if (retVal > 4)
            {
                retVal = 3;
            }
            return 4 + retVal;
        }
    }

    private int getObjectif()
    {
        if (this.loadLvl < objectifPrefabIndex.Length)
        {
            return this.loadLvl;
        }
        else
        {
            int retVal = Random.Range(0, objectifPrefabIndex.Length);

            return retVal;
        }
    }

    private int getNbObjectif()
    {
        if (this.loadLvl < nbObjectif.Length)
        {
            return nbObjectif[this.loadLvl];
        }
        else
        {
            int retVal = Random.Range(0, this.loadLvl * 3);
            if (retVal > 20)
            {
                retVal = 20;
            }
            return retVal;
        }
    }
}
