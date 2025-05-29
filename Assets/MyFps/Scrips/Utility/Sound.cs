using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
namespace MyFps
{
    //���� �Ӽ� �����͸� �����ϴ� ����ȭ�� ��ũ��Ʈ
    [System.Serializable]
    public class Sound
    {
        //AudionSource ������Ʈ�� �Ӽ�
        public AudioClip clip;  //���� Ŭ�� ������
        [Range(0f, 1f)]
        public float volum = 1f;     //���� ����
        [Range(0.1f, 3f)]
        public float pitch = 1f;     //���� ����ӵ�
        public bool loop;   //���� �ݺ� ���� ����
        public string name;     //�����̸� �ν�����â���� ����

        public AudioSource source;

        
    }
}