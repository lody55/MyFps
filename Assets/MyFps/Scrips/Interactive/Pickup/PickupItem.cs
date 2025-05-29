using UnityEngine;
namespace MyFps
{
    //�ʿ� ������ ������ �ε����� �Ա�
    public class PickupItem : MonoBehaviour
    {
        

        public float rotateSpeed = 90f;     //�ʴ� 90��ȸ��
        public float bobbingAmount = 0.25f; // ���Ʒ� ��鸲 ����
        public float bobbingSpeed = 3f; //���Ʒ� ��鸮�� ���ǵ�

        

        private Vector3 startPos;

        

        private void Start()
        {
            startPos = transform.position;
        }
        private void Update()
        {
            //360�� ȸ���ϱ�
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);

            //���Ʒ� ����
            float newY = startPos.y + Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;
            Vector3 pos = transform.position;
            pos.y = newY;
            transform.position = pos;
            

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (OnPickup())
                {
                    
                    Destroy(gameObject);
                }

            }
        }


        //������ üũ�Ͽ� �������� ������ true, �������� false
        protected virtual bool OnPickup()
        {
            //�������� ������ �ִ��� üũ
            //������ �Ա� ����

            return true;
        }
    }
}

/*
 1. �÷��̾ �ε����� �浹 üũ : �浹�ϸ�
- źȯ 7�� ����
- ������ ų
- ������ 360�� ȸ��
- ���Ʒ��� �Դٰ��� ��鸲 ����
 */