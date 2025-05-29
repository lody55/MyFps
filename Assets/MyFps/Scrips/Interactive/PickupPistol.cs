using TMPro;
using UnityEngine;
namespace MyFps
{
    //������(����) ȹ�� ���ͷ�Ƽ�� ����
    public class PickupPistol : Interactive
    {
        //[SerializeField] private PistolShoot pistolShoot;
        protected string pickup = "Pick Up Pistol";
        

        [SerializeField] protected GameObject realPistol;
        [SerializeField] protected GameObject arrow;

        public GameObject ammoUI;
        public GameObject ammoBox;
        public GameObject secondTrigger;
        //public GameObject arrow2;


        private void Start()
        {
            
        }




        protected override void ShowActionUI()
        {
            actionUI.SetActive(true);
            actionText.text = pickup;
        }
        
        protected override void DoAction()
        {
            HideActionUI();
            realPistol.SetActive(true);
            //���� ������ ����
            PlayerDataManager.Instance.weaponType = WeaponType.Pistol;
            //pistolShoot = GetComponent<PistolShoot>();
            
            ammoBox.SetActive(true);
            secondTrigger.SetActive(true);
            arrow.SetActive(false);
            extraCross.SetActive(false);
            ammoUI.SetActive(true);
            //arrow2.SetActive(true);
            Destroy(gameObject);
            
        }

    }

    
}