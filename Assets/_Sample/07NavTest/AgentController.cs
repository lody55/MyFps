using UnityEngine;
using UnityEngine.AI;
namespace MySample
{
    public class AgentController : MonoBehaviour
    {
        #region Variables
        //����
        private NavMeshAgent agent;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //���� 
            agent = this.GetComponent<NavMeshAgent>();
        }
        private void Update()
        {
            //���콺 ��Ŭ���� �������� Agent �̵�
            if(Input.GetMouseButtonDown(0))
            {
                //Ŭ���� ���� ���ϱ�
                Vector3 worldPosition = RayToWorld();
                //Agent�� �̵� ��ǥ���� ����
                agent.SetDestination(worldPosition);
            }
        }
        #endregion

        #region Custom Method
        //���� ������ �� ������ - ���콺 ��ġ�� ��� Ray�� �̿� 
        private Vector3 RayToWorld()
        {
            Vector3 worldPos = Vector3.zero;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                worldPos = hit.point;
            }
            return worldPos;
        }
        #endregion
    }
}