using Unity.Properties;
using UnityEngine;
namespace MyFps
{
    //������Ʈ�� ���콺 �����͸� �ٶ󺻴�
    public class LookAtMouse : MonoBehaviour
    {
        #region Variables
        //���콺 �����Ͱ� ����Ű�� ���� ������ ��
        private Vector3 worldPosition;

        #endregion

        #region Unity Event Method
        private void Update()
        {
            //���� ������ �� ������ - Ray �̿�
            worldPosition = RayToWorld();

            //���� ������ �� �ٶ󺸱�
            transform.LookAt(worldPosition);

        }


        #endregion

        #region Custom Method
        //���� ������ �� ������ - ���콺 ��ġ���� �̿�
        private Vector3 ScreenToWorld()
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPos = Vector3.zero;
            worldPos = Camera.main.ScreenToWorldPoint(new Vector3 (mousePosition.x, mousePosition.y, 0.3f));
            return worldPos;
        }


        //���� ������ �� ������ - ���콺 ��ġ�� ��� Ray�� �̿� 
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