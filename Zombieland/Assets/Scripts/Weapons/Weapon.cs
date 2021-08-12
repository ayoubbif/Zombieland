using System.Collections;
using TMPro;
using UnityEngine;


public class Weapon : MonoBehaviour
{   
    [SerializeField] Camera FPSCamera;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject hitImpact;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] AudioClip fireWeaponClip;
    
    [SerializeField] float range = 50f;
    [SerializeField] float damage = 15f;
    [SerializeField] float timeBetweenShots = 0.5f;

    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] TextMeshProUGUI ammoText;

    AudioSource weaponAudioSource;

    public bool canShoot = true;
    public bool isFiring = false;

    private void OnEnable() 
    {
        canShoot = true; //Fix the bug when swapping the weapons
    }
    private void Start() 
    {
        weaponAudioSource = GetComponent<AudioSource>();
    }
    void Update()
    {        
        DisplayAmmo(); //Display the current ammo amount

        if (Input.GetButtonDown("Fire1") && canShoot == true)
        {
            StartCoroutine(FireWeapon());
            StartCoroutine(PlayShootAnimation());
        }
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetAmmoCount(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    IEnumerator FireWeapon()
    {
        canShoot = false;
        if (ammoSlot.GetAmmoCount(ammoType) > 0) //If the magazine isn't empty, start the firing sequence
        {
            StartFiringSequence();
        }
        yield return new WaitForSeconds(timeBetweenShots); //Wait for the time between each shot
        canShoot = true;
    }
    private void StartFiringSequence()
    {
        StartCoroutine(PlayMuzzleFlash());
        PlayWeaponAudio();
        ProcessRaycast();
        ammoSlot.ReduceAmmo(ammoType); //Reduce the ammo when a bullet is shot
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        bool isHit = Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out hit, range); //Sending a raycast from our FPS camera with a given range 

        if (isHit) //If the raycast hit a collider
        {
            CreateHitImpact(hit); 
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>(); //Defining our enemy target
            if (target == null) { return; } //If the target isn't enemy target, do nothing
            target.TakeDamage(damage); //The target takes damage
        } else { return; }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitImpact, hit.point, Quaternion.LookRotation(hit.normal)); //Instantiate the hit impact
        Destroy(impact, .15f); //Destroy after 0.15 seconds
    }

    private void PlayWeaponAudio() //Play the gunshot audio clip
    {
        weaponAudioSource.clip = fireWeaponClip;
        weaponAudioSource.PlayOneShot(fireWeaponClip);
    }

    IEnumerator PlayMuzzleFlash() //Play the muzzle flash particles
    {
        muzzleFlash.Play();
        yield return new WaitForEndOfFrame();
        muzzleFlash.Stop();
    }
    IEnumerator PlayShootAnimation() //Play the recoil animation
    {
        isFiring = true;
        weapon.GetComponent<Animator>().Play("Gunshot");
        yield return new WaitForSeconds(0.25f);
        weapon.GetComponent<Animator>().Play("Empty State");
        isFiring = false; //So that the animation would only play once
    }
}
