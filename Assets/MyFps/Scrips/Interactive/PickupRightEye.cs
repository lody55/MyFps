using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace MyFps
{
    public class PickupRightEye : Interactive
    {
        #region Variables
        private string RightEyeKeyAction = "Pickup RightEye";

        [SerializeField] private PuzzleKey puzzleKey = PuzzleKey.RIGHTEYE_KEY;

        //���� UI
        public GameObject puzzleUI;
        public Image puzzleImage;

        public Sprite puzzleSprite;

        //���� ���
        public TextMeshProUGUI sequenceText;
        [SerializeField] private string sequence = "You have obtained a puzzle item";
        #endregion
        private void Start()
        {

        }
        #region Custom Method



        protected override void ShowActionUI()
        {
            actionUI.SetActive(true);
            actionText.text = RightEyeKeyAction;
        }
        protected override void DoAction()
        {
            StartCoroutine(ShowRightPuzzleUI());
        }
        IEnumerator ShowRightPuzzleUI()
        {
            //���� ������ ȹ��
            PlayerDataManager.Instance.GetPuzzle(puzzleKey);

            actionUI.SetActive(false);
            extraCross.SetActive(false);

            unInteractive = true;
            yield return new WaitForSeconds(0.3f);
            //����
            puzzleUI.SetActive(true);
            puzzleImage.sprite = puzzleSprite;
            yield return new WaitForSeconds(0.5f);
            sequenceText.text = sequence;
            yield return new WaitForSeconds(1.5f);

            sequenceText.text = "";
            puzzleUI.SetActive(false);

            Destroy(gameObject);
            actionText.text = "";

        }

        #endregion
    }

}