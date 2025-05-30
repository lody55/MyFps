using MyFps;
using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class KeyDoorCellOpen : DoorCellOpen
{
    private string IsOpen = "IsOpen";

    

    [SerializeField] private PuzzleKey puzzleKey;

    //�ó����� �ؽ�Ʈ
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
        //UI����� ������ �浹ü ����
        HideActionUI();
        animator.SetBool(IsOpen, true);        //�� ���� �ִϸ��̼�
        this.GetComponent<BoxCollider>().enabled = false;   //�浹ü ����
        audioSource.Play();     //������ �Ҹ�
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
