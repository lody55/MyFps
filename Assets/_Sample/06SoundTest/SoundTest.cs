using Unity.Properties;
using UnityEngine;
namespace MySample
{
    public class SoundTest : MonoBehaviour
    {
        #region Variables
        private AudioSource audioSource;

        //Audio Souce �Ӽ�
        public AudioClip clip;
        [SerializeField]private float volume = 1f;
        [SerializeField]private float pitch = 1.0f;
        [SerializeField]private bool loop = false;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();

            //
            //SoundOnShot();
            SoundPlay();
        }

        #endregion

        #region Custom Method
        //���� �÷���
        private void SoundOnShot()
        {
            //audioSource.Play();
            audioSource.PlayOneShot(clip);
        }
        private void SoundPlay()
        {
            //Audio Souce �Ӽ� ����
            audioSource.clip = clip;

            audioSource.volume = volume;

            audioSource.pitch = pitch;

            audioSource.loop = loop;

            //���� �÷���
            audioSource.Play();
        }

        #endregion
    }
}