using UnityEngine;
using UnityEngine.Events;
namespace MyFps
{
    public class RobotHealth : MonoBehaviour, IDamageable
    {
        //�κ��� ü���� �����ϴ� Ŭ����

        #region Variables
        //ü��
        private float currentHealth;
        [SerializeField] private float maxHealth = 20f;
        private bool isDeath = false;

        //������ ȣ��Ǵ� �̺�Ʈ �Լ� ����
        public UnityAction OnDie;
        #endregion

        #region Property
        public bool IsDeath => isDeath;
        

        #endregion

        #region Unity Event Method
        private void Start()
        {
            //�ʱ�ȭ
            currentHealth = maxHealth;
        }
        #endregion

        #region Custom Method
        //������ �Ա�
        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            Debug.Log($"Robot currentHealth : {currentHealth}");
            //������ ���� - Sfx , Vfx...���


            if (currentHealth <= 0f && isDeath == false)
            {
                Death();
            }
        }
        private void Death()
        {
            isDeath = true;

            //������ ��ϵǴ� �Լ� ȣ��, ����ó��...��
            OnDie?.Invoke();

            Destroy(gameObject, 5f);
        }
        #endregion
    }
}