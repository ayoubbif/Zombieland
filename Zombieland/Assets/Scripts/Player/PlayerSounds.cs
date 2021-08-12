using UnityEngine;
public class PlayerSounds : MonoBehaviour
{
    [SerializeField] AudioClip walkSound;
    [SerializeField] AudioClip walkConcreteSound;
    [SerializeField] AudioClip reloadClip;
    [SerializeField] AudioClip batteryPickupSound;
    [SerializeField] AudioClip healthPickupClip;

    AudioClip currentWalkSound;

    public float footstepDelayWalking;
    public float footstepDelayRunning;

    float footstepDelay;
    float nextFootstep = 0;

    private void Start()
    {
        footstepDelay = footstepDelayWalking;
    }

    public void PlayReloadSound()
    {
        GetComponent<AudioSource>().PlayOneShot(reloadClip);
    }

    public void PlayBatteryPickupSound()
    {
        GetComponent<AudioSource>().PlayOneShot(batteryPickupSound);
    }
    public void PlayHealthPickupSound()
    {
        GetComponent<AudioSource>().PlayOneShot(healthPickupClip);
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Ground")
        {
            currentWalkSound = walkSound;
        }
        else if (other.gameObject.tag == "Concrete")
        {
            currentWalkSound = walkConcreteSound;
        }
    }
    public void SetFootstepsRunning(bool isRunning)
    {
        if (isRunning)
        {
            footstepDelay = footstepDelayRunning;
        }
        else
        {
            footstepDelay = footstepDelayWalking;
        }
    }
    void Update()
    {
        //WASD input for footsteps
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
        {
            nextFootstep -= Time.deltaTime;
            if (nextFootstep <= 0)
            {
                GetComponent<AudioSource>().PlayOneShot(currentWalkSound, 0.7f);
                nextFootstep += footstepDelay;
            }
        }
    }
}