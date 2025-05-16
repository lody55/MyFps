using MySample;
using System.Linq;
using TMPro;
using UnityEngine;
namespace MyFps
{
    //������ ���ͷ�Ƽ�� �׼� ����
    //�÷��̾�� �������� �Ÿ��� 2���� �϶� ���콺 ������ �÷� ������ ActionŰ�� ��������
    public class DoorCellOpen : MonoBehaviour
    {
        #region

        

        //���� �÷��̾���� �Ÿ�
        private float theDistance = 2f;

        //�׼� UI
        [SerializeField] private GameObject actionUI;
        public TextMeshProUGUI actionText;

        public string action = "Open The Door";

        //�ִϸ��̼�
        public Animator animator;

        //�ִ� �Ķ���� ��Ʈ��
        private string paramIsOpen = "IsOpen";
        #endregion


        #region Unity Event Method
        private void Update()
        {
            theDistance = PlayerCasting.distanceFromTarget;

        }

        private void OnMouseOver()
        {
            
            if (theDistance <= 2f)
            {
                //Debug.Log("�׼� (E)Ű�� ��������");
                //Action UI �����ֱ�
                ShowActionUI();
                //Ű �Է� üũ
                if(Input.GetKeyDown(KeyCode.E))
                {
                    //UI����� ������ �浹ü ����
                    HideActionUI();
                    animator.SetBool(paramIsOpen, true);        //�� ���� �ִϸ��̼�
                    this.GetComponent<BoxCollider>().enabled = false;   //�浹ü ����
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
        }

        #endregion
        private void ShowActionUI()
        {
            actionUI.SetActive(true);
            actionText.text = action;
        }

        void HideActionUI()
        {
            actionUI.SetActive(false);
            actionText.text = "";
        }

    }
}