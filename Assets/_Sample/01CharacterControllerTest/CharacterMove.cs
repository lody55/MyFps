using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
namespace MySample
{
    //ĳ���� ��Ʈ�ѷ� �̵����� Ŭ����
    public class CharacterMove : MonoBehaviour
    {
        #region Variables
        //����
        private CharacterController controller;

        //�Է�
        private Vector2 inputMove;

        //�̵�
        [SerializeField]private float moveSpeed = 10f;

        //�߷�
        private float gravity = -9.81f;
        private Vector3 velocity;   //�߷� ��꿡 ���� �̵� �ӵ�

        //�׶��� üũ
        public Transform groundCheck;   //�߹ٴ� ��ġ
        [SerializeField]private float checkRange = 0.2f;    //üũ�ϴ� ���� �ݰ�
        [SerializeField]private LayerMask groundMask;   //�׶��� ���̾� �Ǻ�

        //���� ����
        [SerializeField] private float jumpHeight = 2f;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            controller = this.GetComponent<CharacterController>();
            
        }

        private void Update()
        {
            //���� ������
            bool isGrounded = GroundCheck();
            if (isGrounded && velocity.y < 0f)
            {
                velocity.y = -5f;
            }
            //����
            //Global �� �̵�
            //Vector3 move = new Vector3(inputMove.x, 0, inputMove.y) * moveSpeed;
            //Local �� �̵�
            Vector3 move = (transform.right *inputMove.x + transform.forward * inputMove.y) * moveSpeed;
            //�̵�
            controller.Move(move * Time.deltaTime);

            //�߷¿� ���� y�� �̵�
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
        #endregion

        #region Custum Method
        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();
        }

        //�׶��� üũ
        bool GroundCheck()
        {
            return Physics.CheckSphere(groundCheck.position, checkRange , groundMask);
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started && GroundCheck())
            {
                //���� ���̸� �ٴ� �ӵ� ���ϱ�
                velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
            }
        }
        #endregion
    }
}

/*
 - �ӵ� = �߷°��ӵ� x �ð�
- ������ �Ÿ� = 0.5 
 */