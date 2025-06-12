using UnityEngine;
namespace FPS.Game
{
    //�������� �Դ� �浹ü���� �������� �������� ����ϴ� Ŭ����
    public class Damageable : MonoBehaviour
    {
        #region Variables
        //���� Health
        private Health health;

        //������ ���
        [SerializeField] float damageMultiplier = 1.0f;

        //���� ������ ���
        private float sensibilityToSelfDamage = 0.5f;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //����
            //Health ������Ʈ�� ĳ��������Ʈ�� �� �� �θ������Ʈ�� �پ� �־�� �Ѵ�
            health = GetComponent<Health>();
            if(health == null)
            {
                health = this.GetComponentInParent<Health>();
            }

        }
        #endregion

        #region Custom Method
        public void InflictDamage(float damage, bool isExplotionDamage, GameObject damageSource)
        {
            if (health == null) return;
            var totalDamage = damage;

            //���� ������ �ƴ� ��츸 ������ ��� ����
            if (isExplotionDamage == false)
            {
                //������ ��� ����
                totalDamage *= damageMultiplier;
            }

            //���� ������ üũ
            if(health.gameObject == damageSource)
            {
                totalDamage *= sensibilityToSelfDamage;
            }
            //������ ��� �� ������ ����
            health.TakeDamage(totalDamage, damageSource);
            
        }
        #endregion
    }
}