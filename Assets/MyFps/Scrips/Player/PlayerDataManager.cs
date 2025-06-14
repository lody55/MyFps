
using UnityEngine;
namespace MyFps
{
    //장착 무기 타입
    public enum WeaponType
    {
        None,
        Pistol,

    }
    public enum PuzzleKey
    {
        ROOM01_KEY,
        LEFTEYE_KEY,
        RIGHTEYE_KEY,
        
        MAX_KEY     //퍼즐 아이템 갯수

    }


    //플레이어 데이터 관리 클래스 - 싱글톤(다음 씬에서 데이터 보존)
    public class PlayerDataManager : PersistanceSingleton<PlayerDataManager>
    {
        #region Variables   
        private int sceneNumber;    //플레이중인 씬 번호

        private float playerHealth; //플레이어 체력
        
        private int ammoCount;  //소지 탄환 갯수

        private bool[] hasKey;  //퍼즐 아이템 소지여부 체크

        private float maxPlayerHealth = 20f;
        #endregion

        //무기타입
        #region Property
        public WeaponType weaponType { get; set; }
        public int AmmoCount
        {
            get { return ammoCount; }
            set
            {
                ammoCount = value;
            }

        }

        public int SceneNumber
        {
            get { return sceneNumber; }
            set { sceneNumber = value; }
        }
        public float PlayerHealth
        {
            get { return playerHealth; }
            set { playerHealth = value; }
        }

        //게임오버시 플레이한 씬 번호
        public int NowScene
        {
            get;
            set;
        }

        #endregion

        #region Unity Event Method
        protected override void Awake()
        {
            base.Awake();
            //플레이 데이터 초기화
            InitPlayerData();
        }
        
        #endregion

        #region Custom Method
        //ammo 저축 함수

        //ammo 사용 함수
        public bool UseAmmo(int amount)
        {
            if (ammoCount < amount)
            {
                return false;
            }
            ammoCount -= amount;
            return true;
        }
        //퍼즐 아이템 획득 - 매개변수로 퍼즐 타입 받는다
        public void GetPuzzle(PuzzleKey key)
        {
            hasKey[(int)key] = true;
            Debug.Log($"퍼즐 키 획득: {key}");
        }

        //퍼즐 아이템 소지 여부 체크
        public bool HasPuzzle(PuzzleKey key)
        {
            return hasKey[(int)key];
        }

        //플레이어 데이터 초기화
        public void InitPlayerData(PlayData pData = null)
        {
            if (pData != null)
            {
                sceneNumber = pData.sceneNumber;
                ammoCount = pData.ammoCount;
                playerHealth = pData.playerHealth;
            }
            else
            {
                sceneNumber = -1;
                ammoCount = 0;
                playerHealth = maxPlayerHealth;
            }
            
            weaponType = WeaponType.None;

            //퍼즐 아이템 설정 : 퍼즐 아이템 갯수만큼 불형 요소수 생성
            hasKey = new bool[(int)PuzzleKey.MAX_KEY];
        }


        
        #endregion
        

    }
}