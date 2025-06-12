using UnityEngine;
namespace FPS.Game
{
    public class Destructable : MonoBehaviour
    {
        #region Variables
        //����
        private Health health;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //����
            health = this.GetComponent<Health>();
        }
        private void Start()
        {
            //�ʱ�ȭ
            //Health�� UnityAction �Լ� ���
            health.OnDamage += OnDamaged;

        }
        #endregion
        #region Custom Method
        //Health�� UnityAction �Լ� OnDamage�� ��ϵ� �Լ�
        private void OnDamaged(float damage, GameObject damageSource)
        {
            //ToDo : ������ ����
        }
        //Health�� UnityAction �Լ� OnDie�� ��ϵ� �Լ�
        private void OnDie()
        {
            //����ó��....

            //������Ʈ ų
            Destroy(gameObject);
        }
        //Health�� UnityAction �Լ� OnHeal�� ��ϵ� �Լ�
        private void OnHeal(float healAmount)
        {
            //ToDo : �� ����
        }
        #endregion

    }
}