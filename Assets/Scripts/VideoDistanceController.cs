using UnityEngine;
using UnityEngine.Video;

public class VideoDistanceController : MonoBehaviour
{
    public Transform playerTransform;       
    public MeshRenderer video;  
    private VideoPlayer videoPlayer;
    public float playDistance = 5f;         

    private bool isPlaying = false;

    private void Start()
    {
        videoPlayer = video.GetComponent<VideoPlayer>();
    }
    void Update()
    {
        if (playerTransform == null || videoPlayer == null)
            return;

        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= playDistance)
        {
            if (!isPlaying)
            {
                video.enabled = true;
                videoPlayer.Play();
                isPlaying = true;
            }
        }
        else
        {
            if (isPlaying)
            {
                videoPlayer.Pause();
                video.enabled = false;
                isPlaying = false;
            }
        }
    }
}