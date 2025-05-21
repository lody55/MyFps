using System.Collections;
using UnityEngine;
namespace MyFps
{

    public class CJumpScareTrigger : MonoBehaviour
    {
        //문열리는 애니메이션
        [SerializeField]private Animator animator;
        //적 등장 BGM
        [SerializeField] private AudioSource jumpScare;
        //문열리는 사운드
        [SerializeField] private AudioSource doorBang;
        //적 오브젝트
        [SerializeField] private GameObject enemy;

        private string isOpen = "IsOpen";
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(SequencePlayer());
            }
        }

        IEnumerator SequencePlayer()
        {
            
            animator.SetBool(isOpen, true);
            doorBang.Play();
            //적등장
            enemy.SetActive(true);
            yield return new WaitForSeconds(1f);
            
            //적 등장 사운드
            jumpScare.Play();

            //로봇의 상태가 걷기 상태로 변경
            Robot robot = enemy.GetComponent<Robot>();
            if(robot)
            {
                robot.ChangeState(RobotState.R_Walk);
            }
        }

    }
}