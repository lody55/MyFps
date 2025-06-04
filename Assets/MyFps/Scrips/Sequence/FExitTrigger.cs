using MyDefence;
using UnityEngine;
namespace MyFps
{
    //트리거가 작동하면 메인 메뉴 보내기
    public class FExitTrigger : MonoBehaviour
    {
        #region Variables
        private SceneFader sceneFader;
        private string sceneName = "MainMenu";
        #endregion

        #region Unity Event Method
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                SceneClear();
            }
        }


        #endregion
        #region Custom Method
        private void SceneClear()
        {
            //Bgm사운드 스탑
            AudioManager.Instance.StopBgm();
            //다음씬 플레이
            sceneFader.FadeTo(sceneName);
            //커서 제어 풀어주기
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
        }
        #endregion
    }
}