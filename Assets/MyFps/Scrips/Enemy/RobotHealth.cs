using UnityEngine;
using UnityEngine.Events;
namespace MyFps
{
    public class RobotHealth : MonoBehaviour, IDamageable
    {
        //로봇의 체력을 관리하는 클래스

        #region Variables
        //체력
        private float currentHealth;
        [SerializeField] private float maxHealth = 20f;
        private bool isDeath = false;

        //죽음시 호출되는 이벤트 함수 선언
        public UnityAction OnDie;
        #endregion

        #region Property
        public bool IsDeath => isDeath;
        

        #endregion

        #region Unity Event Method
        private void Start()
        {
            //초기화
            currentHealth = maxHealth;
        }
        #endregion

        #region Custom Method
        //데미지 입기
        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            Debug.Log($"Robot currentHealth : {currentHealth}");
            //데미지 연출 - Sfx , Vfx...등등


            if (currentHealth <= 0f && isDeath == false)
            {
                Death();
            }
        }
        private void Death()
        {
            isDeath = true;

            //죽음시 등록되는 함수 호출, 보상처리...등
            OnDie?.Invoke();

            Destroy(gameObject, 5f);
        }
        #endregion
    }
}