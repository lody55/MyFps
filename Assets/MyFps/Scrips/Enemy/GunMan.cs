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

        //디텍팅
        [SerializeField]private float detectingRange = 15f;

        //공격 : 총 발사 : 멈춰서 총 발사 애니메이션 
        [SerializeField] private float attackRange = 5f;

        [SerializeField]private float attackTime = 2f;
        [SerializeField]private float attackCountdown = 0f;

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
            ChangeState(RobotState.R_Idle);
        }

        private void Update()
        {
            //죽음체크
            if (gunManHealth.IsDeath) return;

            //적 디텍팅
            target = new Vector3(thePlayer.position.x, this.transform.position.y, thePlayer.position.z);
            float distance = Vector3.Distance(transform.position, target);
            
            if(distance <= attackRange) //5
            {
                ChangeState(RobotState.R_Attack);
            }

            else if(distance <= detectingRange) //15
            {
                //추격 상태로 변경
                ChangeState(RobotState.R_Chase);
            }

            //상태 구현
            switch (gunManState)
            {
                case RobotState.R_Idle:
                    if(wayPoints.Length >= 0)
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

                agent.ResetPath();
            }
            
            else
            {
                animator.SetInteger(enemyState, (int)gunManState);
            }

            
        }

        //죽음시 호출되는 함수
        private void OnDie()
        {
            ChangeState(RobotState.R_Death);
            this.GetComponent<BoxCollider>().enabled = false;
            
            //추가 구현 내용

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