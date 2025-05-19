using UnityEngine;
namespace MySample
{
    public class RigidBodyTest : MonoBehaviour
    {
        #region Variables
        //참조
        private Rigidbody rb;

        //힘
        //[SerializeField] private float power = 100f;
        #endregion

        private void Start()
        {
            rb = this.GetComponent<Rigidbody>();

            //일회성으로 앞 방향으로 100의 힘으로 오브젝트를 이동시킨다
            //rb.AddForce(Vector3.forward * power, ForceMode.Impulse);
            //rb.AddRelativeForce(Vector3.forward * power, ForceMode.Force);
        }
        private void FixedUpdate()
        {
            
            rb.AddForce(Vector3.forward * 2, ForceMode.Force);
        }
    }
}

/* 
ForceMode.Force = 연속적으로 힘을줌 , 질량도 고려한다 (로컬 앞방향)
- 바람 , 자리력처럼 연속적으로 주어지는 힘

ForceMode.Acceleration = 연속 , 질량 무시  (로컬 앞방향)
- 중력 , 질량에 상관없이 일정한 가속을 구현할때

ForceMode.Impulse = 불연속(1회성) , 질량 고려
- 타격 , 폭발 등 순간적으로 적용하는 힘

ForceMode.VelocityChage = 불연속(1회성) , 질량 무시
- 순간적으로 지정한 속도의 변화를 줄 때
 */