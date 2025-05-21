using UnityEngine;
namespace CollisionTests
{
    public class Player : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 10f;
        
        Rigidbody rb;
        private Renderer render;
        private Color originColor;

        

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            render = this.GetComponent<Renderer>();

            //�ʱ�ȭ
            originColor = render.material.color;

            //�����Ҷ� ������ ��
            MoveRight();
        }

        public void MoveRight()
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(Vector3.right * moveSpeed, ForceMode.Impulse);
        }

        public void Moveleft()
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(Vector3.left * moveSpeed, ForceMode.Impulse);
        }
        public void ChageColorRed()
        {
            //���������� �÷� ü����
            render.material.color = Color.red;
        }
        public void ChangeColorOrigin()
        {
            //���������� �÷� ü����
            render.material.color = originColor;
        }
    }
}