using UnityEngine;
using UnityEngine.InputSystem;
namespace MyFps
{
    public class PickupTheKey : Interactive
    {
        #region Variables
        private string leftEye = "Get Key?";

        [SerializeField] private PuzzleKey puzzleKey = PuzzleKey.ROOM01_KEY;

        #endregion

        private void Start()
        {

        }
        


        #region Custom Method
        protected override void ShowActionUI()
        {
            actionUI.SetActive(true);
            actionText.text = leftEye;
        }
        protected override void DoAction()
        {
            //∆€¡Ò æ∆¿Ã≈€ »πµÊ
            PlayerDataManager.Instance.GetPuzzle(puzzleKey);

            actionUI.SetActive(false);
            extraCross.SetActive(false);

            actionText.text = "";
            Destroy(gameObject);
        }

        #endregion
    }
}
