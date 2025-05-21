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

            //초기화
            originColor = render.material.color;

            //시작할때 오른쪽 힘
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
            //빨간색으로 컬러 체인지
            render.material.color = Color.red;
        }
        public void ChangeColorOrigin()
        {
            //원래색으로 컬러 체인지
            render.material.color = originColor;
        }
    }
}