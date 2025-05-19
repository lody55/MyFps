using UnityEngine;
using UnityEngine.InputSystem;
namespace MySample
{
    public class PlayerAniTest : MonoBehaviour
    {
        #region Variables
        //참조
        private Animator animator;
        private Vector3 inputMove;
        private float moveX;
        private float moveY;


        //이동
        [SerializeField] private float moveSpeed = 5f;
        
        //애니메이션 파라미터
        [SerializeField] string moveMode = "MoveMode";
        


        #endregion

        #region Unity Event Method

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            moveX = Input.GetAxis("Horizontal");    //ad, 좌우 화살표
            moveY = Input.GetAxis("Vertical");      //ws, 위아래 화살표

            // 이동
            Vector3 dir = new Vector3(moveX, 0, moveY);
            transform.Translate(dir * moveSpeed * Time.deltaTime,Space.World);

            //transform.position += inputMove * moveSpeed * Time.deltaTime;
            //애니메이션
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
                //앞뒤좌우
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
 1.WASD입력 받아서 플레이어 움직임 구현(old input, new input)

 2.앞뒤좌우 이동시 앞뒤좌우 애니메이션 플레이

 3.이동이 없을때에는 대기 애니메이션을 플레이한다
 */