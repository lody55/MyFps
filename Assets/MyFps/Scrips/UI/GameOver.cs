using MyDefence;
using UnityEngine;
namespace MyFps
{
    //���ӿ���ó�� : �޴����� , �ٽ��ϱ�
    public class GameOver : MonoBehaviour
    {
        #region Variables
        public SceneFader fader;

        [SerializeField]private string loadToScene = "";
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //Ŀ�� ����
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //FadeIn ȿ��
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