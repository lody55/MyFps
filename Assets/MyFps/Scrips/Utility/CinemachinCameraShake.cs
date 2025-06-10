using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
namespace MyFps
{
    //�ó׸ӽ� ī�޶� ��鸲 ȿ�� ���� : �̱���
    public class CinemachinCameraShake : Singleton<CinemachinCameraShake>
    {
        #region Variables
        //����
        private CinemachineBasicMultiChannelPerlin channelPerlin;

        //��鸲 üũ
        private bool isShake = false;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //����
            channelPerlin = this.GetComponent<CinemachineBasicMultiChannelPerlin>();
        }

        private void Update()
        {
            //TODO : Cheating Test
            if(Input.GetKeyDown(KeyCode.T))
            {
                Shake(2f,2f, 0.5f);
            }
        }
        #endregion

        #region Custom Method
        //ī�޶� ����
        //amplitudeGain : ��鸲 ũ��,frequenceGain : ��鸲 �ӵ�,shakeTime : ��鸲 �ð�
        public void Shake(float amplitudeGain, float frequencyGain, float shakeTime )
        {
            //��鸲 üũ
            if (isShake) return;

            StartCoroutine(StartShake(amplitudeGain, frequencyGain, shakeTime));
        }

        IEnumerator StartShake(float amplitudeGain, float frequencyGain, float shakeTime)
        {
            isShake = true;
            //��鸲 ����
            channelPerlin.AmplitudeGain = amplitudeGain;
            channelPerlin.FrequencyGain = frequencyGain;
            //��鸲 �ð�
            yield return new WaitForSeconds(shakeTime);

            //��鸲 ����
            channelPerlin.AmplitudeGain = 0f;
            channelPerlin.FrequencyGain = 0f;
            isShake = false;
        }
        #endregion
    }
}