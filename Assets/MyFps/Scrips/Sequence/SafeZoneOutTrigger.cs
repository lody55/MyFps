using UnityEngine;
namespace MyFps
{
    //트리거에 들어가면 플레이어가 안전지역에 있다 저장
    public class SafeZoneOutTrigger : MonoBehaviour
    {
        #region Variables
        //SafeZonOutTrigger 오브젝트
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
