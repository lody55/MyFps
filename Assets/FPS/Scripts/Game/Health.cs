using UnityEngine;
using UnityEngine.Events;
namespace FPS.Game
{
    //ü���� �����ϴ� Ŭ����
    public class Health : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float maxHealth = 100f;

        private bool isDeath = false;

        //ü�� ���� ��� ����
        [SerializeField]private float criticalHealthRatio = 0.2f;

        public UnityAction<float> OnHeal;  //���ϸ� ��ϵ� �Լ��� ȣ���Ѵ�
        public UnityAction<float, GameObject> OnDamage; //������ ������ ��ϵ� �Լ��� ȣ���Ѵ�
        public UnityAction OnDie;   //������ ��ϵ� �Լ��� ȣ���Ѵ�
        #endregion
        #region ProPerty
        public float CurrentHealth { get; private set; }

        //���� üũ
        public bool Invincible { get; set; }

        #endregion
        #region Unity Event Method
        private void Start()
        {
            //�ʱ�ȭ
            CurrentHealth = maxHealth;
        }


        #endregion

        #region Custom Method
        //�� �������� ������ �ִ��� üũ
        public bool CanPickUp() => CurrentHealth < maxHealth;

        //UI HP �� ������ ��
        public float GetRatio() => CurrentHealth / maxHealth;

        public bool IsCritical() => GetRatio() <= criticalHealthRatio;
        //���� üũ
        
        //�Ű����� ��������, �������� �� ������Ʈ
        public void TakeDamage(float damage, GameObject damageSource)
        {
            //����üũ
            if (Invincible == true) return;

            //������ ��� - ������ ���� ������ ���
            float beforeHealth = CurrentHealth;     //������ �Ա� ���� ü��
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);  //ü���� �ִ� �ּҰ� Ŭ����

            //���� ������
            float realDamage = beforeHealth - CurrentHealth;
            if(realDamage > 0f)
            {
                //������ ȿ�� - ��ϵ� �Լ��� ȣ���Ѵ�
                OnDamage?.Invoke(realDamage, damageSource);
            }

            //���� ó��
            HandleDeath();
        }
        private void HandleDeath()
        {
            if (isDeath == true) return;
            
            if(CurrentHealth <= 0)
            {
                isDeath = true;

                //���� ����
                OnDie?.Invoke();
            }
        }
        public void Heal(float healAmount)
        {
            
            float beforeHealth = CurrentHealth;     //�� �ޱ� ���� ü��
            CurrentHealth += healAmount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);  //ü���� �ִ� �ּҰ� Ŭ����

            //���� ���� ���
            float realHeal = CurrentHealth - beforeHealth;
            if (healAmount >= 0f)
            {
                //�� ���� - ��ϵ� �Լ��� ȣ���Ѵ�
                OnHeal?.Invoke(realHeal);
            }
        }
        #endregion

    }
}