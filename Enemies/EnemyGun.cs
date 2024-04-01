using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class EnemyGun : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileForce = 33f;
    [SerializeField] AudioClip gunSound;

    private void Start() {
        
    }
        
    public void Shoot()
    {
        AudioSource.PlayClipAtPoint(gunSound, transform.position);
        Rigidbody rb = Instantiate(projectile, transform.position, transform.rotation).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * -projectileForce, ForceMode.Impulse);
    }
}
