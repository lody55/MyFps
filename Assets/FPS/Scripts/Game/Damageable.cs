using UnityEngine;
namespace FPS.Game
{
    //데미지를 입는 충돌체마다 부착시켜 데미지를 계산하는 클래스
    public class Damageable : MonoBehaviour
    {
        #region Variables
        //참조 Health
        private Health health;

        //데미지 계수
        [SerializeField] float damageMultiplier = 1.0f;

        //셀프 데미지 계수
        private float sensibilityToSelfDamage = 0.5f;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            //Health 컴포넌트는 캐릭오브젝트의 맨 위 부모오브젝트에 붙어 있어야 한다
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

            //범위 공격이 아닌 경우만 데미지 계수 적용
            if (isExplotionDamage == false)
            {
                //데미지 계수 연산
                totalDamage *= damageMultiplier;
            }

            //셀프 데미지 체크
            if(health.gameObject == damageSource)
            {
                totalDamage *= sensibilityToSelfDamage;
            }
            //데미지 계산 후 데미지 적용
            health.TakeDamage(totalDamage, damageSource);
            
        }
        #endregion
    }
}