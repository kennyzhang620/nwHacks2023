using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.Json;
using Newtonsoft.Json;
using TinyJson;

public class DataLoader : MonoBehaviour
{
    string persistPath;
    public bool Reader = true;
    public bool DisplayError;
    public GameObject NewComer;

    public int remotestatus = 0;

    public void GetRequest()
    {
        StartCoroutine(Download());
    }

    IEnumerator Upload()
    {
        WWWForm webForm = new WWWForm();
        TimeSpan t = DateTime.UtcNow - new DateTime(2001, 6, 20);
        // print("S: " + secondsSinceEpoch);

        int hash = (int)t.TotalSeconds;
        //   print(hash);

        webForm.AddField("", 0);

        print("Uploading...");
        UnityWebRequest www = UnityWebRequest.Post("https://infiniteblockmdns.azurewebsites.net/append", webForm);
        www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            print(www.error);
        }
        else
        {
            print("Form upload complete!");
        }
    }

    IEnumerator Delete(int uid)
    {
        // Not part of leaderboards anymore.

        WWWForm webForm = new WWWForm();
        webForm.AddField("uid", uid);

        print("Deleting...");
        UnityWebRequest www = UnityWebRequest.Post("https://infiniteblockmdns.azurewebsites.net/delete", webForm);
        www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            print(www.error);
        }
        else
        {
            print("Deletion complete!");
        }
    }

    IEnumerator Download()
    {
        print("Downloading...");
        UnityWebRequest www = UnityWebRequest.Get("https://livepeer.studio/api/asset");
        www.SetRequestHeader("Authorization", "Bearer b6debf67-87f3-45fa-a7b8-fadb4abbfef4");

        /* 
         [{"id":"ec6f79f3-129c-4889-bf5c-f533208de66e","hash":[{"hash":"092abe31b5786a4aa2590866b6b783a0","algorithm":"md5"},
        {"hash":"803d8f32769e3f36aef3245ca65f210cb6c4cfaca1c68cdc2f295fdd0c8276d5","algorithm":"sha256"}],"name":"dronebee.mp4","size":10143984,
        "source":{"type":"directUpload"},"status":{"phase":"ready","updatedAt":1674348170971},"userId":"dc5ee86b-6861-4158-9190-3109afa7e9c8",
        "createdAt":1674348114373,"videoSpec":{"format":"mp4","duration":41.642667},"playbackId":"ec6f5nvvqgkln3qf","playbackUrl":"https://lp-playback.com/hls/ec6f5nvvqgkln3qf/index.m3u8",
        "downloadUrl":"https://lp-playback.com/hls/ec6f5nvvqgkln3qf/video"}]
         
         */
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            print(www.error);
        }
        else
        {
            var json = www.downloadHandler.text;
            print(json);
            //Game_Data.Videos = JsonUtility.FromJson<List<AssetA>>(json);


            Game_Data.Videos = JSONParser.FromJson<List<AssetA>>(json);

            for (int i=0;i<Game_Data.Videos.Count;i++)
            {
                print(Game_Data.Videos[i].downloadUrl);
            }
        }
    }

    private void Start()
    {
        StartCoroutine(Download());
    }

    /*
    // Update is called once per frame
    void Update()
    {
        DisplayError = GameData.ErrorStatus;
        
        if (GameData.AccessMode == -1)
        {
            GameData.AccessMode = 0;
            EraseData();
        }
    }
    */
}