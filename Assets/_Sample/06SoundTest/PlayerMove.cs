using UnityEngine;
namespace MySample
{
    //�÷��̾� �̵� Ŭ����
    public class PlayerMove : MonoBehaviour
    {
        #region Variables
        //���� 
        private Rigidbody rb;

        //������ �̵��ϴ� ��
        [SerializeField] private float forwardForce = 10f;

        //�¿�� �̵��ϴ� ��
        [SerializeField] private float sideForce = 5f;

        //��ǲ ��
        private float dx = 0f;

        #endregion

        #region Unity Event Method

        private void Start()
        {
            //����
            rb = this.GetComponent<Rigidbody>();
        }
        private void Update()
        {
            dx = Input.GetAxis("Horizontal");   // -1 ~ 1
            
        }
        private void FixedUpdate()
        {
            rb.AddForce(0f, 0f, forwardForce, ForceMode.Acceleration);

            if(dx < 0 )
            {
                rb.AddForce(-sideForce, 0f, 0f, ForceMode.Acceleration);
            }
            if(dx > 0)
            {
                rb.AddForce(sideForce, 0f, 0f, ForceMode.Acceleration);
            }
        }
        #endregion

        #region Custom Method

        #endregion
    }
}

/*
 �̵��� Rigidbody Force�� �̿��Ͽ� �̵��Ѵ�
������ �ڵ����� �̵�
�¿�Ű�� �Է¹޾� �¿� �̵�
 */