using MyDefence;
using UnityEngine;
using UnityEngine.InputSystem;
namespace MyFps
{
    //���� �� �޴� ���� Ŭ����
    public class PauseUI : MonoBehaviour
    {
        #region Variables
        public GameObject pauseUI;
        private bool isPause = false;
        private string loadToScene = "MainMenu";
        private SceneFader fader;
        
        #endregion

        
        #region Custom Method
        //new Input ����
        public void OnPause(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                PauseGo();
            }
        }
        //escŰ ������ UI Ȱ��ȭ, �ٽ� escŰ ������ UI ��Ȱ��ȭ - ���Ű
        public void PauseGo()
        {
            isPause = !isPause;
            pauseUI.SetActive(isPause);

            
            Time.timeScale = isPause ? 0f : 1f;

            Cursor.visible = isPause;
            Cursor.lockState = isPause ? CursorLockMode.None : CursorLockMode.Locked;
        }

        //�޴����� ��ư ȣ��
        public void MenuGo()
        {
            Time.timeScale = 1f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            fader.FadeTo(loadToScene);

            Debug.Log("���θ޴��� ���ϴ�");
        }

        public void ContinueGo()
        {
            isPause = false;
            pauseUI.SetActive(false);

            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        #endregion
    }
}