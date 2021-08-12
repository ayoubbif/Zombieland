using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathHandler : MonoBehaviour
{

    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI timeScore;

    public Button replayButton, quitButton;

    void Start()
    {
        gameOverCanvas.enabled = false; //Disable game over canvas
        replayButton.onClick.AddListener(ReplayGame); //Add listener to replay button
        quitButton.onClick.AddListener(QuitGame); //Add listener to quit button
    }

    public  void HandleDeath()
    {
        //Disable weapon scripts while time scale is equal to 0
        Time.timeScale = 0;
        FindObjectOfType<WeaponSwitcher>().enabled  = false;
        FindObjectOfType<Weapon>().enabled  = false;

        gameOverCanvas.enabled = true; //Enable game over canvas

        //Lock and hide the cursor
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true; 

        timeScore.text = timerText.text; //Display the timer score
    }
    
    private void ReplayGame()
    {
        SceneManager.LoadScene(1); //Load game scene 
    }
    private void QuitGame()
    {
        Time.timeScale = 1;
        Application.Quit(); //Quit
    }
}
