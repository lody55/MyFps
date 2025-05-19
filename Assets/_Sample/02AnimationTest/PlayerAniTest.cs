using UnityEngine;
using UnityEngine.InputSystem;
namespace MySample
{
    public class PlayerAniTest : MonoBehaviour
    {
        #region Variables
        //����
        private Animator animator;
        private Vector3 inputMove;
        private float moveX;
        private float moveY;


        //�̵�
        [SerializeField] private float moveSpeed = 5f;
        
        //�ִϸ��̼� �Ķ����
        [SerializeField] string moveMode = "MoveMode";
        


        #endregion

        #region Unity Event Method

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            moveX = Input.GetAxis("Horizontal");    //ad, �¿� ȭ��ǥ
            moveY = Input.GetAxis("Vertical");      //ws, ���Ʒ� ȭ��ǥ

            // �̵�
            Vector3 dir = new Vector3(moveX, 0, moveY);
            transform.Translate(dir * moveSpeed * Time.deltaTime,Space.World);

            //transform.position += inputMove * moveSpeed * Time.deltaTime;
            //�ִϸ��̼�
            //AnimationStateTest();
            AniamtionBlendTest();


        }
        #endregion
        #region Custom Method
        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            inputMove = new Vector3(input.x, 0, input.y);
        }
        void AnimationStateTest()
        {
            if(moveX == 0f && moveY == 0f)
            {
                animator.SetInteger(moveMode, 2);
            }
            else
            {
                //�յ��¿�
                if(moveY > 0)
                {
                    animator.SetInteger(moveMode, 1);
                }
                else if(moveY <0 )
                {
                    animator.SetInteger(moveMode, 0);
                }
                if(moveX <0)
                {
                    animator.SetInteger(moveMode, 3);
                }
                if(moveX >0)
                {
                    animator.SetInteger(moveMode, 4);
                }
            }
        }
        void AniamtionBlendTest()
        {
            animator.SetFloat("MoveX", moveX);
            animator.SetFloat("MoveY", moveY);
        }
        #endregion
    }
}
/*
 1.WASD�Է� �޾Ƽ� �÷��̾� ������ ����(old input, new input)

 2.�յ��¿� �̵��� �յ��¿� �ִϸ��̼� �÷���

 3.�̵��� ���������� ��� �ִϸ��̼��� �÷����Ѵ�
 */