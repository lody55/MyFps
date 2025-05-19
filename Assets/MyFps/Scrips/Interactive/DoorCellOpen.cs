using MySample;
using System.Linq;
using TMPro;
using UnityEngine;
namespace MyFps
{
    //문열기 인터렉티브 액션 구현
    //플레이어와 문까지의 거리가 2이하 일때 마우스 문위에 올려 놓으면 Action키를 누르세요
    public class DoorCellOpen : MonoBehaviour
    {
        #region

        

        //문과 플레이어와의 거리
        private float theDistance = 2f;

        //액션 UI
        [SerializeField] private GameObject actionUI;
        public TextMeshProUGUI actionText;

        public string action = "Open The Door";

        //애니메이션
        public Animator animator;

        //애니 파라미터 스트링
        private string paramIsOpen = "IsOpen";

        //크로스헤어
        public GameObject extraCross;
        #endregion


        #region Unity Event Method
        private void Update()
        {
            theDistance = PlayerCasting.distanceFromTarget;

        }

        private void OnMouseOver()
        {
            extraCross.SetActive(true);
            if (theDistance <= 2f)
            {
                //Debug.Log("액션 (E)키를 누르세요");
                //Action UI 보여주기
                ShowActionUI();
                //키 입력 체크
                if(Input.GetKeyDown(KeyCode.E))
                {
                    //UI숨기고 문열고 충돌체 제거
                    HideActionUI();
                    animator.SetBool(paramIsOpen, true);        //문 여는 애니메이션
                    this.GetComponent<BoxCollider>().enabled = false;   //충돌체 제거
                }
            }
            else
            {
                HideActionUI();
            }
        }
        private void OnMouseExit()
        {

            HideActionUI();
            extraCross.SetActive(false);
        }

        #endregion
        private void ShowActionUI()
        {
            actionUI.SetActive(true);
            actionText.text = action;

            //extraCross.SetActive(true);
        }

        void HideActionUI()
        {
            actionUI.SetActive(false);
            actionText.text = "";

            //extraCross.SetActive(false);
        }

    }
}