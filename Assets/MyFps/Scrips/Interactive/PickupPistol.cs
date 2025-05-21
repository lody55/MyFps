using TMPro;
using UnityEngine;
namespace MyFps
{
    //������(����) ȹ�� ���ͷ�Ƽ�� ����
    public class PickupPistol : Interactive
    {
        
        protected string pickup = "Pick Up Pistol";
        

        [SerializeField] protected GameObject realPistol;
        [SerializeField] protected GameObject arrow;
        
        


        
        
        

        protected override void ShowActionUI()
        {
            actionUI.SetActive(true);
            actionText.text = pickup;
        }
        
        protected override void DoAction()
        {
            HideActionUI();
            realPistol.SetActive(true);
            arrow.SetActive(false);
            extraCross.SetActive(false);
            Destroy(gameObject);
        }

    }

    
}