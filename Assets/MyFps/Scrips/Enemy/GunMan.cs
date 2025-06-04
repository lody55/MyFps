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

        //������
        [SerializeField]private float detectingRange = 15f;

        //���� : �� �߻� : ���缭 �� �߻� �ִϸ��̼� 
        [SerializeField] private float attackRange = 5f;

        [SerializeField]private float attackTime = 2f;
        [SerializeField]private float attackCountdown = 0f;

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
            ChangeState(RobotState.R_Idle);
        }

        private void Update()
        {
            //����üũ
            if (gunManHealth.IsDeath) return;

            //�� ������
            target = new Vector3(thePlayer.position.x, this.transform.position.y, thePlayer.position.z);
            float distance = Vector3.Distance(transform.position, target);
            
            if(distance <= attackRange) //5
            {
                ChangeState(RobotState.R_Attack);
            }

            else if(distance <= detectingRange) //15
            {
                //�߰� ���·� ����
                ChangeState(RobotState.R_Chase);
            }

            //���� ����
            switch (gunManState)
            {
                case RobotState.R_Idle:
                    if(wayPoints.Length >= 0)
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

                agent.ResetPath();
            }
            
            else
            {
                animator.SetInteger(enemyState, (int)gunManState);
            }

            
        }

        //������ ȣ��Ǵ� �Լ�
        private void OnDie()
        {
            ChangeState(RobotState.R_Death);
            this.GetComponent<BoxCollider>().enabled = false;
            
            //�߰� ���� ����

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
        #endregion
    }
}