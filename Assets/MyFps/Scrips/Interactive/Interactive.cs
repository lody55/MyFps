using TMPro;
using UnityEngine;
namespace MyFps
{
    //���ͷ�Ƽ�� �׼��� �θ� Ŭ����
    public class Interactive : MonoBehaviour
    {
        [SerializeField]protected float distance = 0f;
        protected string action = "Do Action";

        public Animator animator;
        
        
        [SerializeField] protected GameObject actionUI;
        
        [SerializeField] protected GameObject extraCross;
        [SerializeField] protected TextMeshProUGUI actionText;
        //���ͷ�Ƽ�� ��� ��� ����
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
            //���ͷ�Ƽ�� ��� ����
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
