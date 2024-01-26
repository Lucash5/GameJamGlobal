using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Main Levels")]
    public Level[] levels;

    // Instanssi jota voidaan kutsua mistä vaan
    public static LevelManager Instance;

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
        SceneManager.sceneLoaded += OnSceneLoaded;

        ChangeLevel("Main Menu");
    }

    /// Tätä kutsutaan automaattisesti AINA kun Sceneä vaihdetaan
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.path == GetSceneDetails("Main Menu").ScenePath)
        {
            UIManager.Instance.OpenMainMenuUI();
            AudioManager.Instance.PlayMusicTrack(AudioManager.Instance.mainMenuMusic);
        }
        else
        {
            AudioManager.Instance.PlayMusicTrack(AudioManager.Instance.gameMusic);
            UIManager.Instance.OpenHUDPanel();
        }
    }

    /// <summary>
    /// Tällä haetaan tieto kentästä, onko se olemassa ja palautetaan Scene tiedosto 
    /// </summary>
    /// <param name="levelName"></param>
    /// <returns></returns>
    public SceneReference GetSceneDetails(string levelName)
    {
        foreach (Level level in levels)
        {
            if (level.levelName.Equals(levelName))
            {
                return level.scene;
            }
        }

        return null;
    }

    /// <summary>
    /// Tätä voidaan kutsua mistä vain käyttämällä LevelManager.Instance.ChangeLevel("levelin nimi")
    /// Sitten se vaihtaa kentä JOS se on olemassa sillä nimellä
    /// </summary>
    /// <param name="newLevelName"></param>
    public void ChangeLevel(string newLevelName)
    {
        if (GetSceneDetails(newLevelName) != null)
        {
            Debug.Log("LEVEL MANAGER - Loading Level: " + newLevelName);
            SceneManager.LoadScene(GetSceneDetails(newLevelName));
        }
        else
        {
            Debug.LogWarning("LEVEL MANAGER - Could not load level: " + newLevelName + ". Level does not exist in list");
        }
    }
}

[System.Serializable]
public class Level
{
    public string levelName;
    public SceneReference scene;
}