using MyDefence;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
namespace MyFps
{
    //�÷��� �� ������ ����
    public class AOpenning : MonoBehaviour
    {
        //�÷��̾� ������Ʈ
        public GameObject thePlayer;
        //�ؽ�Ʈ
        public TextMeshProUGUI sequenceText;

        //���̴� ��ü
        public SceneFader fader;
        private string sequence = "I need get out of here";
        private string sequece2 = "....Where I am?";

        //�����
        public AudioSource bgm01;

        //��� ����
        public AudioSource line01;
        public AudioSource line02;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            StartCoroutine(StartText());
            
        }

        IEnumerator StartText()
        {
            PlayerInput input = thePlayer.GetComponent<PlayerInput>();
            input.enabled = false;

            //Player.SetActive(false);
            //���̵��� ����
            fader.FadeStart(1f);
            sequenceText.text = sequece2;
            line01.Play();

            yield return new WaitForSeconds(3f);
            
            sequenceText.text = sequence;
            line02.Play();

            yield return new WaitForSeconds(3f);
            //Player.SetActive(true);
            input.enabled = true;
            sequenceText.text = "";
            bgm01.Play();
        }
    }
}