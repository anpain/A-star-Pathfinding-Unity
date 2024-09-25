using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartGame : MonoBehaviour
{
    public TMP_InputField seedField;
    public Scrollbar modeScrollbar;
    public int seed;
    public int mode;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void NewGame()
    {
        if (seedField.text == null)
            seed = Random.Range(0, 100000);

        if (modeScrollbar.value > 0.875f) //Пустошь
        {
            mode = 1;
        }
        else if (modeScrollbar.value > 0.625f) //Острова
        {
            mode = 2;

        }
        else if (modeScrollbar.value > 0.375f) //Обычная  
        {
            mode = 3;

        }
        else if (modeScrollbar.value > 0.175f)//Немного гористая
        {
            mode = 4;

        }
        else
        { //Горы
            mode = 5;

        }

    SceneManager.LoadScene("World");
    }
}
