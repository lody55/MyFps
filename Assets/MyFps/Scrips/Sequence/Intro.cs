using MyDefence;
using System.Collections;
using System.Xml.Serialization;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;
namespace MyFps
{
    //인트로 연출 구현
    public class Intro : MonoBehaviour
    {
        #region Variables
        //참조 
        public SceneFader fader;

        public CinemachineSplineCart cart;  //돌리 카트 
        

        //이동
        private string loadToScene = "MainScene01";
        private bool[] isArrive;    //이동 포인트 지점에 도착했는지 여부 체크
        [SerializeField]private int wayPointIndex;  //다음 이동 목표 지점

        SplineAutoDolly.FixedSpeed dolly;
        //연출
        public Animator animator;

        public GameObject introUI;
        public GameObject theShedLight;

        [SerializeField]private string aroundTrigger = "Arround";
        #endregion
        #region Unity Event Method
        private void Start()
        {
            //초기화
            isArrive = new bool[5];
            wayPointIndex = 0;
            dolly = cart.AutomaticDolly.Method as SplineAutoDolly.FixedSpeed;
            dolly.Speed = 0f;
            
            //시퀀스 
            StartCoroutine(PlayStartSequence());
        }
        #endregion

        private void Update()
        {
            //도착 판정, 1회만 판정
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
            //마지막 엔드 포인트 지점 도착 체크
            if(wayPointIndex == isArrive.Length - 1)
            {
                StartCoroutine(PlayEndSequence());
            }
            else
            {
                StartCoroutine(PlayStaySequence());
            }
            
        }
        //시작 시퀀스 : 페이드효과, 두리번거리기, 카메라 이동 시작
        IEnumerator PlayStartSequence()
        {
            isArrive[0] = true;
            yield return new WaitForSeconds(0.2f);
            //페이드 효과
            fader.FadeStart();
            //배경음악 
            AudioManager.Instance.PlayBgm("IntroBGM");
            yield return new WaitForSeconds(1f);
            //둘러보기
            animator.SetTrigger(aroundTrigger);
            yield return new WaitForSeconds(4f);
            //이동시작
            wayPointIndex = 1;  //다음 목표지점 설정
            dolly.Speed = 0.05f;

        }
        //목표 지점 도착

        //이동 포인트 지점 도착판정
        IEnumerator PlayStaySequence()
        {
            //도착 체크
            isArrive[wayPointIndex] = true;
            //이동 멈춤
            dolly.Speed = 0f;
            //둘러보기
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
                    wayPointIndex++; //다음 목표지점 설정
                    yield break;  // 3번의 경우 다른 속도이므로 여기서 종료
            }

            //다음 목표지점 설정 및 이동 시작
            wayPointIndex++;
            dolly.Speed = 0.05f;
        }
        //최종지점
        IEnumerator PlayEndSequence()
        {
            //오두막 라이트 끄고 다음 씬으로 이동
            theShedLight.SetActive(false);
            //도착 체크
            isArrive[wayPointIndex] = true;
            //이동 멈춤
            dolly.Speed = 0f;

            yield return new WaitForSeconds(2f);

            //배경음 종료
            AudioManager.Instance.StopBgm();

            //다음 씬 가기
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