using MyDefence;
using UnityEngine;
namespace MyFps
{
    //Ʈ���Ű� �۵��ϸ� ���� �޴� ������
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
            //Bgm���� ��ž
            AudioManager.Instance.StopBgm();
            //������ �÷���
            sceneFader.FadeTo(sceneName);
            //Ŀ�� ���� Ǯ���ֱ�
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
        }
        #endregion
    }
}