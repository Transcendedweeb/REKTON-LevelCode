using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] public float playerHealth = 100f;
    [SerializeField] GameObject healthFeedback;
    public Slider healthBar;

    void Start()
    {
        healthBar.maxValue = playerHealth;
        healthBar.value = playerHealth;
    }

    public void PlayerHealthHandler(float damageTaken){
        playerHealth -= damageTaken;
        UpdateHealthBar();
        healthFeedback.SetActive(true);
        Invoke("FeedbackOff", .9f);
        if (playerHealth <= 0){ 
            Debug.Log("death");
        } else { 
            Debug.Log($"Remaining health: {playerHealth}"); 
        }
    }

    void FeedbackOff()
    { 
        healthFeedback.SetActive(false);
    }

    public void UpdateHealthBar()
    {
        healthBar.value = playerHealth;
    }
}
