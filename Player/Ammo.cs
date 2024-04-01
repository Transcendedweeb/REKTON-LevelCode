using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] int smgStartingMags;
    [SerializeField] int arStartingMags;
    [SerializeField] int pistolStartingMags;
    [SerializeField] int sgStartingAmount;
    [SerializeField] int smgMaxMags;
    [SerializeField] int arMaxMags;
    [SerializeField] int pistolMaxMags;
    [SerializeField] int sgMaxAmount;

    public int GetMagsAmount(string gunKind)
    {
        if(gunKind == "smg") return smgStartingMags;
        else if(gunKind == "ar") return arStartingMags;
        else if (gunKind == "sg") return sgStartingAmount;
        else return pistolStartingMags;
    }

    public void RemoveMag(string gunKind)
    {
        if(gunKind == "smg")
        {
            smgStartingMags -=1;
        }
        else if(gunKind == "ar")
        {
            arStartingMags-=1;
        }
        else if(gunKind == "sg")
        {
            sgStartingAmount-=1;
        }
        else
        {
            pistolStartingMags-=1;
        }
    }

    public void MaxAmmo()
    {
        smgStartingMags = smgMaxMags;
        arStartingMags = arMaxMags;
        pistolStartingMags = pistolMaxMags;
        sgStartingAmount = sgMaxAmount;
    }
}
