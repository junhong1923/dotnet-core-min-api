namespace dotnet_core_min_api.Models;

/// <summary>
/// 包裹目前所在位置
/// </summary>
public class LocationEntity
{
    /// <summary>
    /// 地點序號
    /// </summary>
    public int id { get; set; }

    /// <summary>
    /// 所在地點
    /// </summary>
    public string title { get; set; }

    /// <summary>
    /// 所在城市
    /// </summary>
    public string city { get; set; }

    /// <summary>
    /// 所在地址
    /// </summary>
    public string address { get; set; }
}