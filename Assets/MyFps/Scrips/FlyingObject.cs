using UnityEngine;
namespace MyFps
{
    //그라운드에 부딪히면 사운드 플레이
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