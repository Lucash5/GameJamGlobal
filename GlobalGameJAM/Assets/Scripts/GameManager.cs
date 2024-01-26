using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //instanssi jota voidaan kutusa mist� vaan
    public static GameManager instance;


    private void Awake()
    {
        //Tarkistetaan onko gameManagerista olemassa jo instanssi
        // jos ei, niin instanssi = t�m� gameManager
        // Jos on jo olemassa tuhotaan t�m� objekti
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
