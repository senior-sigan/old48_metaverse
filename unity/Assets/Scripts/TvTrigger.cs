using UnityEngine;
using UnityEngine.Video;

public class TvTrigger : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            videoPlayer.Play();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            videoPlayer.Pause();
        }
    }
}