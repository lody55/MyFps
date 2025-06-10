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
        private string newGame = "MainScene01";

        //�޴�
        public GameObject mainMenuUI;
        public GameObject optionUI;
        public GameObject creditCanvas;

        private bool isShowOptions;
        private bool isShowCredits;
        //��������
        public AudioMixer audioMixer;
        public Slider sfxSlider;
        public Slider bgmSlider;
        #endregion
        private void Start()
        {
            //�ɼ� ���尪 ��������
            LoadOptions();
            //����
            audioManager = AudioManager.Instance;
            //�� ���۽� ���̵� �� ȿ��
            fader.FadeStart(0);
            //�޴� ����� �÷���
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
            Debug.Log("�ε����!");
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