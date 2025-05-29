using UnityEngine;
namespace MyFps
{
    public class PickupAmmoBox : Interactive
    {
        #region Variables
        protected string pickup = "Pick Up AmmoBox";

        //  [SerializeField] protected GameObject arrow;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            
        }
        #endregion

        #region Custom Method
        protected override void ShowActionUI()
        {
            actionUI.SetActive(true);
            actionText.text = pickup;
        }
        protected override void DoAction()
        {
            PlayerDataManager.Instance.AmmoCount += 7;
            //arrow.SetActive(false);
            actionUI.SetActive(false);
            extraCross.SetActive(false);
            Debug.Log("źȯ 7���� ���� �߽��ϴ�");
            actionText.text = "";
            Destroy(gameObject);
            
        }
        #endregion
    }
}