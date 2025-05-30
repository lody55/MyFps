using System.Collections;
using UnityEngine;
namespace MyFps
{
    public class BreakableObject : MonoBehaviour, IDamageable
    {
        //공격을 받으면(데미지 입으면) 오브젝트가 부서진다
        //오브젝트 부서지는 연출, 두번 다시 공격을 받지 않아야 된다
        //부서질때 그릇 깨지는 사운드 플레이
        //아이템 숨기기
        #region Variables
        public GameObject fakeObject;
        public GameObject realObject;
        public GameObject spereObject;  //액티브 연출용 오브젝트

        private float currentHealth = 1f;
        private bool isBroken = false;
        [SerializeField] bool unBreakable = false;  //깨지지 않는 오브젝트

        //숨겨진 아이템
        //public GameObject hiddenItemPrefab;
        [SerializeField]private Vector3 offset;

        public GameObject hiddenItem;

        #endregion

        

        #region Custom Method
        public void TakeDamage(float damage)
        {
            if (unBreakable) return;    //무적 모드

            currentHealth -= damage;

            //데미지 연출 - Sfx , Vfx...등등
            Debug.Log($"남은 {currentHealth}");

            if (currentHealth <= 0f && isBroken == false)
            {
                Broken();

                //깨지는 연출
                StartCoroutine(Break());
            }
        }
        public void Broken()
        {
            //충돌체 제거
            this.GetComponent<BoxCollider>().enabled = false;
            //깨지는 연출
            isBroken = true;
            
        }
        IEnumerator Break()
        {
            //사운드 플레이
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

            //숨겨진 아이템 나타내기
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