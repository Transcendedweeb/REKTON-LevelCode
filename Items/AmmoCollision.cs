using UnityEngine;

public class AmmoCollision : MonoBehaviour
{
    [SerializeField] AudioClip maxAmmo;
    AudioSource audioSource;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player") 
        {
            audioSource.PlayOneShot(maxAmmo);
            other.gameObject.GetComponent<Ammo>().MaxAmmo();
            other.gameObject.GetComponentInChildren<Reload>().UpdateGameText();
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
