using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CellBehaviour : MonoBehaviour, IPointerClickHandler
{

    public Sprite x_image;
    int NeighbourhoodCount = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().sprite = x_image;
        gameObject.tag = "x";

        // 1 iç kare kontrol edilmesi yeterlidir.

        int Index = Convert.ToInt32(gameObject.name);
        int NeighbourCount = 0;
        int[] Neighbours = new int[4];
        int[] NeighbourGo = new int[4];

        Neighbours[0] = Index + 1;                      //right
        Neighbours[1] = Index - 1;                      //left
        Neighbours[2] = Index - Globals.row_size;       //top
        Neighbours[3] = Index + Globals.row_size;       //bottom

        for(int i = 0; i < Neighbours.Length; i++)
        {
            if (GameObject.Find(Neighbours[i].ToString()).tag == "x")
            {
                // komşudur
                NeighbourGo[NeighbourCount] = Neighbours[i];
                if (IsThereNeighbourOfNeighbour(Neighbours[i], i))
                {
                    Destroy(GameObject.Find(Neighbours[i].ToString()));
                    Destroy(this.gameObject);
                    Globals.match_count++; 
                    return;
                }

                NeighbourCount++;
            }
        }

        
        if(NeighbourCount >= 2)
        {
            // komşuları sil
            for(int i = 0; i < NeighbourCount; i++)
            {
                Destroy(GameObject.Find(NeighbourGo[i].ToString()));        
            }
            Destroy(gameObject);
            Globals.match_count++;
        }
        
    }

    bool IsThereNeighbourOfNeighbour(int neighbour_index , int direction)
    {
        bool result = false;
        int NeighbourCount = 0;
        int[] Neighbours = new int[3];


        if(direction == 0)
        {
            Neighbours[0] = neighbour_index + 1;
            Neighbours[1] = neighbour_index - Globals.row_size;
            Neighbours[2] = neighbour_index + Globals.row_size;
        }
        else if(direction == 1)
        {
            Neighbours[0] = neighbour_index - 1;
            Neighbours[1] = neighbour_index - Globals.row_size;
            Neighbours[2] = neighbour_index + Globals.row_size;
        }
        else if(direction == 2)
        {
            Neighbours[0] = neighbour_index + 1;
            Neighbours[1] = neighbour_index - 1;
            Neighbours[2] = neighbour_index - Globals.row_size;
        }
        else if(direction == 3)
        {
            Neighbours[0] = neighbour_index + 1;
            Neighbours[1] = neighbour_index - 1;
            Neighbours[2] = neighbour_index + Globals.row_size;
        }

        for (int i = 0; i < Neighbours.Length; i++)
        {
            if (GameObject.Find(Neighbours[i].ToString()).tag == "x")
            {
                // komşudur
                Destroy(GameObject.Find(Neighbours[i].ToString()));

                NeighbourCount++;

                result = true;
            }
        }

        return result;
    }

}
