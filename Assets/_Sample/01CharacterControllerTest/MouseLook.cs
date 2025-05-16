using UnityEngine;
using UnityEngine.InputSystem;
namespace MyFps
{
    public class MouseLook : MonoBehaviour
    {
        #region Variables

        //����
        public Transform cameratrans;   //ī�޶� ��Ʈ

        //���콺 �Է°��� ���� ��
        [SerializeField]private float sensivity = 5f;


        //ī�޶� ȸ��
        float rotateX = 0f;

        //���콺 �Է�(��ġ) ��
        private Vector2 inputLook;

        //�׶��� üũ

        #endregion

        #region Unity Event Method
        private void Update()
        {
            //���콺 ������ �¿� �Է¹޾� �÷��̾� �����̼� �¿� ȸ��
            //float mouseX = Input.GetAxis("Mouse X") * sensivity;
            this.transform.Rotate(Vector3.up * Time.deltaTime * inputLook.x * sensivity);

            //���콺 ������ �� �Ʒ� �Է¹޾� ī�޶� ���Ʒ� ȸ��
            //float mouseY = Input.GetAxis("Mouse Y") * sensivity;

            rotateX -= inputLook.y * Time.deltaTime * sensivity;
            rotateX = Mathf.Clamp(rotateX, -90f, 40f);

            //ī�޶� ���Ʒ� ȸ��
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