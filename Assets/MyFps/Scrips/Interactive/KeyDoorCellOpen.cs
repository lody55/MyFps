using MyFps;
using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class KeyDoorCellOpen : DoorCellOpen
{
    private string IsOpen = "IsOpen";

    

    [SerializeField] private PuzzleKey puzzleKey;

    //시나리오 텍스트
    public TextMeshProUGUI sequenceText;
    [SerializeField]private string sequence = "You need to The Key";
    protected override void DoAction()
    {

        if (PlayerDataManager.Instance.HasPuzzle(puzzleKey))
        {
            OpenedDoor();
        }
        else
        {

            StartCoroutine(LockedDoor());
        }

        
    }

    private void OpenedDoor()
    {
        //UI숨기고 문열고 충돌체 제거
        HideActionUI();
        animator.SetBool(IsOpen, true);        //문 여는 애니메이션
        this.GetComponent<BoxCollider>().enabled = false;   //충돌체 제거
        audioSource.Play();     //문여는 소리
    }
    IEnumerator LockedDoor()
    {
        unInteractive = true;
        AudioManager.Instance.Play("DoorLocked");
        //this.GetComponent<Collider>().enabled = false; 
        sequenceText.text = "";
        yield return new WaitForSeconds(1f);
        sequenceText.text = sequence;
        yield return new WaitForSeconds(2f);
        //this.GetComponent<Collider>().enabled = true;
        unInteractive = false;
        sequenceText.text = "";
    }
}
