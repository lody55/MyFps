using UnityEngine;
namespace MyFps
{
    //���� ���� Ÿ��
    public enum WeaponType
    {
        None,
        Pistol,

    }


    //�÷��̾� ������ ���� Ŭ���� - �̱���(���� ������ ������ ����)
    public class PlayerDataManager : PersistanceSingleton<PlayerDataManager>
    {
        #region Variables
        //���� Ÿ��


        private int ammoCount;
 
        #endregion

        #region Property
        public WeaponType weaponType { get; set; }
        public int AmmoCount
        {
            get { return ammoCount; }
            set
            {
                ammoCount = value;
            }

        }
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //�÷��� ������ �ʱ�ȭ
            weaponType = WeaponType.None;
            ammoCount = 0;
        }
        #endregion

        #region Custom Method
        //ammo ���� �Լ�

        //ammo ��� �Լ�
        
        #endregion
        public bool UseAmmo(int amount)
        {
            if(ammoCount<amount)
            {
                return false;
            }
            ammoCount -= amount;
            return true;
        }
    }
}