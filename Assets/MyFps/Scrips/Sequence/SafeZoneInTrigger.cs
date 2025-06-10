using System.Linq.Expressions;
using UnityEngine;
namespace MyFps
{
    //Ʈ���ſ� ���� �÷��̾ ���������� �ִ� ����
    public class SafeZoneInTrigger : MonoBehaviour
    {
        #region Variables
        public GameObject safeZoneOutTrigger;
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                PlayerController.safeZoneIn = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                safeZoneOutTrigger.SetActive(true);

                this.gameObject.SetActive(false);
            }
        }
    }
}