using Unity.Properties;
using UnityEngine;
namespace MyFps
{
    //�κ� ����
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
        //����
        public Animator animator;
        public Transform thePlayer;     //���� Ÿ��
        private RobotHealth robot;

        //�κ��� ���� ����
        private RobotState robotState;

        //�κ��� ���� ����
        private RobotState beforeState;

        //�ִϸ��̼� �Ķ����
        private string enemyState = "EnemyState";


        

        //�̵� �ӵ�(�ȱ�)
        [SerializeField]private float moveSpeed = 5f;

        //���� ����
        [SerializeField]private float attackRange = 2f;

        //���� �ð�
        [SerializeField] private float attackDelay = 2f;
        //���� Ÿ�̸�
        [SerializeField] private float lastAttackTime;

        //���ݷ�
        private float attackDamage = 5f; 

        #endregion

        #region Unity Event Method
        
        
        private void Awake()
        {
            robot = this.GetComponent<RobotHealth>();
            //����
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
        //���ο� ���¸� �Ű������� �޾� ���ο� ���·� ����
        public void ChangeState(RobotState newState)
        {
            //���� ���� üũ
            if(robotState == newState)
            {
                return;
            }

            //���� ���¸� ���� ���·� ����
            beforeState = robotState; 

            //���ο� ���¸� ���� ���·� ����
            robotState = newState;

            //���� ���濡 ���� ���� ����
            animator.SetInteger(enemyState, (int)robotState);
        }

       
        private void EnemyAttack()
        {


            Vector3 target = new Vector3(thePlayer.position.x, this.transform.position.y, thePlayer.position.z);
            Vector3 dir = target - this.transform.position;

            float distance = Vector3.Distance(transform.position, target);



            //��Ÿ� üũ
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
                    
                   

                    //���� ���� üũ
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
                //Ÿ�̸� ����
                
                //Ÿ�̸� �ʱ�ȭ
                lastAttackTime = 0f;
                
            }
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

        //������ ȣ��Ǵ� �Լ�
        private void OnDie()
        {
            ChangeState(RobotState.R_Death);
            this.GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject, 10f);
        }
        #endregion


    }
}