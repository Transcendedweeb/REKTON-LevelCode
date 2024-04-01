using UnityEngine;
using UnityEngine.AI;

public class Fase2 : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject bossMesh;
    [SerializeField] GameObject rocketsIndicator;
    [SerializeField] GameObject jumpIndicator;
    [SerializeField] GameObject moltenPillars;
    [SerializeField] float jumpDelay;
    [SerializeField] float rocketsFallTime;
    [SerializeField] AudioClip[] voiceArray;
    [SerializeField] AudioClip laugh;
    int arrayLength;
    GameObject jumper;
    GameObject vortex;
    GameObject rockets;
    GameObject force;
    AudioSource Audio;
    NavMeshAgent navMeshAgent;
    bool punching = false;
    void Awake()
    {
        SetupGameObjects();
        arrayLength = voiceArray.Length;
        Audio = GetComponent<AudioSource>();
        navMeshAgent = boss.GetComponent<NavMeshAgent>();
        JumpAnimation();
    }

    private void Update() 
    {
        if(navMeshAgent.enabled && !punching)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
    }

    // Jump Move
    void JumpAnimation()
    {
        Audio.PlayOneShot(laugh);
        jumper.SetActive(true);
        boss.GetComponent<Animator>().SetBool("Jump", true);
        Invoke("JumpMove1", 1.5f);
    }

    void JumpMove1()
    {
        boss.GetComponent<BoxCollider>().enabled = false;
        bossMesh.GetComponent<SkinnedMeshRenderer>().enabled = false;
        Invoke("JumpMove2", jumpDelay);
    }

    void JumpMove2()
    {
        PlayCcClip();
        Vector3 newPos = player.transform.position;
        newPos.y = newPos.y-1f;
        boss.GetComponent<Animator>().SetBool("Jump", false);
        boss.transform.position = newPos;
        bossMesh.GetComponent<SkinnedMeshRenderer>().enabled = true;
        Instantiate(jumpIndicator, newPos, transform.rotation);
        CheckNextMove("PlayVortex", 4f);
        Invoke("EnableBox", .5f);
    }

    void EnableBox()
    {
        boss.GetComponent<BoxCollider>().enabled = true;
    }

    // ------------------------------------------------------
    // Vortex move
    private void PlayVortex()
    {
        boss.GetComponent<Animator>().SetTrigger("Tpose");
        navMeshAgent.enabled = true;
        vortex.SetActive(true);
        PlayCcClip();
        Invoke("StopVortex", 10f);
    }

    private void StopVortex()
    {
        FacePlayer();
        boss.GetComponent<Animator>().SetTrigger("Idle");
        navMeshAgent.enabled = false;
        CheckNextMove("PlayRockets", 2f);
    }

    // ------------------------------------------------------
    // Rockets
    void PlayRockets()
    {
        PlayCcClip();
        FacePlayer();
        boss.GetComponent<Animator>().SetTrigger("Rockets");
        rockets.SetActive(true);
        Invoke("ShootRockets", 6f);
        Invoke("Punchuuh", 2f);
    }

    void ShootRockets()
    {
        Vector3 newPos = player.transform.position;
        newPos.y = newPos.y-.8f;
        Instantiate(rocketsIndicator, newPos, transform.rotation);
    }
    // ------------------------------------------------------
    // PunchMove
    void Punchuuh()
    {
        PlayCcClip();
        boss.GetComponent<Animator>().SetTrigger("Punch");
        force.SetActive(true);
        Invoke("DashPunch", 1f);
    }

    void DashPunch()
    {
        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(player.transform.position);
        Invoke("EndPunching", 3f);
    }

    void EndPunching()
    {
        navMeshAgent.enabled = false;
        punching = false;
        boss.GetComponent<Animator>().SetTrigger("Idle");
        jumpDelay = 10f;
        Invoke("Pillars", 5f);
        CheckNextMove("JumpAnimation", 2f);
    }

    // ------------------------------------------------------
    // Pillars
    void Pillars()
    {
        moltenPillars.SetActive(true);
        Invoke("DeactivatePillars", 25f);
    }
    void DeactivatePillars()
    {
        moltenPillars.SetActive(false);
    }
    // ------------------------------------------------------
    // Check next move
    private void CheckNextMove(string moveName, float cd)
    {
        DisableMoves();
        Invoke(moveName, cd);
    }
    // ------------------------------------------------------

    private void SetupGameObjects()
    {
        jumper = this.transform.Find("PulseGrenade").gameObject;
        vortex = this.transform.Find("FusionCore").gameObject;
        rockets = this.transform.Find("Rockets").gameObject;
        force = this.transform.Find("Force").gameObject;
    }

    void PlayCcClip()
    {
        int rng = Random.Range(0, arrayLength);
        Audio.PlayOneShot(voiceArray[rng]);
    }

    void FacePlayer()
    {
        boss.transform.LookAt(player.transform);
    }

    void DisableMoves()
    {
        jumper.SetActive(false);
        vortex.SetActive(false);
        rockets.SetActive(false);
        force.SetActive(false);
    }
}
