using UnityEngine;
namespace MyFps
{
    //인터페이스 - 데미지  
    public interface IDamageable 
    {
        public void TakeDamage(float damage);
    }
}