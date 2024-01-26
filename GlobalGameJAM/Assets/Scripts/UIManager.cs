using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [Header("Main Panels")]
    public GameObject mainMenuPanel;
    public GameObject hudPanel;
    public GameObject optionsPanel;

    // Instanssi jota voidaan kutsua mistä vaan
    public static UIManager Instance;

    void Awake()
    {
        // tarkistetaan onko GameManagerista olemassa jo instanssi
        // jos ei ole, niin instanssi = tämä gamemanager
        // Jos on olemassa, niin tuhotaan tämä objekti

        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        mainMenuPanel.SetActive(false);
        hudPanel.SetActive(false);
    }

    public void OpenMainMenuUI()
    {
        mainMenuPanel.SetActive(true);
        hudPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void OpenHUDPanel()
    {
        hudPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }


    public void StartButton(string levelToChangeTo)
    {
        LevelManager.Instance.ChangeLevel(levelToChangeTo);
    }
}