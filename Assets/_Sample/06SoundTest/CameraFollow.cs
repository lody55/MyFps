using System.Security.Cryptography;
using UnityEngine;
namespace MySample
{
    //�÷��̾ offset ��ġ���� �Ѿư���
    public class CameraFollow : MonoBehaviour
    {
        #region Variables
        public Transform thePlayer;
        public Vector3 offset;

        #endregion
        private void LateUpdate()
        {
            transform.position = thePlayer.position + offset;
        }
    }
}