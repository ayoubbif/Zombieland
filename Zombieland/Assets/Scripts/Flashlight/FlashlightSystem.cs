using UnityEngine;

public class FlashlightSystem : MonoBehaviour
{

    [SerializeField] float lightDecay = 0.1f;
    [SerializeField] float angleDecay = 0.1f;

    [SerializeField] float minSpotAngle = 40.0f;

    Light myLight;

    float minLightIntensity = 0.0f;
    [SerializeField] float maxLightIntensity;
    [SerializeField] float maxSpotAngle;

    //Starting value for the lerp
    float newIntensity;
    float newSpotAngle;

    void Start()
    {
        myLight = GetComponent<Light>();
        myLight.intensity = maxLightIntensity;
        myLight.spotAngle =  maxSpotAngle;
        newIntensity = maxLightIntensity;
        newSpotAngle = maxSpotAngle;
    }

    void Update()
    {
        DecraseLightAngle();
        DecreaseLightIntensity();
    }

    public float GetLightIntensity()
    {
        return myLight.intensity;
    }
    public void RestoreLightAngle(float restoreAngle)
    {
        myLight.spotAngle = restoreAngle;
    }

    public void RestoreLightIntensity(float intensityAmmount)
    {
        myLight.intensity += intensityAmmount;
    }

    public void ChargeFlashlight(BatteryLevel level) 
    {
        if (level == BatteryLevel.Full)
        {
            
        }
        if (level == BatteryLevel.Half)
        {

        }
        if (level == BatteryLevel.Full) 
        {
            myLight.spotAngle = maxSpotAngle;
            myLight.intensity = maxLightIntensity;
            newIntensity = maxLightIntensity;
            newSpotAngle = maxSpotAngle;
        }
    }
    private void DecreaseLightIntensity()
    {
            if (myLight.intensity >= minLightIntensity)
            {
                myLight.intensity = newIntensity;
                newIntensity -= lightDecay * Time.deltaTime;
            }

    }
    private void DecraseLightAngle()
    {
        if (myLight.spotAngle >= minSpotAngle)
        {
            myLight.spotAngle = newSpotAngle;
            newSpotAngle -= angleDecay * Time.deltaTime;
        }

    }
}
