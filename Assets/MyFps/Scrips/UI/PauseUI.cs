using MyDefence;
using UnityEngine;
using UnityEngine.InputSystem;
namespace MyFps
{
    //게임 중 메뉴 관리 클래스
    public class PauseUI : MonoBehaviour
    {
        #region Variables
        public GameObject pauseUI;
        private bool isPause = false;
        private string loadToScene = "MainMenu";
        private SceneFader fader;
        
        #endregion

        
        #region Custom Method
        //new Input 연결
        public void OnPause(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                PauseGo();
            }
        }
        //esc키 누르면 UI 활성화, 다시 esc키 누르면 UI 비활성화 - 토글키
        public void PauseGo()
        {
            isPause = !isPause;
            pauseUI.SetActive(isPause);

            
            Time.timeScale = isPause ? 0f : 1f;

            Cursor.visible = isPause;
            Cursor.lockState = isPause ? CursorLockMode.None : CursorLockMode.Locked;
        }

        //메뉴가기 버튼 호출
        public void MenuGo()
        {
            Time.timeScale = 1f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            fader.FadeTo(loadToScene);

            Debug.Log("메인메뉴로 갑니다");
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