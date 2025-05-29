using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
namespace MyFps
{
    //사운드 속성 데이터를 관리하는 직렬화된 스크립트
    [System.Serializable]
    public class Sound
    {
        //AudionSource 컴포넌트의 속성
        public AudioClip clip;  //사운드 클립 리스소
        [Range(0f, 1f)]
        public float volum = 1f;     //사운드 볼륨
        [Range(0.1f, 3f)]
        public float pitch = 1f;     //사운드 재생속도
        public bool loop;   //사운드 반복 실행 여부
        public string name;     //사운드이름 인스펙터창에서 지정

        public AudioSource source;

        
    }
}