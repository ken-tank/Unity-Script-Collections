using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;

public class VideoPlayerEvents : MonoBehaviour
{
    public UnityEvent onFirstFrame, onEndFrame;
    [SerializeField] float customFrame;
    public UnityEvent onCustomFrame;

    VideoPlayer videoPlayer;

    void Awake() 
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    bool once1 = true, once2 = true, once3 = true;
    void Update() 
    {
        if (videoPlayer.frame == 0 && once1)
        {
            onFirstFrame.Invoke();
            once1 = false;
        }

        if (videoPlayer.frame == ((int)videoPlayer.frameCount) - 1 && once2)
        {
            onEndFrame.Invoke();
            once2 = false;
        }

        if (videoPlayer.frame == customFrame && once3)
        {
            onEndFrame.Invoke();
            once3 = false;
        }
    }
}
