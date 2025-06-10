using MyDefence;
using System.Collections;
using System.Xml.Serialization;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;
namespace MyFps
{
    //��Ʈ�� ���� ����
    public class Intro : MonoBehaviour
    {
        #region Variables
        //���� 
        public SceneFader fader;

        public CinemachineSplineCart cart;  //���� īƮ 
        

        //�̵�
        private string loadToScene = "MainScene01";
        private bool[] isArrive;    //�̵� ����Ʈ ������ �����ߴ��� ���� üũ
        [SerializeField]private int wayPointIndex;  //���� �̵� ��ǥ ����

        SplineAutoDolly.FixedSpeed dolly;
        //����
        public Animator animator;

        public GameObject introUI;
        public GameObject theShedLight;

        [SerializeField]private string aroundTrigger = "Arround";
        #endregion
        #region Unity Event Method
        private void Start()
        {
            //�ʱ�ȭ
            isArrive = new bool[5];
            wayPointIndex = 0;
            dolly = cart.AutomaticDolly.Method as SplineAutoDolly.FixedSpeed;
            dolly.Speed = 0f;
            
            //������ 
            StartCoroutine(PlayStartSequence());
        }
        #endregion

        private void Update()
        {
            //���� ����, 1ȸ�� ����
            if(cart.SplinePosition >= wayPointIndex && isArrive[wayPointIndex] == false)
            {
                Arrive();
            }
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                GotoPlayScene();
            }
        }

        #region Custom Method
        void Arrive()
        {
            //������ ���� ����Ʈ ���� ���� üũ
            if(wayPointIndex == isArrive.Length - 1)
            {
                StartCoroutine(PlayEndSequence());
            }
            else
            {
                StartCoroutine(PlayStaySequence());
            }
            
        }
        //���� ������ : ���̵�ȿ��, �θ����Ÿ���, ī�޶� �̵� ����
        IEnumerator PlayStartSequence()
        {
            isArrive[0] = true;
            yield return new WaitForSeconds(0.2f);
            //���̵� ȿ��
            fader.FadeStart();
            //������� 
            AudioManager.Instance.PlayBgm("IntroBGM");
            yield return new WaitForSeconds(1f);
            //�ѷ�����
            animator.SetTrigger(aroundTrigger);
            yield return new WaitForSeconds(4f);
            //�̵�����
            wayPointIndex = 1;  //���� ��ǥ���� ����
            dolly.Speed = 0.05f;

        }
        //��ǥ ���� ����

        //�̵� ����Ʈ ���� ��������
        IEnumerator PlayStaySequence()
        {
            //���� üũ
            isArrive[wayPointIndex] = true;
            //�̵� ����
            dolly.Speed = 0f;
            //�ѷ�����
            animator.SetTrigger(aroundTrigger);
            yield return new WaitForSeconds(4f);

            switch (wayPointIndex)
            {
                case 1:
                    introUI.SetActive(true);
                    break;
                case 2:
                    introUI.SetActive(false);
                    break;
                case 3:
                    theShedLight.SetActive(true);
                    dolly.Speed = 0.15f;
                    wayPointIndex++; //���� ��ǥ���� ����
                    yield break;  // 3���� ��� �ٸ� �ӵ��̹Ƿ� ���⼭ ����
            }

            //���� ��ǥ���� ���� �� �̵� ����
            wayPointIndex++;
            dolly.Speed = 0.05f;
        }
        //��������
        IEnumerator PlayEndSequence()
        {
            //���θ� ����Ʈ ���� ���� ������ �̵�
            theShedLight.SetActive(false);
            //���� üũ
            isArrive[wayPointIndex] = true;
            //�̵� ����
            dolly.Speed = 0f;

            yield return new WaitForSeconds(2f);

            //����� ����
            AudioManager.Instance.StopBgm();

            //���� �� ����
            fader.FadeTo(loadToScene);
        }
        void GotoPlayScene()
        {
            StopAllCoroutines();

            AudioManager.Instance.StopBgm();

            fader.FadeTo(loadToScene);
        }
        #endregion
    }
}