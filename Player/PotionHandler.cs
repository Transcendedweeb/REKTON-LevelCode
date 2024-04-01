using UnityEngine;
using UnityEngine.UI;

public class PotionHandler : MonoBehaviour
{
    [SerializeField] GameObject potionBottle;
    [SerializeField] int startingAmountOfPotions = 3;
    [SerializeField] Text potionAmountText;
    public bool isUsingPotion = false;
    public int amountLeft;

    private void Start() {
        amountLeft = startingAmountOfPotions;
        potionAmountText.text = amountLeft.ToString();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C) && !isUsingPotion && !CheckFlags() && amountLeft > 0){
            isUsingPotion = true;
            potionBottle.SetActive(true);
            potionBottle.GetComponent<Potion>().ActivateThis();
        }
    }

    bool CheckFlags()
    {
        bool[] flagsArray = {
            GetComponentInChildren<Reload>().IsReloading(),
            GetComponentInChildren<Shoot>().IsADSActive(),
        };

        foreach(bool flag in flagsArray)
        {
            if(flag) return true;
        }
        return false;
    }
}
