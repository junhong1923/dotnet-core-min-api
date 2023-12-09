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
