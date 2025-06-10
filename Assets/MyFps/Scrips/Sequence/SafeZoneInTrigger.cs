using System.Linq.Expressions;
using UnityEngine;
namespace MyFps
{
    //트리거에 들어가면 플레이어가 안전지역에 있다 저장
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