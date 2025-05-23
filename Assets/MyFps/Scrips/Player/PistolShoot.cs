using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
namespace MyFps
{
    //�ǽ��� ���� Ŭ����
    public class PistolShoot : MonoBehaviour
    {
        #region Variables

        private Animator animator;
        [SerializeField] GameObject fireEffect;
        [SerializeField] AudioSource soundSource;
        [SerializeField] Transform firePoint;

        public ParticleSystem muzzleSystem;
        //�ǰ� ����Ʈ - �淿�� �ǰݵǴ� �������� ����Ʈ ȿ�� �߻�
        public GameObject hitImpactPrefab;

        //hit ��ݰ���
        [SerializeField]private float impactForce = 10f;

        

        //�������
        private bool isFire = false;

        //�ִϸ��̼� �Ķ����
        private string fire = "Fire";
        //���ݷ�
        private float attackDamage = 5f;
        //���ݹ���
        private float maxAttackDistance = 200f;

        #endregion

        #region Unity Event Method
        private void Start()
        {
            animator =this.GetComponent<Animator>();
        }

        private void OnDrawGizmosSelected()
        {
            //FirePoint���� DrawRay(Red) �ִ� 200����
            //���̸� ���� 200 �ȿ� �浹ü�� ������ �浹ü���� ���̸� �׸���
            //�浹ü�� ������ ���̸� 200���� �׸���

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

            //���̸� ���� 200 �ȿ� ��(�κ�)�� ������ ������ �������� �ش�
            RaycastHit hit;
            bool isHit = Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.forward), out hit);
            if(isHit)
            {
               
                
                Debug.Log($"{hit.transform.name}���� {attackDamage} �������� �ش�");
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

            //�ִϸ��̼� �÷���
            animator.SetTrigger(fire);

            if (muzzleSystem)
            {
                muzzleSystem.Play();
            }
            //����
            fireEffect.SetActive(true);
            soundSource.Play();
            yield return new WaitForSeconds(0.3f);
            //�߻� ����Ʈ �÷��� ��Ȱ��ȭ
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