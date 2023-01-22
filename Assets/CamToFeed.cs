using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamToFeed : MonoBehaviour
{
    // Start is called before the first frame update
    public Material videoScreen;
    private WebCamTexture backCamera;
    public RenderTexture rt;

    // Use t$$anonymous$$s for initialization
    void Start()
    {
        backCamera = new WebCamTexture();
        videoScreen.mainTexture = backCamera;
        backCamera.Play();
    }
}
