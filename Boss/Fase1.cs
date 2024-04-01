using UnityEngine;

public class Fase1 : MonoBehaviour
{   
    [SerializeField] float switchFaseTime;
    [SerializeField] AudioClip[] voicePack;
    [SerializeField] GameObject displayHealthbar;
    GameObject shield;
    GameObject flamethrowers;
    GameObject minionSpawner;
    GameObject ionCannon;
    GameObject meteors;
    GameObject[] dodgeMoves;
    AudioSource Audio;
    int arrayRawLength;
    int shieldAnimationTime = 2;
    int lastSpell = 444;

    private void Start() 
    {
        Audio = GetComponent<AudioSource>();
    }
    void OnEnable()
    {
        SetupGameObjects();
        shield.GetComponent<Animator>().SetBool("IsShieldUp", true);
        displayHealthbar.SetActive(true);
        Invoke("DodgeFaseHandler", shieldAnimationTime+2);
    }

    void DodgeFaseHandler()
    {   
        while (true)
        {
            int moveSelector = Random.Range(0, arrayRawLength);
            if (moveSelector != lastSpell)
            {
                dodgeMoves[moveSelector].SetActive(true);
                Invoke("DisableDodgeMoves", switchFaseTime);
                lastSpell = moveSelector;
                break;
            }
        }
        
    }

    void ShootFaseHandler()
    {
        minionSpawner.SetActive(true);
        Invoke("DisableShootFase", switchFaseTime);
    }

    void DisableDodgeMoves()
    {
        foreach(GameObject move in dodgeMoves)
        {
            move.SetActive(false);
        }
        shield.GetComponent<Animator>().SetBool("IsShieldUp", false);
        PlayVoice();
        Invoke("ShootFaseHandler", shieldAnimationTime);
    }

    void DisableShootFase()
    {
        minionSpawner.SetActive(false);
        shield.GetComponent<Animator>().SetBool("IsShieldUp", true);
        PlayVoice();
        Invoke("DodgeFaseHandler", shieldAnimationTime);
    }

    void SetupGameObjects()
    {
        shield = this.transform.Find("PlasmaBarrier").gameObject;
        flamethrowers = this.transform.Find("Flamethrowers").gameObject;
        minionSpawner = this.transform.Find("MinionSpawner").gameObject;
        ionCannon = this.transform.Find("IonBeams").gameObject;
        meteors = this.transform.Find("Meteor").gameObject;
        SetupDodgeArray();
        GetComponentInParent<Fase1Health>().healthActive = true;
        GetComponentInParent<AudioSource>().enabled = true;
    }

    void SetupDodgeArray()
    {
        dodgeMoves = new GameObject[] {flamethrowers, ionCannon, meteors};
        arrayRawLength = dodgeMoves.Length;
    }

    void PlayVoice()
    {
        int num = Random.Range(0, voicePack.Length);
        Audio.PlayOneShot(voicePack[num]);
    }
}