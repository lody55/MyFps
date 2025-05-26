using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;

namespace MyFps
{
    public class BFirstTrigger : MonoBehaviour
    {

        //첫번째 트리거 연출
        #region Variables

        [SerializeField] private GameObject thePlayer;
        [SerializeField] private GameObject arrow;
        [SerializeField] private TextMeshProUGUI sequenceText;
        private string squence = "Looks like a weapon on that table";

        public AudioSource line03;
        
        #endregion

        #region Unity Event Method
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Debug.Log($"other : {other.name}");
                StartCoroutine(SequencePlayer());
            }
        }
        #endregion

        #region Custom Method

        IEnumerator SequencePlayer()
        {
            PlayerInput input = thePlayer.GetComponent<PlayerInput>();
            input.enabled = false;
            //thePlayer.SetActive(false);
            sequenceText.text = squence;
            line03.Play();
            arrow.SetActive(false);
            yield return new WaitForSeconds(1f);
            arrow.SetActive(true);
            yield return new WaitForSeconds(1f);
            sequenceText.text = "";
            //thePlayer.SetActive(true);
            input.enabled = true;
            Destroy(gameObject);
        }
        #endregion
    }
}