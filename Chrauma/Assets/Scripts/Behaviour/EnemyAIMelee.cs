using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Pool;

//debug
using TMPro;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float startingHealth;
    public float currentHealth;
    public GameObject lifeDropPrefab;

    // * Patrol settings
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // * Attack settings
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // * Detection ranges
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
	EnemyAnimator enemyAnimator;
    GameObject _LifeDropTarget;
    

    //debug
    [SerializeField] TMP_Text hptext;
    [SerializeField] TMP_Text hptext2;
    public bool debugHP = false;
    [SerializeField] ParticleSystem bloodSplatter;
    [SerializeField] GameObject deathSmoke;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
		enemyAnimator = GetComponentInChildren<EnemyAnimator>();

        // *set current hp when spawn
        currentHealth = startingHealth;

    }


	private void Start()
	{
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _LifeDropTarget = GameObject.FindGameObjectWithTag("LifeDropTarget");
        if (debugHP) DebugHP(0);
	}

    private void Update()
    {
        
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange && !alreadyAttacked) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        // * assez proche ??
        if (distanceToWalkPoint.magnitude < 1)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            // * attack ici
            // Debug.Log("Melee attack!");
            enemyAnimator.startAttackAnimation();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (debugHP) Debug.Log(gameObject.name + " hp:" + currentHealth);
        bloodSplatter.Play();
        if (debugHP )DebugHP(damage);
        if (currentHealth <= 0) EnemyDies();
    }
    private IObjectPool<Enemy> enemyPool;


    public void SetPool(IObjectPool<Enemy> pool)
    {
        enemyPool = pool;
    }

    private void EnemyDies()
    {
        // * il va drop la LifeDrop
        for (int i = 0; i < startingHealth/10; i++)
        {
            var go = Instantiate(lifeDropPrefab, transform.position + new Vector3(0, Random.Range(0, 2)), Quaternion.identity);
            var goscript = go.GetComponent<FollowLifeDrop>();
            goscript.Target = _LifeDropTarget.transform;
            goscript.StartFollowing();
        }
        Instantiate(deathSmoke, transform.position, Quaternion.identity);
        if(enemyPool != null) enemyPool.Release(this);
    }

    private void OnDrawGizmosSelected_()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    private void DebugHP(int damage)
    {
        hptext.text = "" + currentHealth;
        hptext2.text = "" + damage;
    }
}
