using System.Collections;
using UnityEngine;
namespace MyFps
{
    public class BreakableObject : MonoBehaviour, IDamageable
    {
        //������ ������(������ ������) ������Ʈ�� �μ�����
        //������Ʈ �μ����� ����, �ι� �ٽ� ������ ���� �ʾƾ� �ȴ�
        //�μ����� �׸� ������ ���� �÷���
        //������ �����
        #region Variables
        public GameObject fakeObject;
        public GameObject realObject;
        public GameObject spereObject;  //��Ƽ�� ����� ������Ʈ

        private float currentHealth = 1f;
        private bool isBroken = false;
        [SerializeField] bool unBreakable = false;  //������ �ʴ� ������Ʈ

        //������ ������
        //public GameObject hiddenItemPrefab;
        [SerializeField]private Vector3 offset;

        public GameObject hiddenItem;

        #endregion

        

        #region Custom Method
        public void TakeDamage(float damage)
        {
            if (unBreakable) return;    //���� ���

            currentHealth -= damage;

            //������ ���� - Sfx , Vfx...���
            Debug.Log($"���� {currentHealth}");

            if (currentHealth <= 0f && isBroken == false)
            {
                Broken();

                //������ ����
                StartCoroutine(Break());
            }
        }
        public void Broken()
        {
            //�浹ü ����
            this.GetComponent<BoxCollider>().enabled = false;
            //������ ����
            isBroken = true;
            
        }
        IEnumerator Break()
        {
            //���� �÷���
            AudioManager.Instance.Play("PotterySmash");
                
            fakeObject.SetActive(false);
            if (spereObject)
            {
                yield return new WaitForSeconds(0.1f);
                spereObject.SetActive(true);
            }
            realObject.SetActive(true);
            if(spereObject)
            {
                yield return new WaitForSeconds(0.1f);
                spereObject.SetActive(false);
            }

            //������ ������ ��Ÿ����
            //if(hiddenItemPrefab)
            //{
            //    Instantiate(hiddenItemPrefab, transform.position + offset, Quaternion.identity);
            //}
            if(hiddenItem)
            {
                hiddenItem.SetActive(true);
            }
        }
        #endregion
    }
}