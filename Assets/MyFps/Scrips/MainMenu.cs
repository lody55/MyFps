using MyDefence;
using System.Collections;

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
namespace MyFps
{
    public class MainMenu : MonoBehaviour
    {
        //메인 메뉴 씬을 관리하는 클래스
        #region Variables
        //참조
        private AudioManager audioManager;

        [SerializeField]private SceneFader fader;
        private string newGame = "Intro";

        //메뉴
        public GameObject mainMenuUI;
        public GameObject optionUI;
        public GameObject creditCanvas;

        public GameObject loadeGameButton;

        private bool isShowOptions;
        private bool isShowCredits;
        //볼륨조절
        public AudioMixer audioMixer;
        public Slider sfxSlider;
        public Slider bgmSlider;

        //게임 데이터
        private int sceneNumber = -1;
        #endregion
        private void Start()
        {
            //게임데이터 가져와서 초기화 하기
            GameDataInit();
            
            //참조
            audioManager = AudioManager.Instance;
            //씬 시작시 페이드 인 효과
            fader.FadeStart(0);
            //메뉴 배경음 플레이
            //audioManager.PlayBgm("MenuMusic");

            //메뉴 UI 세팅
            if(sceneNumber >= 0)
            {
                loadeGameButton.SetActive(true);
            }
            else
            {
                loadeGameButton.SetActive(false);
            }
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(isShowOptions)
                {
                    HideOption();
                }
                else if(isShowCredits)
                {
                    HideCredits();
                }
            }
        }
        private void GameDataInit()
        {
            //옵션 저장값 가져와서 게임에 적용
            LoadOptions();

            //게임 플레이 저장값 가져오기 : 빌드 번호
            //PlayerPrefs 모드
            //sceneNumber = PlayerPrefs.GetInt("SceneNumber", -1);
            //FileSystem 모드
            PlayData playData = SaveLoad.LoadData();
            PlayerDataManager.Instance.InitPlayerData(playData);
            sceneNumber = PlayerDataManager.Instance.SceneNumber;
            
        }
        public void NewGameGo()
        {
            audioManager.StopBgm();
            audioManager.Play("MenuSelect");
            fader.FadeTo(newGame);
        }
        public void LoadGameGo()
        {
            audioManager.Play("MenuSelect");
            audioManager.StopBgm();
            fader.FadeTo(sceneNumber);
        }
        public void OptionsGo()
        {
            isShowOptions = true;
            //메뉴선택 사운드
            audioManager.Play("MenuSelect");
            //옵션 UI 보여주기
            mainMenuUI.SetActive(false);
            optionUI.SetActive(true);
        }
        public void CreditsGo()
        {
            StartCoroutine(ShowCreditUI());
            
        }
        public void QuitGameGo()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("게임 종료");
        }
        public void HideOption()
        {
            optionUI.SetActive(false);
            mainMenuUI.SetActive(true);
            isShowOptions = false;
        }

        //BGM 볼륨 조절
        public void SetBgmVolume(float value)
        {
            //볼륨값 저장
            PlayerPrefs.SetFloat("Bgm", value);

            //볼륨 조절
            audioMixer.SetFloat("Bgm", value);
            
        }
        public void SetSfxVolume(float value)
        {
            //효과음값 저장
            PlayerPrefs.SetFloat("Sfx", value);

            //효과음 조절
            audioMixer.SetFloat("Sfx", value);
        }
        
        //옵션 저장값 가져와서 게임에 적용
        private void LoadOptions()
        {
            //배경음 볼륨값 가져오기
            float bgmVolume = PlayerPrefs.GetFloat("Bgm", 0f);
            //오디오 믹서에 적용
            SetBgmVolume(bgmVolume);
            //UI에 적용
            bgmSlider.value = bgmVolume;

            //효과음 볼륨값 가져오기
            float sfxVolume = PlayerPrefs.GetFloat("Sfx", 0f);
            //오디오 믹서에 적용
            SetSfxVolume(sfxVolume);
            //UI에 적용
            sfxSlider.value = sfxVolume;

            //기타....
        }
        IEnumerator ShowCreditUI()
        {
            isShowCredits = true;
            //메뉴 선택 사운드
            audioManager.Play("MenuSelect");

            creditCanvas.SetActive(true);

            mainMenuUI.SetActive(false);
            yield return new WaitForSeconds(7f);

            HideCredits();
        }
        public void HideCredits()
        {
            creditCanvas.SetActive(false);
            mainMenuUI.SetActive(true);
            isShowCredits = false;
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