using UnityEngine;
namespace MyFps
{
    public class PickupItemAmmoBox : PickupItem
    {
        #region Variables
        //�����۸Դ� ȿ��
        private int giveAmmo = 7;

        #endregion

        #region Unity Event Method

        #endregion

        #region Custom Method
        protected override bool OnPickup()
        {
            //�����۸Ծ�����üũ

            //������ ���� �� ȿ��
            PlayerDataManager.Instance.AmmoCount += giveAmmo;
            return true;
        }
        #endregion
    }
}