using UnityEngine;
namespace MyFps
{
    //장착 무기 타입
    public enum WeaponType
    {
        None,
        Pistol,

    }


    //플레이어 데이터 관리 클래스 - 싱글톤(다음 씬에서 데이터 보존)
    public class PlayerDataManager : PersistanceSingleton<PlayerDataManager>
    {
        #region Variables
        //무기 타입


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
            //플레이 데이터 초기화
            weaponType = WeaponType.None;
            ammoCount = 0;
        }
        #endregion

        #region Custom Method
        //ammo 저축 함수

        //ammo 사용 함수
        
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