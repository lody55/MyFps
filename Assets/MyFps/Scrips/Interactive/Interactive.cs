using TMPro;
using UnityEngine;
namespace MyFps
{
    //인터렉티브 액션의 부모 클래스
    public class Interactive : MonoBehaviour
    {
        [SerializeField]protected float distance = 0f;
        protected string action = "Do Action";

        public Animator animator;
        
        
        [SerializeField] protected GameObject actionUI;
        
        [SerializeField] protected GameObject extraCross;
        [SerializeField] protected TextMeshProUGUI actionText;
        //인터렉티브 기능 사용 여부
        [SerializeField]protected bool unInteractive = false;
        
        private Transform playerTransform;



        private void Start()
        {
            
        }

        void Update()
        {
            distance = PlayerCasting.distanceFromTarget;
        }

        private void OnMouseOver()
        {
            //인터렉티브 기능 끄기
            if (unInteractive) return;


            if (distance <= 2f)
            {
                
                ShowActionUI();
                extraCross.SetActive(true);
                
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
