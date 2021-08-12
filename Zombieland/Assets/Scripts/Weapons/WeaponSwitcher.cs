using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;

    void Start()
    {
        SetActiveWeapon();
    }

    private void SetActiveWeapon() //Setting active weapon using indexes
    {
        int weaponIndex = 0;

        foreach (Transform weapon in transform)
        {
            weaponIndex = SwapWeapon(weaponIndex, weapon);
        }
    }

    private int SwapWeapon(int weaponIndex, Transform weapon)
    {
        if (weaponIndex == currentWeapon)
        {
            weapon.gameObject.SetActive(true);
        }
        else
        {
            weapon.gameObject.SetActive(false);
        }
        weaponIndex++;
        return weaponIndex;
    }

    void Update()
    {
        int previousWeapon = currentWeapon;
        ProcessKeyInput();
        ProcessScrollWheel();

        if (previousWeapon != currentWeapon)
        {
            SetActiveWeapon();
        }
    }

    private void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0; // Pistol
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1; // Shotgun
        }
    }

    private void ProcessScrollWheel()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0) // backward
        {
            CycleForward();
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
        {
            CycleBackward();
        }
    }

    private void CycleBackward()
    {
        if (currentWeapon == 0)
        {
            currentWeapon = transform.childCount - 1;
        }
        else
        {
            currentWeapon--;
        }
    }

    private void CycleForward()
    {
        if (currentWeapon >= transform.childCount - 1)
        {
            currentWeapon = 0;
        }
        else
        {
            currentWeapon++;
        }
    }
}