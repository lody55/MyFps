using System.Collections;
using UnityEngine;
namespace MyFps
{
    public class JumpTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject activitySphere; 
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                StartCoroutine(Activity());
            }
        }

        IEnumerator Activity()
        {
            activitySphere.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            activitySphere.SetActive(false);
        }
    }
}