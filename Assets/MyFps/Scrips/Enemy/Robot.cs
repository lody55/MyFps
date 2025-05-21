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
        #region Variables
        //참조
        public Animator animator;
        public Transform thePlayer;     //공격 타겟
        
        //로봇의 현재 상태
        private RobotState robotState;

        //로봇의 이전 상태
        private RobotState beforeState;

        //애니메이션 파라미터
        private string enemyState = "EnemyState";

        //체력
        private float currentHealth;
        [SerializeField]private float maxHealth = 20f;
        private bool isDeath = false;


        //이동 속도(걷기)
        [SerializeField]private float moveSpeed = 5f;

        //공격 범위
        [SerializeField]private float attackRange = 2f;

        //공격 시간
        [SerializeField] private float attackDelay = 2f;
        //마지막 공격시간
        [SerializeField] private float lastAttackTime;

        #endregion

        #region Unity Event Method
        private void Start()
        {
            //참조
            animator = this.GetComponent<Animator>();
            
        }
        private void OnEnable()
        {
            //초기화
            ChangeState(RobotState.R_Idle);
        }

        private void Update()
        {
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

        //데미지 입기
        public void TakeDamage(float damage)
        {
            currentHealth -= damage;

            //데미지 연출 - Sfx , Vfx...등등

            if (currentHealth <= 0f && isDeath == false)
            {
                Death();
            }
        }
        private void Death()
        {
            isDeath = true;

            ChangeState(RobotState.R_Death);
            Destroy(gameObject, 3f);

            //보상처리...등
            
        }
        private void EnemyAttack()
        {


            Vector3 target = new Vector3(thePlayer.position.x, this.transform.position.y, thePlayer.position.z);
            Vector3 dir = target - this.transform.position;

            float distance = Vector3.Distance(transform.position, target);

            if (distance > attackRange)
            {
                
                    ChangeState(RobotState.R_Walk);
                
            }

            //사거리 체크
            if (distance <= attackRange)
            {
                ChangeState(RobotState.R_Attack);
                if (Time.time - lastAttackTime >= attackDelay)
                {
                    ChangeState(RobotState.R_Attack);
                    lastAttackTime = Time.time;
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
                    break;
                case RobotState.R_Death:
                    break;
            }
            
            
        }
        #endregion


    }
}