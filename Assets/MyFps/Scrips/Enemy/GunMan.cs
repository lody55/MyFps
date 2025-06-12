using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace MyFps
{
    //건맨
    public class GunMan : MonoBehaviour
    {
        #region Variables
        //참조
        public Animator animator;
        public Transform thePlayer;     //공격 타겟
        private RobotHealth gunManHealth;
        private NavMeshAgent agent;

        //애니메이션 파라미터
        private string enemyState = "EnemyState";
        private string fire = "Fire";
        //패트롤
        public Transform[] wayPoints;
        private int nowPointIndex = 0;
        //대기 타이머 
        [SerializeField]private float idleTime = 2f;

        private Vector3 originPosition;

        //디텍팅
        [SerializeField]private float detectingRange = 15f;
        private float distance;

        //공격 : 총 발사 : 멈춰서 총 발사 애니메이션 
        [SerializeField] private float attackRange = 5f;

        [SerializeField]private float attackTime = 2f;
        [SerializeField]private float attackCountdown = 0f;

        private float attackDamage = 5f;

        private Vector3 target; //플레이어의 위치

        private float idleCountdown = 0f;

        //로봇의 현재 상태
        private RobotState gunManState;

        //로봇의 이전 상태
        private RobotState beforeState;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
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
            //초기화
            originPosition = transform.position;

            ChangeState(RobotState.R_Idle);
        }

        private void Update()
        {
            if (PlayerController.safeZoneIn )
            {
                if(gunManState == RobotState.R_Attack || gunManState == RobotState.R_Chase)
                {
                    Debug.Log("제자리로 돌아가라");
                    //제자리에 돌아가라
                    BackHome();
                    return;
                }
            }
            else    //Enemey Zone에 있는 경우
            {
                //적 디텍팅
                target = new Vector3(thePlayer.position.x, this.transform.position.y, thePlayer.position.z);
                distance = Vector3.Distance(transform.position, target);

                if (distance <= attackRange) //5
                {

                    ChangeState(RobotState.R_Attack);
                }

                else if (distance <= detectingRange) //15
                {
                    //추격 상태로 변경
                    ChangeState(RobotState.R_Chase);
                }
            }
            //죽음체크
            if (gunManHealth.IsDeath) return;

           

            //상태 구현
            switch (gunManState)
            {
                case RobotState.R_Idle:
                    if(wayPoints.Length > 0)
                    {
                        idleCountdown += Time.deltaTime;
                        if(idleCountdown >= idleTime)
                        {
                            //패트롤  상태 변환
                            ChangeState(RobotState.R_Patrol);

                            //타이머 초기화
                            idleCountdown = 0f;
                        }
                    }
                    break;
                case RobotState.R_Walk:
                    //웨이포인트 도착 판정
                    if (agent.remainingDistance <= 0.2f)
                    {
                        ChangeState(RobotState.R_Idle);
                    }

                    break;
                case RobotState.R_Death:
                    break;
                case RobotState.R_Patrol:
                    //웨이포인트 도착 판정
                    if(agent.remainingDistance <= 0.2f)
                    {
                        ChangeState(RobotState.R_Idle);
                    }
                    break;
                case RobotState.R_Chase:
                    //타겟으로 이동(실시간으로 목표지점을 변경)
                    agent.SetDestination(target);
                    //플레이어가 디텍팅 거리에서 벗어나면
                    if(distance > detectingRange)   //15
                    {
                        
                        //제자리로 되돌아가기
                        BackHome();
                    }
                    break;
                case RobotState.R_Attack:
                    //공격 타이머
                    attackCountdown += Time.deltaTime;
                    if(attackCountdown >= attackTime)
                    {
                        //발사
                        animator.SetTrigger(fire);
                        //타이머 초기화
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

        //적 디텍팅 범위 그리기
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
            //현재 상태 체크
            if(gunManState == newState)
            {
                return;
            }

            //현재 상태를 이전 상태로 저장
            beforeState = gunManState; 

            //새로운 상태를 현재 상태로 저장
            gunManState= newState;

            //상태 변경에 따른 구현 내용
            if(gunManState ==RobotState.R_Patrol)
            {
                animator.SetInteger(enemyState, (int)RobotState.R_Walk);
                //다음 웨이포인트로 이동
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
                //타겟을 향한 이동 멈춤
                
                //조준 애니 적용
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
            // Unity 2018.3 이상에서 isOnNavMesh로 체크 가능
            if (agent != null && agent.enabled && agent.isOnNavMesh)
                agent.ResetPath();
        }

        //죽음시 호출되는 함수
        private void OnDie()
        {
            ChangeState(RobotState.R_Death);
            SafeResetPath();
            this.GetComponent<BoxCollider>().enabled = false;

            //추가 구현 내용
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
            Debug.Log($"플레이어에게 {attackDamage}를 준다");
            PlayerHealth playerHealth = thePlayer.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }

        //제자리로 돌아가기
        private void BackHome()
        {
            //패트롤 여부 체크
            if(wayPoints.Length > 1)
            {
                ChangeState(RobotState.R_Patrol);
                
            }
            else
            {
                ChangeState(RobotState.R_Walk); //지정한 위치로 이동
                
                agent.SetDestination(originPosition);
            }
            //조준애니 푼다
            animator.SetLayerWeight(1, 0);
        }
        #endregion
    }
}