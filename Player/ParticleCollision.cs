using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    [SerializeField] float dmgCooldown = 1f;
    bool dmgCooldownSet = false;
    Health health;

    private void Start() 
    {
        health = GetComponent<Health>();
    }
    private void OnParticleCollision(GameObject other) {
        if (dmgCooldownSet) return;
        dmgCooldownSet = true;
        Debug.Log(other.gameObject.tag);

        switch(other.gameObject.tag)
        {
            case "Meteor":
                health.PlayerHealthHandler(25f);
                break;
            case "Flamethrower":
                health.PlayerHealthHandler(20f);
                break;
            case "IonCannon":
                health.PlayerHealthHandler(100f);
                break;
            case "Rocket":
                health.PlayerHealthHandler(30f);
                break;
            case "Projectile":
                health.PlayerHealthHandler(10f);
                break;
            default:
                dmgCooldownSet = false;
                return;
        }
        Invoke("ResetCooldown", dmgCooldown);
    }

    private void OnTriggerStay(Collider other) {
        if (dmgCooldownSet) return;
        dmgCooldownSet = true;
        Debug.Log(other.gameObject.tag);
        switch(other.gameObject.tag)
        {
            case "MoltenGround":
                health.PlayerHealthHandler(5f);
                break;
            case "Boss":
                health.PlayerHealthHandler(20f);
                break;
            case "Vortex":
                health.PlayerHealthHandler(15f);
                break;
            default:
                dmgCooldownSet = false;
                return;
        }

        Invoke("ResetCooldown", dmgCooldown);
    }

    void ResetCooldown()
    {
        dmgCooldownSet = false;
    }
}
