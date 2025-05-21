using MySample;
using System.Linq;
using TMPro;
using UnityEngine;
namespace MyFps
{
    //문열기 인터렉티브 액션 구현
    //플레이어와 문까지의 거리가 2이하 일때 마우스 문위에 올려 놓으면 Action키를 누르세요
    public class DoorCellOpen : Interactive
    {
        #region

        

        //문과 플레이어와의 거리
        

        //액션 UI
        

        protected string actiont = "Open The Door";

        //애니메이션
        

        //애니 파라미터 스트링
        private string paramIsOpen = "IsOpen";

        //문여는 소리
        public AudioSource audioSource;

        #endregion


        #region Unity Event Method





        #endregion

        protected override void ShowActionUI()
        {
            actionUI.SetActive(true);
            actionText.text = actiont;
        }

        protected override void DoAction()
        {
            //UI숨기고 문열고 충돌체 제거
            HideActionUI();
            animator.SetBool(paramIsOpen, true);        //문 여는 애니메이션
            this.GetComponent<BoxCollider>().enabled = false;   //충돌체 제거
            //문여는 소리
            audioSource.Play();
        }


    }
}