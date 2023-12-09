namespace dotnet_core_min_api.Models;

/// <summary>
/// 包裹在物流過程中的歷史紀錄
/// </summary>
public class DeliveryDetailDto
{
    /// <summary>
    /// 歷史紀錄序號
    /// </summary>
    public int id { get; set; }

    /// <summary>
    /// 包裹日期
    /// </summary>
    public DateOnly date { get; set; }

    /// <summary>
    /// 包裹時間
    /// </summary>
    public TimeOnly time { get; set; }

    /// <summary>
    /// 包裹狀態
    /// </summary>
    public string status { get; set; }

    /// <summary>
    /// 物流所在地點序號
    /// </summary>
    public int location_id { get; set; }

    /// <summary>
    /// 物流所在地名稱
    /// </summary>
    public string location_title { get; set; }
}