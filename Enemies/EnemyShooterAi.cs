using UnityEngine;
using UnityEngine.AI;

public class EnemyShooterAi : MonoBehaviour
{
    [SerializeField] float chaseRange = 10f;
    [SerializeField] float engageRange = 10f;
    [SerializeField] float attackRange = 10f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float attackSpeed = 0.2f;
    [SerializeField] GameObject gun1;
    [SerializeField] GameObject gun2;
    [SerializeField] float amountOfDamageDealt;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    bool isInAttackRange = false;
    bool hasAttacked = false;
    bool isEngaging = false;
    bool dealingDamage = false;
    Transform target;
    GameObject targetGameobject;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Awake() 
    {
        targetGameobject = GameObject.FindWithTag("Player");
        target = targetGameobject.transform;
    }

    private void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position); // calculate the distance between target and script holder
        
        if(distanceToTarget <= engageRange){
            isEngaging = true;
        }
        else if(distanceToTarget <= attackRange){
            isEngaging = false;
            isInAttackRange = true;
            isProvoked = true;
        } else if (distanceToTarget <= chaseRange){ 
            isEngaging = false;
            isInAttackRange = false;
            isProvoked = true;
        } else {
            isEngaging = false;
            isInAttackRange = false;
        }
        
        if (isProvoked){
            if (isEngaging) EngagePlayer();
            else if(isInAttackRange) {GetComponent<Animator>().SetTrigger("Shooting"); AttackPlayer();}
            else {GetComponent<Animator>().SetTrigger("Running");; EngageTarget();}
        } else{
            isInAttackRange = false;
            isProvoked = false;
        }
    }

    private void EngagePlayer()
    {
        FaceTarget();

        if(distanceToTarget >= navMeshAgent.stoppingDistance){
            GetComponent<Animator>().SetTrigger("Running");
            navMeshAgent.SetDestination(target.position);
        } else{
            if(!dealingDamage)
            {
                GetComponent<Animator>().SetTrigger("Melee");
                Invoke("ResetDealingDamage", 1.2f);
            }
        }
    }

    private void DealDamage()
    {
        dealingDamage = true;
        target.gameObject.GetComponent<Health>().PlayerHealthHandler(amountOfDamageDealt);
    }

    void ResetDealingDamage()
    {
        dealingDamage = false;
    }

    private void AttackPlayer()
    {
        FaceTarget();
        navMeshAgent.SetDestination(transform.position);

        if(!hasAttacked)
        {
            AttackCommand();
            hasAttacked = true;
            Invoke(nameof(ResetAttack), attackSpeed);
        }
    }

    private void AttackCommand()
    {
        gun1.GetComponent<EnemyGun>().Shoot();
        gun2.GetComponent<EnemyGun>().Shoot();
    }

    private void ResetAttack(){
        hasAttacked = false;
    }

    private void EngageTarget()
    {
        FaceTarget();
        navMeshAgent.SetDestination(target.position);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    public void OnDamageTaken(){
        isProvoked = true;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void StopThisComponent()
    {
        navMeshAgent.SetDestination(this.transform.position);
        this.GetComponent<EnemyShooterAi>().enabled = false;
    }
}
