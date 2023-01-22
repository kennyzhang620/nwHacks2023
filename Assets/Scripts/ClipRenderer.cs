using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ClipRenderer : MonoBehaviour
{
    public VideoPlayer VideoPlayer;
    public VideoClip vc;
    public int index = 1;
    // Start is called before the first frame update
    void Start()
    {
        PlayIndex(1);
    }
    public void PlayIndex(int i)
    {
        PlayStatic();
        if (Game_Data.Videos.Count > 1 && i >= 1 && i < Game_Data.Videos.Count)
        {
            try
            {
                VideoPlayer.url = Game_Data.Videos[i].downloadUrl;
                VideoPlayer.source = VideoSource.Url;
                VideoPlayer.controlledAudioTrackCount = 1;
                VideoPlayer.Play();
            }
            catch (Exception ex) { PlayStatic(); }
        }
    }

    public void PlayPrevNext(int ind)
    {

        if (index + ind > Game_Data.Videos.Count)
            index = Game_Data.Videos.Count - 1;

        else if (index + ind < 1)
            index = 1;
        else
            index += ind;

        if (Game_Data.Videos.Count > 1)
        {
            try
            {
                VideoPlayer.url = Game_Data.Videos[index].downloadUrl;
                VideoPlayer.source = VideoSource.Url;
                VideoPlayer.controlledAudioTrackCount = 1;
                VideoPlayer.Play();
            }
            catch (Exception ex) { PlayStatic(); }
        }
    }
    public void PlayStatic()
    {
        if (Game_Data.Videos.Count > 1)
        {
            VideoPlayer.url = Game_Data.Videos[Game_Data.Videos.Count - 1].downloadUrl;
            VideoPlayer.source = VideoSource.Url;
            VideoPlayer.controlledAudioTrackCount = 1;
            VideoPlayer.Play();
        }
    }
}
