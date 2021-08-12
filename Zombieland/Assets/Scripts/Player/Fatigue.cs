using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Fatigue : MonoBehaviour
{
    float fatigueLimit = 1.0f;
    float fatigueLevel;
    float fatigueDecrease = 0.4f;
    float fatigueIncrease = 0.1f;

    [SerializeField] Slider FatigueBar;

    [SerializeField] RigidbodyFirstPersonController firstPersonController; //Using the RigidbodyFPSController from the Unity standard assets

    void Start()
    {
        fatigueLevel = fatigueLimit; //Defining fatigue level as the maximum fatigue limit 
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            DecreaseFatigue();
        }
        else if (fatigueLevel <= fatigueLimit) 
        {
            IncreaseFatigue();
        }

        FatigueBar.value = fatigueLevel;

        if (fatigueLevel <= .15)
        {
            firstPersonController.SetFatigue(true); //SetFatigue is defined on the FPSController script in Standard Assets folder
        } 
        else if (fatigueLevel >= .15)
        {
            firstPersonController.SetFatigue(false);
        }
    }

    public bool CanRun()
    {
        if (fatigueLevel >= 0.3) //Player can only run if fatigue level is greater than 0.3
        {
            return true;
        }
        return false;
    }
    private void DecreaseFatigue() //Descreasing fatigue level while holding the Shift button over time
    {
        if (fatigueLevel >= 0) {
            fatigueLevel -= fatigueDecrease * Time.deltaTime;
        }
    }
    private void IncreaseFatigue() //Increasing fatigue level while holding the Shift button over time
    {
        if (fatigueLevel <= fatigueLimit) {
           fatigueLevel += fatigueIncrease * Time.deltaTime;
        }
    }
}
