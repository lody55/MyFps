using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace MyFps
{
    //�Ǹ�
    public class GunMan : MonoBehaviour
    {
        #region Variables
        //����
        public Animator animator;
        public Transform thePlayer;     //���� Ÿ��
        private RobotHealth gunManHealth;
        private NavMeshAgent agent;

        //�ִϸ��̼� �Ķ����
        private string enemyState = "EnemyState";
        private string fire = "Fire";
        //��Ʈ��
        public Transform[] wayPoints;
        private int nowPointIndex = 0;
        //��� Ÿ�̸� 
        [SerializeField]private float idleTime = 2f;

        private Vector3 originPosition;

        //������
        [SerializeField]private float detectingRange = 15f;
        private float distance;

        //���� : �� �߻� : ���缭 �� �߻� �ִϸ��̼� 
        [SerializeField] private float attackRange = 5f;

        [SerializeField]private float attackTime = 2f;
        [SerializeField]private float attackCountdown = 0f;

        private float attackDamage = 5f;

        private Vector3 target; //�÷��̾��� ��ġ

        private float idleCountdown = 0f;

        //�κ��� ���� ����
        private RobotState gunManState;

        //�κ��� ���� ����
        private RobotState beforeState;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //����
            gunManHealth = this.GetComponent<RobotHealth>();
            agent = this.GetComponent<NavMeshAgent>();
            animator = this.GetComponent<Animator>();

        }
        private void OnEnable()
        {
            gunManHealth.OnDie += OnDie;

        }

        private void Start()
        {
            //�ʱ�ȭ
            originPosition = transform.position;

            ChangeState(RobotState.R_Idle);
        }

        private void Update()
        {
            if (PlayerController.safeZoneIn )
            {
                if(gunManState == RobotState.R_Attack || gunManState == RobotState.R_Chase)
                {
                    Debug.Log("���ڸ��� ���ư���");
                    //���ڸ��� ���ư���
                    BackHome();
                    return;
                }
            }
            else    //Enemey Zone�� �ִ� ���
            {
                //�� ������
                target = new Vector3(thePlayer.position.x, this.transform.position.y, thePlayer.position.z);
                distance = Vector3.Distance(transform.position, target);

                if (distance <= attackRange) //5
                {

                    ChangeState(RobotState.R_Attack);
                }

                else if (distance <= detectingRange) //15
                {
                    //�߰� ���·� ����
                    ChangeState(RobotState.R_Chase);
                }
            }
            //����üũ
            if (gunManHealth.IsDeath) return;

           

            //���� ����
            switch (gunManState)
            {
                case RobotState.R_Idle:
                    if(wayPoints.Length > 0)
                    {
                        idleCountdown += Time.deltaTime;
                        if(idleCountdown >= idleTime)
                        {
                            //��Ʈ��  ���� ��ȯ
                            ChangeState(RobotState.R_Patrol);

                            //Ÿ�̸� �ʱ�ȭ
                            idleCountdown = 0f;
                        }
                    }
                    break;
                case RobotState.R_Walk:
                    //��������Ʈ ���� ����
                    if (agent.remainingDistance <= 0.2f)
                    {
                        ChangeState(RobotState.R_Idle);
                    }

                    break;
                case RobotState.R_Death:
                    break;
                case RobotState.R_Patrol:
                    //��������Ʈ ���� ����
                    if(agent.remainingDistance <= 0.2f)
                    {
                        ChangeState(RobotState.R_Idle);
                    }
                    break;
                case RobotState.R_Chase:
                    //Ÿ������ �̵�(�ǽð����� ��ǥ������ ����)
                    agent.SetDestination(target);
                    //�÷��̾ ������ �Ÿ����� �����
                    if(distance > detectingRange)   //15
                    {
                        
                        //���ڸ��� �ǵ��ư���
                        BackHome();
                    }
                    break;
                case RobotState.R_Attack:
                    //���� Ÿ�̸�
                    attackCountdown += Time.deltaTime;
                    if(attackCountdown >= attackTime)
                    {
                        //�߻�
                        animator.SetTrigger(fire);
                        //Ÿ�̸� �ʱ�ȭ
                        attackCountdown = 0f;
                    }
                    transform.LookAt(thePlayer);
                    break;


            }
        }


        private void OnDisable()
        {
            gunManHealth.OnDie -= OnDie;

        }

        //�� ������ ���� �׸���
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectingRange);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
        #endregion

        #region Custom Method
        public void ChangeState(RobotState newState)
        {
            //���� ���� üũ
            if(gunManState == newState)
            {
                return;
            }

            //���� ���¸� ���� ���·� ����
            beforeState = gunManState; 

            //���ο� ���¸� ���� ���·� ����
            gunManState= newState;

            //���� ���濡 ���� ���� ����
            if(gunManState ==RobotState.R_Patrol)
            {
                animator.SetInteger(enemyState, (int)RobotState.R_Walk);
                //���� ��������Ʈ�� �̵�
                GoNextWayPoint();
            }
            else if(gunManState == RobotState.R_Idle)
            {
                animator.SetInteger(enemyState, (int)gunManState);
                idleTime = Random.Range(1f, 3f);

                SafeResetPath();
            }
            else if (gunManState == RobotState.R_Chase)
            {
                animator.SetInteger(enemyState, (int)RobotState.R_Chase);

                animator.SetLayerWeight(1, 1);
                
            }
            else if(gunManState == RobotState.R_Attack)
            {
                animator.SetInteger(enemyState, (int)RobotState.R_Idle);
                //Ÿ���� ���� �̵� ����
                
                //���� �ִ� ����
                animator.SetLayerWeight(1, 1);
                animator.SetTrigger(fire);

                SafeResetPath();
            }
            
            else
            {
                animator.SetInteger(enemyState, (int)gunManState);
            }

            
        }
        private void SafeResetPath()
        {
            // Unity 2018.3 �̻󿡼� isOnNavMesh�� üũ ����
            if (agent != null && agent.enabled && agent.isOnNavMesh)
                agent.ResetPath();
        }

        //������ ȣ��Ǵ� �Լ�
        private void OnDie()
        {
            ChangeState(RobotState.R_Death);
            SafeResetPath();
            this.GetComponent<BoxCollider>().enabled = false;

            //�߰� ���� ����
            agent.enabled = false;


            Destroy(gameObject, 5f);
        }
        private void GoNextWayPoint()
        {
            nowPointIndex++;
            if(nowPointIndex >= wayPoints.Length)
            {
                nowPointIndex = 0;
                
            }
            agent.SetDestination(wayPoints[nowPointIndex].position);


        }
        public void Attack()
        {
            Debug.Log($"�÷��̾�� {attackDamage}�� �ش�");
            PlayerHealth playerHealth = thePlayer.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }

        //���ڸ��� ���ư���
        private void BackHome()
        {
            //��Ʈ�� ���� üũ
            if(wayPoints.Length > 1)
            {
                ChangeState(RobotState.R_Patrol);
                
            }
            else
            {
                ChangeState(RobotState.R_Walk); //������ ��ġ�� �̵�
                
                agent.SetDestination(originPosition);
            }
            //���ؾִ� Ǭ��
            animator.SetLayerWeight(1, 0);
        }
        #endregion
    }
}