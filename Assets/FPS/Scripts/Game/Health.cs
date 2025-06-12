using UnityEngine;
using UnityEngine.Events;
namespace FPS.Game
{
    //체력을 관리하는 클래스
    public class Health : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float maxHealth = 100f;

        private bool isDeath = false;

        //체력 위험 경계 비율
        [SerializeField]private float criticalHealthRatio = 0.2f;

        public UnityAction<float> OnHeal;  //힐하면 등록된 함수를 호출한다
        public UnityAction<float, GameObject> OnDamage; //데미지 입으면 등록된 함수를 호출한다
        public UnityAction OnDie;   //죽을때 등록된 함수를 호출한다
        #endregion
        #region ProPerty
        public float CurrentHealth { get; private set; }

        //무적 체크
        public bool Invincible { get; set; }

        #endregion
        #region Unity Event Method
        private void Start()
        {
            //초기화
            CurrentHealth = maxHealth;
        }


        #endregion

        #region Custom Method
        //힐 아이템을 먹을수 있는지 체크
        public bool CanPickUp() => CurrentHealth < maxHealth;

        //UI HP 바 게이지 량
        public float GetRatio() => CurrentHealth / maxHealth;

        public bool IsCritical() => GetRatio() <= criticalHealthRatio;
        //위험 체크
        
        //매개변수 데미지량, 데미지를 준 오브젝트
        public void TakeDamage(float damage, GameObject damageSource)
        {
            //무적체크
            if (Invincible == true) return;

            //데미지 계산 - 실제로 입은 데미지 계산
            float beforeHealth = CurrentHealth;     //데미지 입기 전의 체력
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);  //체력의 최대 최소값 클램프

            //리얼 데미지
            float realDamage = beforeHealth - CurrentHealth;
            if(realDamage > 0f)
            {
                //데미지 효과 - 등록된 함수를 호출한다
                OnDamage?.Invoke(realDamage, damageSource);
            }

            //죽음 처리
            HandleDeath();
        }
        private void HandleDeath()
        {
            if (isDeath == true) return;
            
            if(CurrentHealth <= 0)
            {
                isDeath = true;

                //죽음 구현
                OnDie?.Invoke();
            }
        }
        public void Heal(float healAmount)
        {
            
            float beforeHealth = CurrentHealth;     //힐 받기 전의 체력
            CurrentHealth += healAmount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);  //체력의 최대 최소값 클램프

            //리얼 힐량 계산
            float realHeal = CurrentHealth - beforeHealth;
            if (healAmount >= 0f)
            {
                //힐 구현 - 등록된 함수를 호출한다
                OnHeal?.Invoke(realHeal);
            }
        }
        #endregion

    }
}