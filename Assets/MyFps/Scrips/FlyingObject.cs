using UnityEngine;
namespace MyFps
{
    //�׶��忡 �ε����� ���� �÷���
    public class FlyingObject : MonoBehaviour
    {
        #region Unity Event Method
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.CompareTag("Ground")&& collision.relativeVelocity.magnitude > 1.0f)
            {
                AudioManager.Instance.Play("DoorBang2");
            }
        }
        #endregion


    }
}