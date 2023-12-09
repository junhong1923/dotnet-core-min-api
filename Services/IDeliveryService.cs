using dotnet_core_min_api.Models;

namespace dotnet_core_min_api.Services;

public interface IDeliveryService
{
    /// <summary>
    /// 透過物流編號查詢出貨狀態
    /// </summary>
    /// <param name="sno">物流編號</param>
    /// <returns>出貨狀態</returns>
    Task<DeliveryDto?> GetDeliveryData(string? sno);

    /// <summary>
    /// 依tracking_status產生報表
    /// </summary>
    /// <returns>物流狀態彙總報表</returns>
    Task<object> Report();
}