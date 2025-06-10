using UnityEngine;
namespace MyFps
{
    //Ʈ���ſ� ���� �÷��̾ ���������� �ִ� ����
    public class SafeZoneOutTrigger : MonoBehaviour
    {
        #region Variables
        //SafeZonOutTrigger ������Ʈ
        public GameObject safeZoneIn;
        #endregion
        #region Unity Event Method
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerController.safeZoneIn = false;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                safeZoneIn.SetActive(true);

                this.gameObject.SetActive(false);
            }
        }
        #endregion
    }
}
