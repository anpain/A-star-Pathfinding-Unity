using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{
    public GameObject menu;

    public BuildingSystem BuildingSystem;

    public void Build()
    {
        BuildingSystem.destruction = false;
        BuildingSystem.building = true;
    }

    public void Destruct()
    {
        BuildingSystem.building = false;
        BuildingSystem.destruction = true;
    }

    public void OpenCloseMenu()
    {
        if (!menu.activeSelf)
        {
            menu.SetActive(true);
        }
        else
            menu.SetActive(false);
    }

}
