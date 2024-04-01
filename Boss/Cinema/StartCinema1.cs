using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class StartCinema1 : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] GameObject  callFase;
    [SerializeField] GameObject bridge;
    [SerializeField] float animationTime;
    [SerializeField] float timeBeforeVoice;
    [SerializeField] AudioClip voice;
    [SerializeField] AudioClip sfx;
    AudioSource Audio;
    GameObject player;

    void Start() 
    {
        Audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") 
        {
            player = other.gameObject;
            GetComponent<BoxCollider>().enabled = false;
            FlagsHandler(player, false);
            RunCinema(player);
        }
    }

    public void RunCinema(GameObject player)
    {
        Audio.PlayOneShot(sfx);
        Invoke("VoiceSound", timeBeforeVoice);
        player.transform.LookAt(boss.transform);
        player.GetComponent<Animator>().SetTrigger("Cinema");
        Invoke("UndoFlags", animationTime);
    }

    private void FlagsHandler(GameObject player , bool setStatus)
    {
        player.GetComponent<FirstPersonController>().enabled = setStatus;
        player.GetComponent<DodgeRoll>().enabled = setStatus;
        player.GetComponent<Health>().enabled = setStatus;
        player.GetComponent<Stamina>().enabled = setStatus;
        player.GetComponent<Ammo>().enabled = setStatus;

        player.GetComponentInChildren<PotionHandler>().enabled = setStatus;
        player.GetComponentInChildren<GunHandler>().enabled = setStatus;
        player.GetComponentInChildren<Reload>().enabled = setStatus;
        player.GetComponentInChildren<Shoot>().enabled = setStatus;
    }

    private void UndoFlags()
    {
        bridge.SetActive(false);
        FlagsHandler(player, true);
        callFase.GetComponent<Fase1>().enabled = true;
    }

    void VoiceSound()
    {
        boss.GetComponent<Animator>().SetTrigger("Idle");
        Audio.PlayOneShot(voice);
    }
}
