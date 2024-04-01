using UnityEngine;

public class HealthCollision : MonoBehaviour
{
    [SerializeField] AudioClip juggernog;
    AudioSource audioSource;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player") 
        {
            audioSource.PlayOneShot(juggernog);
            other.gameObject.GetComponent<Health>().playerHealth = 100f;
            other.gameObject.GetComponent<Health>().UpdateHealthBar();
            DisableThis();
            Invoke("DestroyThis", 5f);
        }
    }

    private void DisableThis()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
    }
    private void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
