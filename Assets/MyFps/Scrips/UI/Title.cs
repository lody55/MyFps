using MyDefence;
using System.Collections;
using UnityEngine;
namespace MyFps
{
    //Ÿ��Ʋ ���� �����ϴ� Ŭ���� : 3�� �Ŀ� �ִ�Ű ���̰� 10�� �Ŀ� ���θ޴� ����
    public class Title : MonoBehaviour
    {
        #region Variables
        [SerializeField]private SceneFader sceneFader;
        [SerializeField]private GameObject anyKey;

        private string loadToScene = "MainMenu";
        #endregion

        #region Unity Event Method
        
        private void Start()
        {

            sceneFader.FadeStart();
            //����� �÷���
            AudioManager.Instance.PlayBgm("TitleBgm");
            //�ڷ�ƾ �Լ� ����
            StartCoroutine(AnyKeyGo());

        }
        private void Update()
        {
            //�ִ�Ű�� ���� �Ŀ� �ƹ�Ű�� ������ ���θ޴� �̵�
            if(Input.anyKeyDown)
            {
                StopAllCoroutines();
                sceneFader.FadeTo(loadToScene);
                AudioManager.Instance.Stop("TitleBgm");
            }
        }
        #endregion

        #region Custom Method
        //�ڷ�ƾ �Լ� : 3�� �� �ִ�Ű 10�� �� ���θ޴�����
        IEnumerator AnyKeyGo()
        {
            anyKey.SetActive(false);
            yield return new WaitForSeconds(3f);
            anyKey.SetActive(true);
            yield return new WaitForSeconds(7f);
            AudioManager.Instance.Stop("TitleBgm");
            sceneFader.FadeTo(loadToScene);

        }
        #endregion
    }
}