using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnManager : MonoBehaviour
{
    public int ID;
    public bool isSelected = false;
    public bool isWorking;

    public static bool mouseOverPawn = false;

    [Space(5)]
    public PawnController PawnController;


    private void OnMouseDown()
    {
        mouseOverPawn = true;
    }

    private void OnMouseUp()
    {
        mouseOverPawn = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BoxSelection>() && !isSelected)
        {
            PawnController.MultipleSelect(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BoxSelection>() && Input.GetMouseButton(0))
        {
            PawnController.DeSelect(this.gameObject);
        }
    }
}
