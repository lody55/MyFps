
using UnityEngine;
namespace MyFps
{
    //���� ���� Ÿ��
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
        
        MAX_KEY     //���� ������ ����

    }


    //�÷��̾� ������ ���� Ŭ���� - �̱���(���� ������ ������ ����)
    public class PlayerDataManager : PersistanceSingleton<PlayerDataManager>
    {
        #region Variables   
        private int sceneNumber;    //�÷������� �� ��ȣ

        private float playerHealth; //�÷��̾� ü��
        
        private int ammoCount;  //���� źȯ ����

        private bool[] hasKey;  //���� ������ �������� üũ

        private float maxPlayerHealth = 20f;
        #endregion

        //����Ÿ��
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

        //���ӿ����� �÷����� �� ��ȣ
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
            //�÷��� ������ �ʱ�ȭ
            InitPlayerData();
        }
        
        #endregion

        #region Custom Method
        //ammo ���� �Լ�

        //ammo ��� �Լ�
        public bool UseAmmo(int amount)
        {
            if (ammoCount < amount)
            {
                return false;
            }
            ammoCount -= amount;
            return true;
        }
        //���� ������ ȹ�� - �Ű������� ���� Ÿ�� �޴´�
        public void GetPuzzle(PuzzleKey key)
        {
            hasKey[(int)key] = true;
            Debug.Log($"���� Ű ȹ��: {key}");
        }

        //���� ������ ���� ���� üũ
        public bool HasPuzzle(PuzzleKey key)
        {
            return hasKey[(int)key];
        }

        //�÷��̾� ������ �ʱ�ȭ
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

            //���� ������ ���� : ���� ������ ������ŭ ���� ��Ҽ� ����
            hasKey = new bool[(int)PuzzleKey.MAX_KEY];
        }


        
        #endregion
        

    }
}