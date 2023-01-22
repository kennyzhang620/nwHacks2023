using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
public class Hash
{
    public string hash { get; set; }
    public string algorithm { get; set; }
}

public class AssetA
{
    public string id { get; set; }
    public List<Hash> hash { get; set; }
    public string name { get; set; }
    public int size { get; set; }
    public Source source { get; set; }
    public Status status { get; set; }
    public string userId { get; set; }
    public long createdAt { get; set; }
    public VideoSpec videoSpec { get; set; }
    public string playbackId { get; set; }
    public string playbackUrl { get; set; }
    public string downloadUrl { get; set; }
}

public class Source
{
    public string type { get; set; }
}

public class Status
{
    public string phase { get; set; }
    public long updatedAt { get; set; }
}

public class VideoSpec
{
    public string format { get; set; }
    public double duration { get; set; }
}

public class UploadObj
{
    public string url;
}

public static class Game_Data 
{
    public static List<AssetA> Videos = new List<AssetA>();
}
