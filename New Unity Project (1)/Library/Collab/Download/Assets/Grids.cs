using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Grids : MonoBehaviour
{
    public GameObject prefab;
    GameObject[,] CellArray;
    GameObject game_size_input;
    

    int sc_width;
    int sc_height;
    int cell_size;
    int cell_total_count;
    int row_count = 4; // as default

    public void Start()
    {
        game_size_input = GameObject.Find("SizeInput");
        sc_width = Screen.width;
        sc_height = Screen.height;
        Globals.row_size = row_count;
        CreateCell(row_count);
    }

    public void ApplySize()
    {
        DestroyCell(CellArray);
        row_count = GetGameSize(game_size_input.GetComponent<InputField>().text);
        Globals.row_size = row_count;
        CreateCell(row_count);
    }
    
    private int GetGameSize(string s)
    {
        int result = 0;
        try
        {
            result = Convert.ToInt32(s);
        }
        catch (FormatException f) 
        {
            Debug.Log(f.Message);
        }

        return result;
    }



    void CreateCell(int row)
    {
        int Index = 0;
        cell_total_count = (int)Mathf.Pow((float)row, 2);
        int column = row;
        cell_size = sc_width / row_count;

        CellArray = new GameObject[row,column];
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                CellArray[i,j] = (GameObject)Instantiate(prefab, transform);
                //CellArray[Index].AddComponent<CellBehaviour>();
                CellArray[i,j].GetComponent<RectTransform>().transform.position = new Vector3((cell_size / 2) + cell_size * j, sc_height - (cell_size / 2) - cell_size * i, 0);
                CellArray[i,j].transform.localScale = new Vector3(cell_size / 50, cell_size / 50, 0);
                CellArray[i,j].GetComponent<Image>().color = Color.grey;
                CellArray[i,j].tag = "not_x";
                CellArray[i,j].name = Convert.ToString(Index);

                Index++;
            }

        }
    }


    private bool DestroyCell(GameObject[,] go)
    {
        for(int i = 0; i < row_count; i++)
        {
            for(int j = 0; j < row_count; j++)
            {
                Destroy(go[i,j]);
            }
        }
        return true;
    }

}
