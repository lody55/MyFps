using Unity.Properties;
using UnityEngine;
namespace MyFps
{
    //오브젝트가 마우스 포인터를 바라본다
    public class LookAtMouse : MonoBehaviour
    {
        #region Variables
        //마우스 포인터가 가리키는 월드 포지션 값
        private Vector3 worldPosition;

        #endregion

        #region Unity Event Method
        private void Update()
        {
            //월드 포지션 값 얻어오기 - Ray 이용
            worldPosition = RayToWorld();

            //월드 포지션 값 바라보기
            transform.LookAt(worldPosition);

        }


        #endregion

        #region Custom Method
        //월드 포지션 값 얻어오기 - 마우스 위치값을 이용
        private Vector3 ScreenToWorld()
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPos = Vector3.zero;
            worldPos = Camera.main.ScreenToWorldPoint(new Vector3 (mousePosition.x, mousePosition.y, 0.3f));
            return worldPos;
        }


        //월드 포지션 값 얻어오기 - 마우스 위치에 쏘는 Ray값 이용 
        private Vector3 RayToWorld()
        {
            Vector3 worldPos = Vector3.zero;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                worldPos = hit.point;
            }
            return worldPos;
        }
        #endregion
    }
}