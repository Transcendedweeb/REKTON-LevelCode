using UnityEngine;
using UnityEngine.UI;

public class Reload : MonoBehaviour
{
    [SerializeField] float reloadTime;
    [SerializeField] GameObject gamer;
    [SerializeField] Text ammoText;
    [SerializeField] Text magsText;
    Ammo gamerMags;
    Shoot thisGun;
    string weaponKind;

    bool isReloading = false;
    private void Start() {
        gamerMags = gamer.GetComponent<Ammo>();
        thisGun = this.GetComponent<Shoot>();
        weaponKind = thisGun.weaponKind;
        magsText.text = gamerMags.GetMagsAmount(weaponKind).ToString();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.R) && !CheckFlags()){
            if(isReloading) return;
            CheckMagAmount();
        }
    }

    private void CheckMagAmount()
    {
        int magsLeft = gamerMags.GetMagsAmount(weaponKind);

        if(magsLeft > 0)
        {
            isReloading = true;
            GetComponent<Animator>().SetBool("IsReloading", true);
            Invoke("ReloadHandler", reloadTime);
        }
        else return;
    }

    private void ReloadHandler()
    {
        gamerMags.RemoveMag(weaponKind);
        magsText.text = gamerMags.GetMagsAmount(weaponKind).ToString();
        thisGun.ammoInChamber = thisGun.magCapacity;
        ammoText.text = thisGun.ammoInChamber.ToString();
        GetComponent<Animator>().SetBool("IsReloading", false);
        isReloading = false;
    }

    private bool CheckFlags()
    {
        bool[] flagsArray = {
            GetComponentInParent<PotionHandler>().isUsingPotion,
        };

        foreach(bool flag in flagsArray)
        {
            if(flag) return true;
        }
        return false;
    }

    public bool IsReloading()
    {
        return isReloading;
    }

    public void UpdateGameText()
    {
        gamerMags = gamer.GetComponent<Ammo>();
        thisGun = this.GetComponent<Shoot>();
        magsText.text = gamerMags.GetMagsAmount(weaponKind).ToString();
        ammoText.text = thisGun.ammoInChamber.ToString();
    }
}
