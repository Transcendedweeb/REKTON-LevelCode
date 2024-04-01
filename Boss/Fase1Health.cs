using UnityEngine;using UnityEngine.UI;

public class Fase1Health : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] float fase2Health;
    [SerializeField] float deathAnimationTime = 2f;
    [SerializeField] int currentFase = 1;
    [SerializeField] GameObject cinema;
    [SerializeField] GameObject player;
    [SerializeField] GameObject pillars;
    [SerializeField] GameObject canvasHealth;
    [SerializeField] GameObject end;
    [SerializeField] AudioClip death;
    [SerializeField] Text bossName;
    public bool healthActive = false;
    public Slider healthBar;
    float maxHealth;

    private void Awake() {
        maxHealth = health;
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public void TakeDamage(float damage)
    {
        if(!healthActive) return;

        health -= damage;
        UpdateHealthBar();
        if(health <= 0 && currentFase == 1){
            healthActive = false;
            currentFase = 2;
            transform.GetChild(2).gameObject.GetComponent<AudioSource>().enabled = false;
            // transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
            Destroy(transform.GetChild(2).gameObject);
            GetComponent<Animator>().SetTrigger("Kneel");
            Invoke("DeathHandler1", deathAnimationTime);
        } else if (health <= 0 && currentFase == 2)
        {
            healthActive = false;
            GetComponent<BoxCollider>().enabled = false;
            Destroy(transform.GetChild(5).gameObject);
            pillars.SetActive(false);
            canvasHealth.SetActive(false);
            end.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(death);
            GetComponent<Animator>().SetTrigger("Death");
        }
    }

    // Update is called once per frame
    void DeathHandler1()
    {
        health = fase2Health;
        cinema.GetComponent<StartCinema2>().enabled = true;
    }

    public void UpdateHealthBar()
    {
        healthBar.value = health;
    }

    public void ChangeToFase2()
    {
        healthActive = true;
        maxHealth = fase2Health;
        healthBar.maxValue = fase2Health;
        health = fase2Health;
        healthBar.value = health;
        bossName.text = "Extermination Protocol REKTon";
    }
}
