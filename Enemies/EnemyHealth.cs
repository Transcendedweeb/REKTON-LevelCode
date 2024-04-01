using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] float dropChance = 10f;
    [SerializeField] GameObject [] item;
    [SerializeField] float deathAnimationTime = 2f;


    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        health -= damage;
        if(health <= 0){
            if (GetComponent<EnemyMeleeAI>())
            {
                GetComponent<EnemyMeleeAI>().StopThisComponent();
            } else
            {
                GetComponent<EnemyShooterAi>().StopThisComponent();
            }
            GetComponent<Animator>().SetTrigger("Death");
            Invoke("DeathHandler", deathAnimationTime);
        }
    }

    void DeathHandler()
    {
        DropItemHandler();
        Destroy(this.gameObject); 
    }

    private void DropItemHandler()
    {
        int RNG = UnityEngine.Random.Range(0, 100);
        if (RNG <= dropChance){
            int RNGItem = UnityEngine.Random.Range(0, item.Length);
            GameObject dropItem = Instantiate(item[RNGItem], this.transform.position, transform.rotation);
        }
    }
}
