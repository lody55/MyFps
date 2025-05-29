using MyDefence;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
namespace MyFps
{
    //���� 2���� ������ Ŭ����
    public class DOpenning : MonoBehaviour
    {
        #region Variables
        //�÷��̾� ������Ʈ
        public GameObject thePlayer;
        //�ؽ�Ʈ
        public TextMeshProUGUI sequenceText;

        //���̴� ��ü
        public SceneFader fader;

        #endregion

        #region Unity Event Method
        private void Start()
        {
            //Ŀ�� ����
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            //����� �÷���
            

            //������ �÷���
            StartCoroutine(SequencePlay());
        }
        #endregion

        #region Custom Method
        IEnumerator SequencePlay()
        {
            PlayerInput input = thePlayer.GetComponent<PlayerInput>();
            input.enabled = false;

            //Player.SetActive(false);
            //���̵��� ����
            fader.FadeStart();

            //ȭ�� �ϴܿ� �ó����� �ؽ�Ʈ �� �ʱ�ȭ
            sequenceText.text = "";
            yield return new WaitForSeconds(1f);

            //TODO : Cheating
            //���� 2���� ����
            PlayerDataManager.Instance.weaponType = WeaponType.Pistol;
            PlayerDataManager.Instance.AmmoCount = 5;
            //����� �÷��� ����

            //ĳ����Ȱ��ȭ
            input.enabled = true;
        }

        #endregion
    }
}