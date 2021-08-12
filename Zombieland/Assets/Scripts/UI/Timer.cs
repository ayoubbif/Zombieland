using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    public static Timer singleton; 
    private float timer = 0f;
    private bool isTimer = false;
    
    void Awake()
    {
        singleton = this; //Defining the singleton so that the timer only starts when we click on Play in the settings menu
    }
    void Update()
    {
        if (isTimer)
        {
            timer += Time.deltaTime; //Increasing the timer over time
            TimerDisplay();
        }
    }

    void TimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer - minutes * 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); //Formatting the timer text into a mins/secs format
    }

    public void StartTimer()
    {
        isTimer = true;
    }

    public void StopTimer()
    {
        isTimer = false;
    }

    public void ResetTimer()
    {
        timer = 0f;
    }

}
