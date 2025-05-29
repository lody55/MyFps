using MyDefence;
using System.Collections;
using UnityEngine;
namespace MyFps
{
    //타이틀 씬을 관리하는 클래스 : 3초 후에 애니키 보이고 10초 후에 메인메뉴 가기
    public class Title : MonoBehaviour
    {
        #region Variables
        [SerializeField]private SceneFader sceneFader;
        [SerializeField]private GameObject anyKey;

        private string loadToScene = "MainMenu";
        #endregion

        #region Unity Event Method
        
        private void Start()
        {

            sceneFader.FadeStart();
            //배경음 플레이
            AudioManager.Instance.PlayBgm("TitleBgm");
            //코루틴 함수 실행
            StartCoroutine(AnyKeyGo());

        }
        private void Update()
        {
            //애니키가 보인 후에 아무키나 누르면 메인메뉴 이동
            if(Input.anyKeyDown)
            {
                StopAllCoroutines();
                sceneFader.FadeTo(loadToScene);
                AudioManager.Instance.Stop("TitleBgm");
            }
        }
        #endregion

        #region Custom Method
        //코루틴 함수 : 3초 후 애니키 10초 후 메인메뉴가기
        IEnumerator AnyKeyGo()
        {
            anyKey.SetActive(false);
            yield return new WaitForSeconds(3f);
            anyKey.SetActive(true);
            yield return new WaitForSeconds(7f);
            AudioManager.Instance.Stop("TitleBgm");
            sceneFader.FadeTo(loadToScene);

        }
        #endregion
    }
}