using MyDefence;
using System.Collections;
using TMPro;
using UnityEngine;
namespace MyFps
{
    //플레이 씬 오프닝 연출
    public class AOpenning : MonoBehaviour
    {
        //플레이어 오브젝트
        public GameObject Player;
        //텍스트
        public TextMeshProUGUI sequenceText;

        //페이더 객체
        public SceneFader fader;
        private string sequence = "I need get out of here";

        private void Start()
        {
            StartCoroutine(StartText());
            
        }

        IEnumerator StartText()
        {
            Player.SetActive(false);
            //페이드인 연출
            fader.FadeStart(1f);
            sequenceText.text = sequence;
            
            yield return new WaitForSeconds(3f);
            Player.SetActive(true);
            sequenceText.text = "";
        }
    }
}