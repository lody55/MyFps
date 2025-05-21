using UnityEngine;
using System.Collections;
using TMPro;

namespace MyFps
{
    public class BFirstTrigger : MonoBehaviour
    {
        //첫번째 트리거 연출
        #region Variables
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject arrow;
        [SerializeField] private TextMeshProUGUI sequenceText;
        private string squence = "Looks like a weapon on that table";
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
            
            player.SetActive(false);
            sequenceText.text = squence;
            arrow.SetActive(false);
            yield return new WaitForSeconds(1f);
            arrow.SetActive(true);
            yield return new WaitForSeconds(1f);
            sequenceText.text = "";
            player.SetActive(true);
            Destroy(gameObject);
        }
        #endregion
    }
}