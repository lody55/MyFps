using MyDefence;
using UnityEngine;
using UnityEngine.Audio;
namespace MyFps
{
    //다음 씬 넘어가기 , 문을 열면 문여는 소리플레이, 배경음 종료, 다음씬 이동
    public class DoorCellExit : Interactive
    {
        [SerializeField] AudioSource bgm01;
        [SerializeField] AudioSource openDoorSound;


        private SceneFader sceneFader;
        private string nextScene = "MainScene02";
        private string paramIsOpen = "IsOpen";
        private string actiont = "Next Stage";

        public void Start()
        {
            sceneFader = FindObjectOfType<SceneFader>();
        }


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
            bgm01.Stop();
            //문여는 소리
            openDoorSound.Play();
            sceneFader.FadeTo(nextScene);
            
            this.GetComponent<BoxCollider>().enabled = false;   //충돌체 제거


        }
    }
}