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
        #region Variables
        //����
        public Animator animator;
        public Transform thePlayer;     //���� Ÿ��
        
        //�κ��� ���� ����
        private RobotState robotState;

        //�κ��� ���� ����
        private RobotState beforeState;

        //�ִϸ��̼� �Ķ����
        private string enemyState = "EnemyState";

        //ü��
        private float currentHealth;
        [SerializeField]private float maxHealth = 20f;
        private bool isDeath = false;


        //�̵� �ӵ�(�ȱ�)
        [SerializeField]private float moveSpeed = 5f;

        //���� ����
        [SerializeField]private float attackRange = 2f;

        //���� �ð�
        [SerializeField] private float attackDelay = 2f;
        //������ ���ݽð�
        [SerializeField] private float lastAttackTime;

        #endregion

        #region Unity Event Method
        private void Start()
        {
            //����
            animator = this.GetComponent<Animator>();
            
        }
        private void OnEnable()
        {
            //�ʱ�ȭ
            ChangeState(RobotState.R_Idle);
        }

        private void Update()
        {
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

        //������ �Ա�
        public void TakeDamage(float damage)
        {
            currentHealth -= damage;

            //������ ���� - Sfx , Vfx...���

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

            //����ó��...��
            
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

            //��Ÿ� üũ
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