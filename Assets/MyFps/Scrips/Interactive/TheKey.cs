using Unity.VisualScripting;
using UnityEngine;
namespace MyFps
{
    public class TheKey : PickupItem
    {
        

        private void Start()
        {
            
        }
        //������ üũ�Ͽ� �������� ������ true, �������� false
        protected override bool OnPickup()
        {
            //���� ������ ȹ��

            return true;
        }
    }
}