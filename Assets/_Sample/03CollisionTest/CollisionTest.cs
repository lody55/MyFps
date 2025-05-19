using CollisionTests;
using UnityEngine;
namespace MySample
{
    public class CollisionTest : MonoBehaviour
    {
        
        #region Unity Event Method
        private void OnCollisionEnter(Collision collision)
        {
            //�÷��̾ �������� 3��ŭ ���� �ش�
            
            Player player = collision.transform.GetComponent<Player>();
            if(player)
            {
                player.Moveleft();
            }
        }
        private void OnCollisionStay(Collision collision)
        {
            
        }
        private void OnCollisionExit(Collision collision)
        {
            
        }
        #endregion

        #region Custom Method
        

        #endregion
    }
}