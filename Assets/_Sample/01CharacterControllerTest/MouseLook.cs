using UnityEngine;
using UnityEngine.InputSystem;
namespace MyFps
{
    public class MouseLook : MonoBehaviour
    {
        #region Variables

        //참조
        public Transform cameratrans;   //카메라 루트

        //마우스 입력값의 보정 값
        [SerializeField]private float sensivity = 5f;


        //카메라 회전
        float rotateX = 0f;

        //마우스 입력(위치) 값
        private Vector2 inputLook;

        //그라운드 체크

        #endregion

        #region Unity Event Method
        private void Update()
        {
            //마우스 포지션 좌우 입력받아 플레이어 로테이션 좌우 회전
            //float mouseX = Input.GetAxis("Mouse X") * sensivity;
            this.transform.Rotate(Vector3.up * Time.deltaTime * inputLook.x * sensivity);

            //마우스 포지션 위 아래 입력받아 카메라 위아래 회전
            //float mouseY = Input.GetAxis("Mouse Y") * sensivity;

            rotateX -= inputLook.y * Time.deltaTime * sensivity;
            rotateX = Mathf.Clamp(rotateX, -90f, 40f);

            //카메라 위아래 회전
            cameratrans.localRotation = Quaternion.Euler(rotateX, 0f, 0f);
        }
        #endregion

        #region Custom Method
        public void OnLook(InputAction.CallbackContext context)
        {
            inputLook = context.ReadValue<Vector2>();
        }
        #endregion
    }
}