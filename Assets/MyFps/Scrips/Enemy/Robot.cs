using Unity.Properties;
using UnityEngine;
namespace MyFps
{
    //로봇 상태
    public enum RobotState
    {
        R_Idle,
        R_Walk,
        R_Attack,
        R_Death
    }

    public class Robot : MonoBehaviour 
    {
        //
        #region Variables
        //참조
        public Animator animator;
        public Transform thePlayer;     //공격 타겟
        private RobotHealth robot;

        //로봇의 현재 상태
        private RobotState robotState;

        //로봇의 이전 상태
        private RobotState beforeState;

        //애니메이션 파라미터
        private string enemyState = "EnemyState";


        

        //이동 속도(걷기)
        [SerializeField]private float moveSpeed = 5f;

        //공격 범위
        [SerializeField]private float attackRange = 2f;

        //공격 시간
        [SerializeField] private float attackDelay = 2f;
        //공격 타이머
        [SerializeField] private float lastAttackTime;

        //공격력
        private float attackDamage = 5f; 

        #endregion

        #region Unity Event Method
        
        
        private void Awake()
        {
            robot = this.GetComponent<RobotHealth>();
            //참조
            animator = this.GetComponent<Animator>();

        }

        private void OnEnable()
        {
            robot.OnDie += OnDie;
            ChangeState(RobotState.R_Idle);
        }
        private void OnDisable()
        {
            robot.OnDie -= OnDie;

        }

        private void Update()
        {
            if (robot.IsDeath) return;
            if (thePlayer == null) return;
            EnemyAttack();
            
        }
        #endregion

        #region Custom Method
        //새로운 상태를 매개변수로 받아 새로운 상태로 셋팅
        public void ChangeState(RobotState newState)
        {
            //현재 상태 체크
            if(robotState == newState)
            {
                return;
            }

            //현재 상태를 이전 상태로 저장
            beforeState = robotState; 

            //새로운 상태를 현재 상태로 저장
            robotState = newState;

            //상태 변경에 따른 구현 내용
            animator.SetInteger(enemyState, (int)robotState);
        }

       
        private void EnemyAttack()
        {


            Vector3 target = new Vector3(thePlayer.position.x, this.transform.position.y, thePlayer.position.z);
            Vector3 dir = target - this.transform.position;

            float distance = Vector3.Distance(transform.position, target);



            //사거리 체크
            if (distance <= attackRange)
            {
                ChangeState(RobotState.R_Attack);
                if (Time.time - lastAttackTime >= attackDelay)
                {
                    ChangeState(RobotState.R_Attack);
                    lastAttackTime = 0f;
                }
            }
            


            switch (robotState)
            {
                case RobotState.R_Idle:
                    break;
                case RobotState.R_Walk: transform.Translate(dir.normalized * moveSpeed * Time.deltaTime,Space.World);
                    transform.LookAt(target);

                    break;
                case RobotState.R_Attack:
                    
                   

                    //공격 범위 체크
                    if (distance > attackRange)
                    {
                        ChangeState(RobotState.R_Walk);
                    }
                    break;
                case RobotState.R_Death:
                    break;
            }

        }

        private void OnAttackTimer()
        {
            lastAttackTime += Time.deltaTime;
            if(lastAttackTime >= attackDelay)
            {
                //타이머 내용
                
                //타이머 초기화
                lastAttackTime = 0f;
                
            }
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

        //죽음시 호출되는 함수
        private void OnDie()
        {
            ChangeState(RobotState.R_Death);
            this.GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject, 10f);
        }
        #endregion


    }
}