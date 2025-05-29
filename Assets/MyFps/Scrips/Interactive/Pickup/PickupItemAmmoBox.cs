using UnityEngine;
namespace MyFps
{
    public class PickupItemAmmoBox : PickupItem
    {
        #region Variables
        //아이템먹는 효과
        private int giveAmmo = 7;

        #endregion

        #region Unity Event Method

        #endregion

        #region Custom Method
        protected override bool OnPickup()
        {
            //아이템먹었는지체크

            //아이템 먹은 후 효과
            PlayerDataManager.Instance.AmmoCount += giveAmmo;
            return true;
        }
        #endregion
    }
}