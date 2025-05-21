using TMPro;
using UnityEngine;
namespace MyFps
{
    //���ͷ�Ƽ�� �׼��� �θ� Ŭ����
    public class Interactive : MonoBehaviour
    {
        protected float distance = 0f;
        protected string action = "Do Action";

        public Animator animator;
        
        //[SerializeField] GameObject realPistol;
        [SerializeField] protected GameObject actionUI;
        //[SerializeField] GameObject arrow;
        [SerializeField] protected GameObject extraCross;
        [SerializeField] protected TextMeshProUGUI actionText;



         void Update()
         {
                distance = PlayerCasting.distanceFromTarget;
         }

        private void OnMouseOver()
        {
            extraCross.SetActive(true);
            if (distance <= 2f)
            {
                //Debug.Log("�׼� (E)Ű�� ��������");
                //Action UI �����ֱ�
                ShowActionUI();
                extraCross.SetActive(true);
                //Ű �Է� üũ
                if (Input.GetKeyDown(KeyCode.E))
                {
                    DoAction();
                }
            }
            else
            {
                HideActionUI();
                extraCross.SetActive(false);
            }
        }

        protected virtual void OnMouseExit()
        {

            HideActionUI();
            extraCross.SetActive(false);
        }


        protected virtual void ShowActionUI()
        {
            actionUI.SetActive(true);
            actionText.text = action;
        }
        protected virtual void HideActionUI()
        {
            actionUI.SetActive(false);
            actionText.text = "";
        }
        protected virtual void DoAction()
        {
            
        }

    }


    
}
