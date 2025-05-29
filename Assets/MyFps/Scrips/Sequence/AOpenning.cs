using MyDefence;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
namespace MyFps
{
    //플레이 씬 오프닝 연출
    public class AOpenning : MonoBehaviour
    {
        //플레이어 오브젝트
        public GameObject thePlayer;
        //텍스트
        public TextMeshProUGUI sequenceText;

        //페이더 객체
        public SceneFader fader;
        private string sequence = "I need get out of here";
        private string sequece2 = "....Where I am?";

        //배경음
        public AudioSource bgm01;

        //대사 음성
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
            //페이드인 연출
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