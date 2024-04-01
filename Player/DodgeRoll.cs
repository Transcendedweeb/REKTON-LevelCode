using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class DodgeRoll : MonoBehaviour
{
    [SerializeField] AudioClip dodgeRollSFX;
    [SerializeField] int staminaUsage = 20;

    AudioSource audioSource;
    bool isReloading;
    bool ADSActive;
    bool rollActive = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.F) && !CheckFlags()){
            if(staminaUsage > GetComponent<Stamina>().GetCurrentStamina() || rollActive) return;

            DodgeRollHandler();
        }
    }

    private void DodgeRollHandler()
    {
        rollActive = true;
        GetComponent<Stamina>().UseStamina(15);
        audioSource.PlayOneShot(dodgeRollSFX);
        GetComponent<FirstPersonController>().enabled = false;
        GetComponent<Animator>().SetBool("Roll", true);
        Invoke("RollOut", .5f);
        Invoke("DeactivateActiveRoll", 1f);
    }

    void ForwardForce()
    {
        Vector3 userDirection = Vector3.forward;
        this.transform.Translate(userDirection * 10f);
    }

    void RollOut(){
        GetComponent<FirstPersonController>().enabled = true;
        GetComponent<Animator>().SetBool("Roll", false);
    }

    void DeactivateActiveRoll(){
        rollActive = false;
    }

    bool CheckFlags()
    {
        bool[] flagsArray = {
            GetComponentInChildren<Reload>().IsReloading(),
            GetComponentInChildren<Shoot>().IsADSActive(),
            GetComponentInChildren<PotionHandler>().isUsingPotion,
        };

        foreach(bool flag in flagsArray)
        {
            if(flag) return true;
        }
        return false;
    }
}
