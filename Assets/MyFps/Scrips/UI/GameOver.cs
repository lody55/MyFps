using MyDefence;
using UnityEngine;
namespace MyFps
{
    //게임오버처리 : 메뉴가기 , 다시하기
    public class GameOver : MonoBehaviour
    {
        #region Variables
        public SceneFader fader;

        [SerializeField]private string loadToScene = "";
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //커서 관리
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //FadeIn 효과
            fader.FadeStart();
        }

        #endregion

        #region Custom Method
        public void Retry()
        {
            int nowScene = PlayerDataManager.Instance.SceneNumber;
            fader.FadeTo(nowScene);
        }

        public void Menu()
        {
            fader.FadeTo(loadToScene);
        }
        #endregion
    }
}