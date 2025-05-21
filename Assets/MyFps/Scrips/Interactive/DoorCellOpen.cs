using MySample;
using System.Linq;
using TMPro;
using UnityEngine;
namespace MyFps
{
    //������ ���ͷ�Ƽ�� �׼� ����
    //�÷��̾�� �������� �Ÿ��� 2���� �϶� ���콺 ������ �÷� ������ ActionŰ�� ��������
    public class DoorCellOpen : Interactive
    {
        #region

        

        //���� �÷��̾���� �Ÿ�
        

        //�׼� UI
        

        protected string actiont = "Open The Door";

        //�ִϸ��̼�
        

        //�ִ� �Ķ���� ��Ʈ��
        private string paramIsOpen = "IsOpen";

        //������ �Ҹ�
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
            //UI����� ������ �浹ü ����
            HideActionUI();
            animator.SetBool(paramIsOpen, true);        //�� ���� �ִϸ��̼�
            this.GetComponent<BoxCollider>().enabled = false;   //�浹ü ����
            //������ �Ҹ�
            audioSource.Play();
        }


    }
}