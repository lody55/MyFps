using MyDefence;
using System.Collections;
using UnityEngine;

namespace MyFps
{
    //플레이어의 체력을 관리하는 클래스
    public class PlayerHealth : MonoBehaviour
    {
        //게임스타트 딜레이 이미지
        //public GameObject blackImage;

        public SceneFader fader;
        [SerializeField] private string loadToScene = "GameOver";
        #region Variables
        //체력
        private float currentHealth;

        [SerializeField] float maxHealth = 20f;

        private bool isDeath;

        //데미지 효과
        public GameObject damageFlash;

        public AudioSource hurt01;
        public AudioSource hurt02;
        public AudioSource hurt03;
        #endregion

        #region Unity Event Method

        private void Start()
        {
            currentHealth = maxHealth;
        }

        #endregion

        #region Custom Method
        //플레이어 데미지

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            Debug.Log($"Player currentHealth : {currentHealth}");

            //데미지 연출(Sfx, Vfx)
            StartCoroutine(DamageEffect());


            if (currentHealth <= 0 && isDeath == false)
            {
                Die();
            }
        }

        IEnumerator DamageEffect()
        {
            //vfx
            damageFlash.SetActive(true);
            //카메라 흔들기
            CinemachinCameraShake.Instance.Shake(2f, 2f, 0.5f);
            //sfx
            int randNum = Random.Range(1, 4);
            if (randNum == 1)
            {
                hurt01.Play();
            }
            else if (randNum == 2)
            {
                hurt02.Play();
            }
            else
            {
                hurt03.Play();
            }
            yield return new WaitForSeconds(1f);
            damageFlash.SetActive(false);
        }

        private void Die()
        {
            isDeath = true;

            fader.FadeTo(loadToScene);
            //죽음처리
            Debug.Log("Game Over");
        }
        #endregion
    }
}