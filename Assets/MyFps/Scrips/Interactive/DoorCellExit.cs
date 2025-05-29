using MyDefence;
using UnityEngine;
using UnityEngine.Audio;
namespace MyFps
{
    //���� �� �Ѿ�� , ���� ���� ������ �Ҹ��÷���, ����� ����, ������ �̵�
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
            //UI����� ������ �浹ü ����
            HideActionUI();
            animator.SetBool(paramIsOpen, true);        //�� ���� �ִϸ��̼�
            bgm01.Stop();
            //������ �Ҹ�
            openDoorSound.Play();
            sceneFader.FadeTo(nextScene);
            
            this.GetComponent<BoxCollider>().enabled = false;   //�浹ü ����


        }
    }
}