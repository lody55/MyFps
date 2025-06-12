using MyDefence;
using System.Collections;

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
namespace MyFps
{
    public class MainMenu : MonoBehaviour
    {
        //���� �޴� ���� �����ϴ� Ŭ����
        #region Variables
        //����
        private AudioManager audioManager;

        [SerializeField]private SceneFader fader;
        private string newGame = "Intro";

        //�޴�
        public GameObject mainMenuUI;
        public GameObject optionUI;
        public GameObject creditCanvas;

        public GameObject loadeGameButton;

        private bool isShowOptions;
        private bool isShowCredits;
        //��������
        public AudioMixer audioMixer;
        public Slider sfxSlider;
        public Slider bgmSlider;

        //���� ������
        private int sceneNumber = -1;
        #endregion
        private void Start()
        {
            //���ӵ����� �����ͼ� �ʱ�ȭ �ϱ�
            GameDataInit();
            
            //����
            audioManager = AudioManager.Instance;
            //�� ���۽� ���̵� �� ȿ��
            fader.FadeStart(0);
            //�޴� ����� �÷���
            //audioManager.PlayBgm("MenuMusic");

            //�޴� UI ����
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
            //�ɼ� ���尪 �����ͼ� ���ӿ� ����
            LoadOptions();

            //���� �÷��� ���尪 �������� : ���� ��ȣ
            //PlayerPrefs ���
            //sceneNumber = PlayerPrefs.GetInt("SceneNumber", -1);
            //FileSystem ���
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
            //�޴����� ����
            audioManager.Play("MenuSelect");
            //�ɼ� UI �����ֱ�
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
            Debug.Log("���� ����");
        }
        public void HideOption()
        {
            optionUI.SetActive(false);
            mainMenuUI.SetActive(true);
            isShowOptions = false;
        }

        //BGM ���� ����
        public void SetBgmVolume(float value)
        {
            //������ ����
            PlayerPrefs.SetFloat("Bgm", value);

            //���� ����
            audioMixer.SetFloat("Bgm", value);
            
        }
        public void SetSfxVolume(float value)
        {
            //ȿ������ ����
            PlayerPrefs.SetFloat("Sfx", value);

            //ȿ���� ����
            audioMixer.SetFloat("Sfx", value);
        }
        
        //�ɼ� ���尪 �����ͼ� ���ӿ� ����
        private void LoadOptions()
        {
            //����� ������ ��������
            float bgmVolume = PlayerPrefs.GetFloat("Bgm", 0f);
            //����� �ͼ��� ����
            SetBgmVolume(bgmVolume);
            //UI�� ����
            bgmSlider.value = bgmVolume;

            //ȿ���� ������ ��������
            float sfxVolume = PlayerPrefs.GetFloat("Sfx", 0f);
            //����� �ͼ��� ����
            SetSfxVolume(sfxVolume);
            //UI�� ����
            sfxSlider.value = sfxVolume;

            //��Ÿ....
        }
        IEnumerator ShowCreditUI()
        {
            isShowCredits = true;
            //�޴� ���� ����
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
 ������ ���θ޴�1��
�ε���� �̹��� �ε����
�ɼ� �� �ɼ�
ũ���� �� ũ����
��������
 */