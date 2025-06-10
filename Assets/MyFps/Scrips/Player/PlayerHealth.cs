using MyDefence;
using System.Collections;
using UnityEngine;

namespace MyFps
{
    //�÷��̾��� ü���� �����ϴ� Ŭ����
    public class PlayerHealth : MonoBehaviour
    {
        //���ӽ�ŸƮ ������ �̹���
        //public GameObject blackImage;

        public SceneFader fader;
        [SerializeField] private string loadToScene = "GameOver";
        #region Variables
        //ü��
        private float currentHealth;

        [SerializeField] float maxHealth = 20f;

        private bool isDeath;

        //������ ȿ��
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
        //�÷��̾� ������

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            Debug.Log($"Player currentHealth : {currentHealth}");

            //������ ����(Sfx, Vfx)
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
            //ī�޶� ����
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
            //����ó��
            Debug.Log("Game Over");
        }
        #endregion
    }
}