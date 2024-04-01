using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class StaminaCollision : MonoBehaviour
{
    [SerializeField] AudioClip speedUp;
    AudioSource audioSource;
    FirstPersonController fpc;
    float originalWalk;
    float originalRun;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player") 
        {
            audioSource.PlayOneShot(speedUp);
            BoostHandler(other);
            DisableThis();
            Invoke("ReturnToNormal", 14f);
            Invoke("DestroyThis", 16f);
        }
    }

    private void BoostHandler(Collision other)
    {
        fpc = other.gameObject.GetComponent<FirstPersonController>();

        originalWalk = fpc.m_WalkSpeed;
        originalRun = fpc.m_RunSpeed;

        fpc.m_WalkSpeed = 10f;
        fpc.m_RunSpeed = 20f;
    }
    private void ReturnToNormal()
    {
        fpc.m_WalkSpeed = originalWalk;
        fpc.m_RunSpeed = originalRun;
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
