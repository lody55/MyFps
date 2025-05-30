using System.Runtime.InteropServices;
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
        
        MAX_KEY     //���� ������ ����
    }


    //�÷��̾� ������ ���� Ŭ���� - �̱���(���� ������ ������ ����)
    public class PlayerDataManager : PersistanceSingleton<PlayerDataManager>
    {
        #region Variables
        
        private int ammoCount;

        private bool[] hasKey;  //���� ������ �������� üũ
 
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
        #endregion

        #region Unity Event Method
        private void Start()
        {
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
        private void InitPlayerData()
        {
            ammoCount = 0;
            weaponType = WeaponType.None;

            //���� ������ ���� : ���� ������ ������ŭ ���� ��Ҽ� ����
            hasKey = new bool[(int)PuzzleKey.MAX_KEY];
        }


        
        #endregion
        

    }
}