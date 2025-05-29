using MyDefence;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
namespace MyFps
{
    //메인 2번씬 오프닝 클래스
    public class DOpenning : MonoBehaviour
    {
        #region Variables
        //플레이어 오브젝트
        public GameObject thePlayer;
        //텍스트
        public TextMeshProUGUI sequenceText;

        //페이더 객체
        public SceneFader fader;

        #endregion

        #region Unity Event Method
        private void Start()
        {
            //커서 제어
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            //배경음 플레이
            

            //시퀀스 플레이
            StartCoroutine(SequencePlay());
        }
        #endregion

        #region Custom Method
        IEnumerator SequencePlay()
        {
            PlayerInput input = thePlayer.GetComponent<PlayerInput>();
            input.enabled = false;

            //Player.SetActive(false);
            //페이드인 연출
            fader.FadeStart();

            //화면 하단에 시나리오 텍스트 빈값 초기화
            sequenceText.text = "";
            yield return new WaitForSeconds(1f);

            //TODO : Cheating
            //메인 2번씬 설정
            PlayerDataManager.Instance.weaponType = WeaponType.Pistol;
            PlayerDataManager.Instance.AmmoCount = 5;
            //배경음 플레이 시작

            //캐릭터활성화
            input.enabled = true;
        }

        #endregion
    }
}