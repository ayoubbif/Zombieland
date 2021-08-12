using UnityEngine;

public class Pause : MonoBehaviour 
{
    [SerializeField] private GameObject pausePanel;

    void Start()
    {
        AppStartup startupObject = FindObjectOfType<AppStartup>();

        // Start the game when difficulty is set
        if (startupObject) {
            Destroy(startupObject, 1);
            PauseGame();
        } else {
            pausePanel.SetActive(false);
        }
    }
    void Update()
    {
        if(Input.GetKeyDown (KeyCode.Escape)) //Input manager for pause panel
        {
            if (!pausePanel.activeInHierarchy) 
            {
                PauseGame();

            } else if (pausePanel.activeInHierarchy) 
            {
                ContinueGame();   
            }
        } 
     }
    private void PauseGame()
    {
        pausePanel.SetActive(true); //Enable the pause panel

        //Disable weapon scripts that still work while time scale is equal to 0
        Time.timeScale = 0;
        FindObjectOfType<WeaponSwitcher>().enabled  = false;
        FindObjectOfType<Weapon>().enabled  = false;
        
        //Lock and hide the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;       
    } 
    public void ContinueGame()
    {
        //Enable the weapon scripts again
        Time.timeScale = 1;
        FindObjectOfType<WeaponSwitcher>().enabled  = true;
        FindObjectOfType<Weapon>().enabled  = true;

        pausePanel.SetActive(false); //Disable the pause panel

        
    }
}