using CollisionTests;
using UnityEngine;
namespace MySample
{
    public class TriggerTest : MonoBehaviour
    {
        
        
        #region Unity Event Method
        private void OnTriggerEnter(Collider other)
        {
            

        }
        private void OnTriggerStay(Collider other)
        {
            Player player = other.transform.GetComponent<Player>();
            player.ChageColorRed();
        }
        private void OnTriggerExit(Collider other)
        {
            Player player = other.transform.GetComponent<Player>();
            if (player)
            {
                player.MoveRight();
                player.ChangeColorOrigin();
            }
        }

        #endregion
    }
}