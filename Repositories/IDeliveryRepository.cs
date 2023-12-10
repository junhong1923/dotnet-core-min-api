using dotnet_core_min_api.Data;
using dotnet_core_min_api.Models;

namespace dotnet_core_min_api.Repositories;

public interface IDeliveryRepository
{
    /// <summary>
    /// 透過物流編號查詢出貨狀態
    /// </summary>
    /// <param name="sno">物流編號</param>
    /// <returns>出貨狀態</returns>
    Task<DeliveryDto?> GetDeliveryData(string sno);

    /// <summary>
    /// 新增假物流資料
    /// </summary>
    /// <returns>物流編號</returns>
    Task<string> InsertFakeDeliveryData(int num);

    /// <summary>
    /// 依DeliveryId取得出貨狀態
    /// </summary>
    /// <param name="ids">DeliveryIds</param>
    /// <returns>出貨狀態</returns>
    Task<DeliveryDto> GetDeliveryDataById(List<int> ids);
    
    /// <summary>
    /// 依tracking_status產生物流狀態彙總報表
    /// </summary>
    /// <returns>物流狀態彙總報表</returns>
    Task<IEnumerable<DeliveryEntity>> Report();
}