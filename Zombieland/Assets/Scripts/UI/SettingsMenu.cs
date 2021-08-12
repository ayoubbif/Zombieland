using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Button easyButton, mediumButton, hardButton, playButton, quitButton;

    [SerializeField] Pause pauseMenu;

    bool gameJustStarted = true;

    DifficultyLevel difficulty;

    void Start()
    {
        //Add listeners to each button
        hardButton.onClick.AddListener(() => SetDifficulty(DifficultyLevel.Hard, hardButton));
        mediumButton.onClick.AddListener(() => SetDifficulty(DifficultyLevel.Medium, mediumButton));
        easyButton.onClick.AddListener(() => SetDifficulty(DifficultyLevel.Easy, easyButton));
        playButton.onClick.AddListener(ClickPlay);
        quitButton.onClick.AddListener(QuitGame);

        //Get the level difficulty
        GetDifficulty();
    }
    public DifficultyLevel GetDifficulty()
    {
        difficulty = (DifficultyLevel)PlayerPrefs.GetInt("difficulty", 1); //Store difficulty in PlayerPref
        return difficulty;
    }

    void ClickPlay()
    {
        PlayerPrefs.Save(); // Save the difficulty

        if (gameJustStarted)
        {   
            gameJustStarted = false;
        }

        pauseMenu.ContinueGame();

        Timer.singleton.StartTimer(); //Start the timer using the Timer singleton
    }

    void SetDifficulty(DifficultyLevel level, Button button) //Set the difficulty for each enemy in the scene
    {
        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();

        foreach (EnemyHealth enemy in enemies)
        {
            enemy.SetDifficulty(level);
        }

        int l = (int)level;
        PlayerPrefs.SetInt("difficulty", l);
        Debug.Log((DifficultyLevel)PlayerPrefs.GetInt("difficulty"));

    }

    private void QuitGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}