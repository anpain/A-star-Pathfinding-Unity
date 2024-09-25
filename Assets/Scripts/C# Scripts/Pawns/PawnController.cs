using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnController : MonoBehaviour
{
    public GameObject[] allPawns;
    public List<GameObject> selectedPawns = new List<GameObject>();


    private void Start()
    {
        allPawns = (GameObject.FindGameObjectsWithTag("Player"));

        for (int i = 0; i < allPawns.Length; i++)
            allPawns[i].GetComponent<PawnManager>().ID = i + 1;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 raycastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    GameObject Pawn = hit.collider.gameObject;

                    if (Input.GetKey(KeyCode.LeftControl) && !Pawn.GetComponent<PawnManager>().isSelected)
                    {
                        AddToSelected(Pawn);
                    }
                    else if(!Pawn.GetComponent<PawnManager>().isSelected)
                    {
                        OneSelect(Pawn);
                    }
                }
            }
            else if (selectedPawns.Count > 0)
            {
                DeSelectAll();
            }
        }
    }

    private void OneSelect(GameObject Pawn)
    {
        DeSelectAll();
        Pawn.GetComponent<PawnManager>().isSelected = true;
        selectedPawns.Add(Pawn);
    }

    public void MultipleSelect(GameObject Pawn)
    {
        Pawn.GetComponent<PawnManager>().isSelected = true;
        selectedPawns.Add(Pawn);
    }

    private void AddToSelected(GameObject Pawn)
    {
        Pawn.GetComponent<PawnManager>().isSelected = true;
        selectedPawns.Add(Pawn);
    }

    public void DeSelect(GameObject Pawn)
    {
        Pawn.GetComponent<PawnManager>().isSelected = false;

        if (selectedPawns.Count > 0)
        {
            for (int i = 0; i < selectedPawns.Count; i++)
            {
                if(selectedPawns[i] == Pawn)
                    selectedPawns.RemoveAt(i);
            }
        }
    }

    private void DeSelectAll()
    {
        if (selectedPawns.Count > 0)
        {
            for (int i = 0; i < selectedPawns.Count; i++)
            {
                selectedPawns[i].GetComponent<PawnManager>().isSelected = false;
            }
            selectedPawns.Clear();
        }
    }

}
