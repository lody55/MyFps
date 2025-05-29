using MyDefence;
using UnityEngine;
namespace MyFps
{
    public class MainMenu : MonoBehaviour
    {
        //메인 메뉴 씬을 관리하는 클래스
        //참조
        private AudioManager audioManager;

        [SerializeField]private SceneFader fader;
        private string newGame = "MainScene01";
        private void Start()
        {
            //참조
            audioManager = AudioManager.Instance;
            //씬 시작시 페이드 인 효과
            fader.FadeStart(0);
            audioManager.PlayBgm("MenuMusic");
        }
        public void NewGameGo()
        {
            audioManager.Play("MenuSelect");
            fader.FadeTo(newGame);
        }
        public void LoadGameGo()
        {
            Debug.Log("로드게임!");
        }
        public void OptionsGo()
        {
            Debug.Log("옵션으로 이동!");
        }
        public void CreditsGo()
        {
            Debug.Log("크레딧 이동");
        }
        public void QuitGameGo()
        {
            Debug.Log("게임 종료");
        }
    }
}

/*
 뉴게임 메인메뉴1번
로드게임 이버그 로드게임
옵션 쇼 옵션
크레딧 쇼 크레딧
게임종료
 */