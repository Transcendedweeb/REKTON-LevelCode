using UnityEngine.UI;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] AudioClip drinking;
    [SerializeField] Text potionAmountText;
    AudioSource audioSource;
    public void ActivateThis()
    {
        Invoke("SetThisDisabled", 3f);
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(drinking);
    }

    public void SetThisDisabled()
    {
        GetComponentInParent<Health>().playerHealth = 100f;
        GetComponentInParent<PotionHandler>().amountLeft -= 1;
        potionAmountText.text = GetComponentInParent<PotionHandler>().amountLeft.ToString();
        GetComponentInParent<PotionHandler>().isUsingPotion = false;
        GetComponentInParent<Health>().UpdateHealthBar();
        this.gameObject.SetActive(false);
    }
}
