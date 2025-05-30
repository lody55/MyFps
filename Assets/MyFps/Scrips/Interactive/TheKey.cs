using Unity.VisualScripting;
using UnityEngine;
namespace MyFps
{
    public class TheKey : PickupItem
    {
        

        private void Start()
        {
            
        }
        //조건을 체크하여 아이템을 먹으면 true, 못먹으면 false
        protected override bool OnPickup()
        {
            //퍼즐 아이템 획득

            return true;
        }
    }
}