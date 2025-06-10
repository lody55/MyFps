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
        private string newGame = "MainScene01";

        //메뉴
        public GameObject mainMenuUI;
        public GameObject optionUI;
        public GameObject creditCanvas;

        private bool isShowOptions;
        private bool isShowCredits;
        //볼륨조절
        public AudioMixer audioMixer;
        public Slider sfxSlider;
        public Slider bgmSlider;
        #endregion
        private void Start()
        {
            //옵션 저장값 가져오기
            LoadOptions();
            //참조
            audioManager = AudioManager.Instance;
            //씬 시작시 페이드 인 효과
            fader.FadeStart(0);
            //메뉴 배경음 플레이
            //audioManager.PlayBgm("MenuMusic");
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