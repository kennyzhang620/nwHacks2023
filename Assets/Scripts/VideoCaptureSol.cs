using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Windows.WebCam;
using UnityEngine.Networking;
using System.Text;
using TinyJson;
using System.IO;

public class VideoCaptureSol : MonoBehaviour
{
    static readonly float MaxRecordingTime = 5.0f;

    VideoCapture m_VideoCapture = null;
    float m_stopRecordingTimer = float.MaxValue;

    public string UploadFilePath;
    public bool test = false;
    
    IEnumerator Upload(string filename)
    {
        WWWForm webForm = new WWWForm();

        print("Uploading...");

        // convert our class to json
        string JsonData = "{\"name\": \"test\"}";
            

        // instance of unity web request
        UnityWebRequest www = UnityWebRequest.Post("https://livepeer.studio/api/asset/request-upload", "POST");

        // setup upload/download headers (this is what sets the json body)
        byte[] bodyRaw = Encoding.UTF8.GetBytes(JsonData);
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        // set your headers
        www.SetRequestHeader("Authorization", "Bearer " + "0488e0b2-7284-42cd-ad30-64a49b924d6c");
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            print(www.error);
        }
        else
        {
            print("---> " + www.downloadHandler.text);
            var json = www.downloadHandler.text;
            UploadObj tx = JSONParser.FromJson<UploadObj>(json);

            print("url " + tx.url);

            UnityWebRequest www2 = UnityWebRequest.Put(tx.url, File.ReadAllBytes(filename));

            // setup upload/download headers (this is what sets the json body)
            // set your headers
            www2.SetRequestHeader("Authorization", "Bearer " + "0488e0b2-7284-42cd-ad30-64a49b924d6c");
            www2.SetRequestHeader("Content-Type", "video/mp4");

            yield return www2.SendWebRequest();

            if (www2.isNetworkError || www2.isHttpError)
            {
                print(www2.error);
            }
            else
            {
                print("Success!");
            }
        }

    }


    // Use this for initialization
    void Start()
    {
       // print("test: " + Application.persistentDataPath+"/test.mp4");
    }

    void Update()
    {

        if (test)
        {
            StartVideoCaptureTest();
          //  StartCoroutine(Upload(Application.persistentDataPath + "/test.mp4"));
            test = false;
        }
        if (m_VideoCapture == null || !m_VideoCapture.IsRecording)
        {
            return;
        }

        if (Time.time > m_stopRecordingTimer)
        {
            m_VideoCapture.StopRecordingAsync(OnStoppedRecordingVideo);
        }
    }

    void StartVideoCaptureTest()
    {
        print("Start! " + Application.persistentDataPath);
        Resolution cameraResolution = VideoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
        Debug.Log(cameraResolution);

        float cameraFramerate = VideoCapture.GetSupportedFrameRatesForResolution(cameraResolution).OrderByDescending((fps) => fps).First();
        Debug.Log(cameraFramerate);

        VideoCapture.CreateAsync(false, delegate (VideoCapture videoCapture)
        {
            if (videoCapture != null)
            {
                m_VideoCapture = videoCapture;
                Debug.Log("Created VideoCapture Instance!");

                CameraParameters cameraParameters = new CameraParameters();
                cameraParameters.hologramOpacity = 0.0f;
                cameraParameters.frameRate = cameraFramerate;
                cameraParameters.cameraResolutionWidth = cameraResolution.width;
                cameraParameters.cameraResolutionHeight = cameraResolution.height;
                cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

                m_VideoCapture.StartVideoModeAsync(cameraParameters,
                    VideoCapture.AudioState.ApplicationAndMicAudio,
                    OnStartedVideoCaptureMode);
            }
            else
            {
                Debug.LogError("Failed to create VideoCapture Instance!");
            }
        });
    }

    void OnStartedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Started Video Capture Mode!");
        string timeStamp = Time.time.ToString().Replace(".", "").Replace(":", "");
        string filename = string.Format("TestVideo_{0}.mp4", timeStamp);
        string filepath = System.IO.Path.Combine(Application.persistentDataPath, filename);
        filepath = filepath.Replace("/", @"\");
        UploadFilePath = filepath;
        m_VideoCapture.StartRecordingAsync(filepath, OnStartedRecordingVideo);
    }

    void OnStoppedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Stopped Video Capture Mode!");

        StartCoroutine(Upload(UploadFilePath));
    }

    void OnStartedRecordingVideo(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Started Recording Video!");
        m_stopRecordingTimer = Time.time + MaxRecordingTime;
    }

    void OnStoppedRecordingVideo(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Stopped Recording Video!");
        m_VideoCapture.StopVideoModeAsync(OnStoppedVideoCaptureMode);
    }
}
