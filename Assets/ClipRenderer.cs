using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ClipRenderer : MonoBehaviour
{
    public VideoPlayer VideoPlayer;
    public VideoClip vc;
    public int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        PlayIndex(0);
    }
    public void PlayIndex(int i)
    {
        PlayStatic();
        if (Game_Data.Videos.Count > 0 && i >= 0 && i < Game_Data.Videos.Count)
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

        if (index + ind < 0)
            index = 0;

        if (Game_Data.Videos.Count > 0)
        {
            try
            {
                VideoPlayer.url = Game_Data.Videos[index += ind].downloadUrl;
                VideoPlayer.source = VideoSource.Url;
                VideoPlayer.controlledAudioTrackCount = 1;
                VideoPlayer.Play();
            }
            catch (Exception ex) { PlayStatic(); }
        }
    }
    public void PlayStatic()
    {
        if (Game_Data.Videos.Count > 0)
        {
            VideoPlayer.url = Game_Data.Videos[Game_Data.Videos.Count - 1].downloadUrl;
            VideoPlayer.source = VideoSource.Url;
            VideoPlayer.controlledAudioTrackCount = 1;
            VideoPlayer.Play();
        }
    }
}
