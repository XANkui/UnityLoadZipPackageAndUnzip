using UnityEngine;

using UnityEngine.UI;

using UnityEngine.Video;

public class URLVideoPlayer : MonoBehaviour
{

    public Button playButton;
    public Text pathText;
    private bool isHasVideo = false;
    private RawImage rawImage;

    private VideoPlayer videoPlayer;

    // Use this for initialization

    void Start()
    {

        rawImage = this.GetComponent<RawImage>();

        videoPlayer = this.GetComponent<VideoPlayer>();
        videoPlayer.url = Application.persistentDataPath + "/" + "Fuyou/Fuyou.mp4";
        //videoPlayer.url = Application.streamingAssetsPath + "/" + "Video/Fuyou.mp4"; //把视频放在StreamingAssets 中视频文件，在Android端也可以加载（Unity 不会压缩此文件夹，保持源文件大小，故所以文件较大会较站空间）
        //videoPlayer.url = @"C:\Users\xan\Music\MV\Fuyou.mp4";     // PC上本地视频地址
        pathText.text = videoPlayer.url;
        playButton.onClick.AddListener(()=> {
            isHasVideo = true;
            videoPlayer.Play();
        });
    }

    // Update is called once per frame

    void Update()
    {
        if (isHasVideo == true)
        {
            rawImage.texture = videoPlayer.texture;

        }

    }

    
}