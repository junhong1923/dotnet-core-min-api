using Microsoft.EntityFrameworkCore;

namespace dotnet_core_min_api.Models;

/// <summary>
/// 物流包裹資訊
/// </summary>
[Keyless]
public class DeliveryDto
{
    /// <summary>
    /// 物流編號
    /// </summary>
    public string sno { get; set; } = null!;

    /// <summary>
    /// 物流狀態
    /// </summary>
    public string tracking_status { get; set; } = null!;
    
    /// <summary>
    /// 預計送達日期
    /// </summary>
    public DateOnly estimated_delivery { get; set; }

    /// <summary>
    /// 包裹歷史紀錄清單
    /// </summary>
    public List<DeliveryDetailDto> details { get; set; } = new();

    /// <summary>
    /// 收件人資訊
    /// </summary>
    public RecipientEntity recipient { get; set;} = new();

    /// <summary>
    /// 包裹目前所在位置
    /// </summary>
    public DeliveryLocationDto current_location { get; set; } = new();
}