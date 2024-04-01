using UnityEngine;

public class GunHandler : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;

    void Start()
    {
        SetWeaponActive();
    }

    void Update()
    {
        int previousWeapon = currentWeapon;
        ProcessKeyInput();

        if(previousWeapon != currentWeapon) SetWeaponActive();
    }

    private void ProcessKeyInput()
    {
        if (CheckIfRunningAnimation()) return;

        if (Input.GetKeyDown(KeyCode.Alpha1)){
            currentWeapon = 0;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2)){
            currentWeapon = 1;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3)){
            currentWeapon = 2;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4)){
            currentWeapon = 3;
        }
    }

    private bool CheckIfRunningAnimation()
    {
        bool isReloading = GetComponentInChildren<Reload>().IsReloading();
        bool isADS = GetComponentInChildren<Shoot>().IsADSActive();
        if (isReloading || isADS) return true;
        else return false;
    }

    private void SetWeaponActive()
    {
        int weaponIndex = 0;

        foreach(Transform weapon in transform)
        {
            if(weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
                GetComponentInChildren<Reload>().UpdateGameText();
            } 
            else weapon.gameObject.SetActive(false);
            weaponIndex++;
        }
    }
}
