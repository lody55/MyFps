using UnityEngine;
namespace FPS.Game
{
    public class Destructable : MonoBehaviour
    {
        #region Variables
        //참조
        private Health health;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            health = this.GetComponent<Health>();
        }
        private void Start()
        {
            //초기화
            //Health의 UnityAction 함수 등록
            health.OnDamage += OnDamaged;

        }
        #endregion
        #region Custom Method
        //Health의 UnityAction 함수 OnDamage에 등록될 함수
        private void OnDamaged(float damage, GameObject damageSource)
        {
            //ToDo : 데미지 구현
        }
        //Health의 UnityAction 함수 OnDie에 등록될 함수
        private void OnDie()
        {
            //죽음처리....

            //오브젝트 킬
            Destroy(gameObject);
        }
        //Health의 UnityAction 함수 OnHeal에 등록될 함수
        private void OnHeal(float healAmount)
        {
            //ToDo : 힐 구현
        }
        #endregion

    }
}