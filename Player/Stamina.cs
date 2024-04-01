using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stamina : MonoBehaviour
{
    public Slider staminaBar;
    IEnumerable coroutine;
    WaitForSeconds regenTick = new WaitForSeconds(.1f);
    Coroutine regen;

    [SerializeField] int maxStamina= 100;
    int currentStamina;
    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    public void UseStamina(int amount){
        if(currentStamina - amount >= 0){
            currentStamina -= amount;
            staminaBar.value = currentStamina;

            if (regen != null){ StopCoroutine(regen); }
            regen = StartCoroutine(RegenStamina());
        }else {
            Debug.Log("No Stamina");
        }
    }

    public int GetCurrentStamina(){
        return currentStamina;
    }

    private IEnumerator RegenStamina(){
        yield return new WaitForSeconds(2);

        while(currentStamina<maxStamina){
            currentStamina += maxStamina / 100;
            staminaBar.value = currentStamina;
            yield return regenTick;
        }
        regen = null;
    }
}