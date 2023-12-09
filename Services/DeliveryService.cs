using System.Text.Json;
using dotnet_core_min_api.Repositories;
using dotnet_core_min_api.Models;

namespace dotnet_core_min_api.Services;

public class DeliveryService : IDeliveryService
{
    private readonly ILogger<DeliveryService> _logger;

    /// <summary>
    /// IDeliveryRepository
    /// </summary>
    private readonly IDeliveryRepository _deliveryRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeliveryService" /> class.
    /// </summary>
    /// <param name="logger">ILogger</param>
    /// <param name="deliveryRepository">IDeliveryRepository</param>
    public DeliveryService(ILogger<DeliveryService> logger, IDeliveryRepository deliveryRepository)
    {
        this._logger = logger;
        this._deliveryRepository = deliveryRepository;
    }
    
    /// <summary>
    /// 透過物流編號查詢出貨狀態
    /// </summary>
    /// <param name="sno">物流編號</param>
    /// <returns>出貨狀態</returns>
    public async Task<DeliveryDto?> GetDeliveryData(string? sno)
    {
        sno ??= string.Empty;
        var data = await this._deliveryRepository.GetDeliveryData(sno);

        return data;
    }
}