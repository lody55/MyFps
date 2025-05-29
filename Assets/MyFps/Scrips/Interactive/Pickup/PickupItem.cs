using UnityEngine;
namespace MyFps
{
    //맵에 떨어진 아이템 부딪혀서 먹기
    public class PickupItem : MonoBehaviour
    {
        

        public float rotateSpeed = 90f;     //초당 90도회전
        public float bobbingAmount = 0.25f; // 위아래 흔들림 높이
        public float bobbingSpeed = 3f; //위아래 흔들리는 스피드

        

        private Vector3 startPos;

        

        private void Start()
        {
            startPos = transform.position;
        }
        private void Update()
        {
            //360도 회전하기
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);

            //위아래 흔들기
            float newY = startPos.y + Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;
            Vector3 pos = transform.position;
            pos.y = newY;
            transform.position = pos;
            

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (OnPickup())
                {
                    
                    Destroy(gameObject);
                }

            }
        }


        //조건을 체크하여 아이템을 먹으면 true, 못먹으면 false
        protected virtual bool OnPickup()
        {
            //아이템을 먹을수 있는지 체크
            //아이템 먹기 구현

            return true;
        }
    }
}

/*
 1. 플레이어가 부딪히는 충돌 체크 : 충돌하면
- 탄환 7개 지급
- 아이템 킬
- 아이템 360도 회전
- 위아래로 왔다갔다 흔들림 구현
 */