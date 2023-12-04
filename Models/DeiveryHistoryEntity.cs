namespace dotnet_core_min_api.Models;

/// <summary>
/// 包裹在物流過程中的歷史紀錄
/// </summary>
public class DeiveryHistoryEntity : DeliveryLocationEntity
{
    /// <summary>
    /// 歷史紀錄序號
    /// </summary>
    public int history_id { get; set; }

    /// <summary>
    /// 包裹日期
    /// </summary>
    public string? date { get; set; }

    /// <summary>
    /// 包裹時間
    /// </summary>
    public string? time { get; set; }

    /// <summary>
    /// 包裹狀態
    /// </summary>
    public string? status { get; set; }
}