using TMPro;
using UnityEngine;
namespace MyFps
{
    //인터렉티브 액션의 부모 클래스
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
                //Debug.Log("액션 (E)키를 누르세요");
                //Action UI 보여주기
                ShowActionUI();
                extraCross.SetActive(true);
                //키 입력 체크
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
