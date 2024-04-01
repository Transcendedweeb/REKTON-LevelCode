using UnityEngine;

public class ParicleDamage : MonoBehaviour
{
    [SerializeField] GameObject impactVFX;
    [SerializeField] float explodeTime;
    public float dealDamage;

    private void Start() 
    {
        Invoke("Explode", explodeTime);
    }

    public void Explode()
    {
        GameObject impact = Instantiate(impactVFX, this.transform.position, impactVFX.transform.rotation);
        Destroy(impact, 1f);
        Destroy(this.gameObject);
    }
}
