using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
namespace MyFps
{
    //피스톨 제어 클래스
    public class PistolShoot : MonoBehaviour
    {
        #region Variables

        private Animator animator;
        [SerializeField] GameObject fireEffect;
        [SerializeField] AudioSource soundSource;
        [SerializeField] Transform firePoint;

        public ParticleSystem muzzleSystem;
        //피격 이펙트 - 뷸렛이 피격되는 지점에서 이펙트 효과 발생
        public GameObject hitImpactPrefab;

        //hit 충격강도
        [SerializeField]private float impactForce = 10f;

        

        //연사방지
        private bool isFire = false;

        //애니메이션 파라미터
        private string fire = "Fire";
        //공격력
        private float attackDamage = 5f;
        //공격범위
        private float maxAttackDistance = 200f;

        #endregion

        #region Unity Event Method
        private void Start()
        {
            animator =this.GetComponent<Animator>();
        }

        private void OnDrawGizmosSelected()
        {
            //FirePoint에서 DrawRay(Red) 최대 200으로
            //레이를 쏴서 200 안에 충돌체가 있으면 충돌체까지 레이를 그리고
            //충돌체가 없으면 레이를 200까지 그린다

            RaycastHit hit;
            bool isHit = Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.forward), out hit);
            Gizmos.color = Color.red;
            if (isHit)
            {
                Gizmos.DrawRay(firePoint.position, firePoint.forward * hit.distance);
            }
            else
            {
                Gizmos.DrawRay(firePoint.position, firePoint.forward * maxAttackDistance);
            }
        }
        #endregion

        #region Custom Method
        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.started && isFire == false) // key Down
            {
                
                StartCoroutine(Shot());
            }
        }

        IEnumerator Shot()
        {
            isFire = true;

            //레이를 쏴서 200 안에 적(로봇)이 있으면 적에게 데미지를 준다
            RaycastHit hit;
            bool isHit = Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.forward), out hit);
            if(isHit)
            {
               
                
                Debug.Log($"{hit.transform.name}에게 {attackDamage} 데미지를 준다");
                //Robot enemy = hit.transform.GetComponent<Robot>();
                //if (enemy != null)
                //{
                //    enemy.TakeDamage(attackDamage);
                //}

                //ZombieRobot zombie = hit.transform.GetComponent<ZombieRobot>();
                //if (zombie != null)
                //{
                //    zombie.TakeDamage(attackDamage);
                //}
                if (hit.rigidbody)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce, ForceMode.Impulse);
                }
                IDamageable damageable = hit.transform.GetComponent<IDamageable>();
                if(damageable != null)
                {
                    
                    damageable.TakeDamage(attackDamage);
                }
                //hit.point
                if (hitImpactPrefab)
                {
                    GameObject hitImpactEffect = Instantiate(hitImpactPrefab, hit.point, Quaternion.identity);
                    Destroy(hitImpactEffect, 2f);
                }
            }

            //애니메이션 플레이
            animator.SetTrigger(fire);

            if (muzzleSystem)
            {
                muzzleSystem.Play();
            }
            //연출
            fireEffect.SetActive(true);
            soundSource.Play();
            yield return new WaitForSeconds(0.3f);
            //발사 이펙트 플래시 비활성화
            fireEffect.SetActive(false);
            if (muzzleSystem)
            {
                muzzleSystem.Stop();
            }
            yield return new WaitForSeconds(0.7f);
            
            isFire = false;
        }

        
        #endregion
    }
}