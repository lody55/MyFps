using Unity.VisualScripting;
using UnityEngine;
namespace MyFps
{
    //���带 �����ϴ� Ŭ����
    public class AudioManager : Singleton<AudioManager>
    {
        #region Variables
        //���� ������ ���
        public Sound[] sounds;

        //����� �̸�
        private string bgmSound = "";


        #endregion

        private void  Awake()
        {
            base.Awake();

            //���� ����
            foreach (var s in sounds)
            {
                //AudioSource ������Ʈ �߰�
                s.source = this.gameObject.AddComponent<AudioSource>();

                //AudioSource ������Ʈ ������ ����
                s.source.clip = s.clip;
                s.source.volume = s.volum;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;

                s.source.playOnAwake = false;
            }
        }

        #region Custom Method
        //���� �÷���
        public void Play(string name)
        {
            Sound sound = null;
            //���� ��Ͽ��� �̸����� ���� ���� ã��
            foreach (var s in sounds)
            {
                if(s.name == name)
                {
                    sound = s;
                    break;
                }
            }
            //ã�Ҵ��� üũ
            if(sound == null)
            {
                Debug.Log("Play / Cannot Find"+name+"Sound");
                return;
            }
            sound.source.Play();
        }

        //���� ����
        public void Stop(string name)
        {
            Sound sound = null;
            //���� ��Ͽ��� �̸����� ���� ���� ã��
            foreach (var s in sounds)
            {
                if (s.name == name)
                {
                    sound = s;
                    break;
                }
            }
            //ã�Ҵ��� üũ
            if (sound == null)
            {
                Debug.Log("Stop / Cannot Find" + name + "Sound");
                return;
            }
            sound.source.Stop();
        }
        
        //����� �÷���
        public void PlayBgm(string name)
        {
            //����� �̸� üũ
            if (bgmSound == name) return;

            //���� ����ǰ� �ִ� ����� ��ž
            Stop(bgmSound);

            //����� �÷���
            Sound sound = null;
            //���� ��Ͽ��� �̸����� ���� ���� ã��
            foreach (var s in sounds)
            {
                if (s.name == name)
                {
                    sound = s;
                    //����� �̸� ����
                    bgmSound = name;
                    break;
                }

            }
            //ã�Ҵ��� üũ
            if (sound == null)
            {
                Debug.Log("Cannot Find" + name + "Sound");
                return;
            }
            sound.source.Play();
        }
        public void StopBgm()
        {
            //����� ����
            Stop(bgmSound);
        }
        #endregion
    }
}