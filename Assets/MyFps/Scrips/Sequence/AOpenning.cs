using MyDefence;
using System.Collections;
using TMPro;
using UnityEngine;
namespace MyFps
{
    //�÷��� �� ������ ����
    public class AOpenning : MonoBehaviour
    {
        //�÷��̾� ������Ʈ
        public GameObject Player;
        //�ؽ�Ʈ
        public TextMeshProUGUI sequenceText;

        //���̴� ��ü
        public SceneFader fader;
        private string sequence = "I need get out of here";

        private void Start()
        {
            StartCoroutine(StartText());
            
        }

        IEnumerator StartText()
        {
            Player.SetActive(false);
            //���̵��� ����
            fader.FadeStart(1f);
            sequenceText.text = sequence;
            
            yield return new WaitForSeconds(3f);
            Player.SetActive(true);
            sequenceText.text = "";
        }
    }
}