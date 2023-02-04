using UnityEngine;
using UnityEngine.AI;
using Wrecking_Clone.Core;
namespace Wrecking_Clone.GamePlay
{
    public class AiEnemy : MonoBehaviour
    {
        #region Properties and Fields
        public NavMeshAgent agent;
        private Transform player;

        public LayerMask whatIsGround;
        public LayerMask whatIsPlayer;

        private Rigidbody m_rb;

        private const string Water = "Water";
        //public Car AiCar;

        //Patroling
        public Vector3 walkPoint;
        public bool walkPointSet;
        public float walkPointRange;

        //Attacking
        public float timeBetweenAttacks;
        public bool readyToAttack = true;

        //States
        public float sightRange, attackRange;
        public bool playerInSightRange;
        public bool playerInAttackRange;
        public bool isDeath = false;

        //Audio
        public AudioSource m_HitAudioSource;
        //Animation
        public Animator EnemyAI_animator;
        #endregion
        public static AiEnemy instance;
        private void Awake()
        {
            instance = this;
            player = GameObject.FindGameObjectWithTag("Player").transform;
            agent = GetComponent<NavMeshAgent>();
            EnemyAI_animator.enabled = false;
        }
        private void Start()
        {
            // AiCar = GetComponent<Car>();
            m_rb = GetComponent<Rigidbody>();
        }
        private void Update()
        {
            if (GameManager.isGamePlay)
            {
                //Check if the any player is in given range which is enemy's range
                //[1] is the 2. player colldider including self

                //first is self collider so we need to look 2. or much more
                playerInSightRange = (Physics.OverlapSphere(transform.position, sightRange, whatIsPlayer).Length > 1) ? true : false;
                playerInAttackRange = (Physics.OverlapSphere(transform.position, attackRange, whatIsPlayer).Length > 1) ? true : false;

                if (!playerInSightRange && !playerInAttackRange) Invoke(nameof(Patroling), .1f);//wait 0.1 sec to change state
                if (playerInSightRange && !playerInAttackRange)
                {
                    Debug.Log("AI is Cahasing");
                    Invoke(nameof(ChasePlayer), .1f);
                }

                if (playerInAttackRange && playerInSightRange)
                {
                    Debug.Log("AI is attacking");
                    Invoke(nameof(AttackOtherPlayer), 1f);
                }
            }
        }


        private void Patroling()
        {
            if (!walkPointSet) SearchWalkPoint();

            if (walkPointSet)
                agent?.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            //Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f)
                walkPointSet = false;
        }
        private void SearchWalkPoint()
        {
            //Calculate random point in range
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            CheckIfGrounded();
        }
        public bool CheckIfGrounded()
        {
            if (Physics.Raycast(walkPoint, -transform.up, 3f, whatIsGround))
                walkPointSet = true;
            return walkPointSet;
        }
        private void ChasePlayer()
        {
            try
            {
                //[1] is the 2. player colldider including self
                player = Physics.OverlapSphere(transform.position, sightRange, whatIsPlayer)[1]?.gameObject.GetComponent<Transform>();
                if (walkPointSet)
                    agent?.SetDestination(player.position);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e);
            }
        }
        private void AttackOtherPlayer()
        {
            //agent?.SetDestination(transform.position);

            if (readyToAttack)
            {
                //Attack
                EnemyAI_animator.enabled = true;
                EnemyAI_animator.SetTrigger("AttackTrigger");
                EnemyAI_animator.enabled = false;
                ///End of attack
                readyToAttack = false;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
        private void ResetAttack()
        {
            readyToAttack = true;
            EnemyAI_animator.enabled = false;
        }

        private void ThrownAway()
        {
            m_HitAudioSource.Play();
            m_rb.AddForce(transform.forward * 4f, ForceMode.Impulse);//Todo make tih much matter
            m_rb.AddForce(transform.up * 2f, ForceMode.Impulse);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }
    }
}