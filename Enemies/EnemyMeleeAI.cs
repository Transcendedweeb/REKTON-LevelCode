using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeAI : MonoBehaviour
{
    [SerializeField] float chaseRange = 8f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float amountOfDamageDealt;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked;
    bool dealingDamage;
    GameObject targetGameobject;
    Transform target;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        dealingDamage = false;
    }

    void Awake() 
    {
        targetGameobject = GameObject.FindWithTag("Player");
        target = targetGameobject.transform;
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position); // calculate the distance between target and script holder

        if (isProvoked){
            EngageTarget();
        } else if (distanceToTarget <= chaseRange){ 
            isProvoked = true;
        } else{
            isProvoked = false;
            GetComponent<Animator>().SetTrigger("Idle");
        }
    }

    public void OnDamageTaken(){
        isProvoked = true;
    }

    void EngageTarget(){
        FaceTarget();

        if(distanceToTarget >= navMeshAgent.stoppingDistance){
            GetComponent<Animator>().SetTrigger("Move");
            navMeshAgent.SetDestination(target.position);
        } else{
            if(!dealingDamage)
            {
                DealDamage();
                Invoke("ResetDealingDamage", 1f);
            }
        }
    }

    private void DealDamage()
    {
        dealingDamage = true;
        target.gameObject.GetComponent<Health>().PlayerHealthHandler(amountOfDamageDealt);
        GetComponent<Animator>().SetTrigger("Attack");
    }

    void ResetDealingDamage()
    {
        dealingDamage = false;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    void FaceTarget(){
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    public void StopThisComponent()
    {
        navMeshAgent.SetDestination(this.transform.position);
        this.GetComponent<EnemyMeleeAI>().enabled = false;
    }
}
