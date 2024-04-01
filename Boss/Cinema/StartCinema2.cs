using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class StartCinema2 : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject cinemaSfx;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject fase;
    [SerializeField] float delay1;
    [SerializeField] float delay2;
    [SerializeField] float delay3;
    [SerializeField] float delay4;
    [SerializeField] float delay5;
    [SerializeField] float delay6;
    [SerializeField] GameObject mesh;
    [SerializeField] Material newMesh;
    [SerializeField] AudioClip laugh;
    [SerializeField] AudioClip voice1;
    [SerializeField] AudioClip voice2;
    [SerializeField] AudioClip voice3;
    AudioSource Audio;

    void OnEnable() 
    {
        Audio = GetComponent<AudioSource>();
        player.transform.LookAt(boss.transform);
        FlagsHandler(player, false);
        Audio.PlayOneShot(laugh);
        Invoke("StartCinema", delay1);
    }

    void StartCinema()
    {
        cinemaSfx.SetActive(true);
        Invoke("StartVoice", delay2);
    }

    void StartVoice()
    {
        Audio.PlayOneShot(voice1);
        Invoke("PlayVoice", delay3);
    }

    void PlayVoice()
    {
        // Play second sfx in delay
        boss.GetComponent<Animator>().SetTrigger("Stand");
        Audio.PlayOneShot(voice2);
        Invoke("ChangeMesh", delay4);
    }

    void ChangeMesh()
    {
        mesh.GetComponent<Renderer>().material = newMesh;
        boss.GetComponent<Fase1Health>().ChangeToFase2();
        Invoke("LastVoice", delay5);
    }

    void LastVoice()
    {
        boss.GetComponent<Animator>().SetTrigger("Roar");
        Audio.PlayOneShot(voice3);
        Invoke("StartFight", delay6);
    }

    void StartFight()
    {
        UndoFlags();
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
        FlagsHandler(player, true);
        fase.SetActive(true);
    }
}
