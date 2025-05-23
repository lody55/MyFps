using TMPro;
using UnityEngine;
namespace MySample
{
    public class MatrixTest : MonoBehaviour
    {
        public TextMeshProUGUI text;

        private Matrix4x4 matrix;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            string str = "";
            //Ʈ�������� ��Ʈ������ ��������
            matrix = transform.localToWorldMatrix;
            str += $"{matrix.m00}, {matrix.m01}, {matrix.m02}, {matrix.m03}\n";
            str += $"{matrix.m10}, {matrix.m11}, {matrix.m12}, {matrix.m13}\n";
            str += $"{matrix.m20}, {matrix.m21}, {matrix.m22}, {matrix.m23}\n";
            str += $"{matrix.m30}, {matrix.m31}, {matrix.m32}, {matrix.m33}\n";

            text.text = str;
        }
        //�Ű������� ���� �÷��̾��� ��ġ���� �Ÿ� ���ϱ�
        public float GetDistanceToPlayer(float playerX , float playerY)
        {
            float a2 = Mathf.Pow(transform.position.y - playerY,2);
            float b2 = Mathf.Pow(transform.position.x - playerX, 2);
            float distance = Mathf.Sqrt(a2 + b2);

            return distance;
        }
        public float GetDistanceToPlayer(Transform player)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            return distance;
        }    

    }
}