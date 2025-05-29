using MyDefence;
using UnityEngine;
namespace MyFps
{
    public class MainMenu : MonoBehaviour
    {
        //���� �޴� ���� �����ϴ� Ŭ����
        //����
        private AudioManager audioManager;

        [SerializeField]private SceneFader fader;
        private string newGame = "MainScene01";
        private void Start()
        {
            //����
            audioManager = AudioManager.Instance;
            //�� ���۽� ���̵� �� ȿ��
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
            Debug.Log("�ε����!");
        }
        public void OptionsGo()
        {
            Debug.Log("�ɼ����� �̵�!");
        }
        public void CreditsGo()
        {
            Debug.Log("ũ���� �̵�");
        }
        public void QuitGameGo()
        {
            Debug.Log("���� ����");
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