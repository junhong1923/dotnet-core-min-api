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

    /// <summary>
    /// 依tracking_status產生報表
    /// </summary>
    /// <returns>物流狀態彙總報表</returns>
    public async Task<object> Report()
    {
        var data = await this._deliveryRepository.Report();
        var result = new
        {
            created_at = DateTime.UtcNow,
            trackingSummary = data
                .GroupBy(i => i.tracking_status)
                .ToDictionary(x => x.Key, y => y.Count())
        };
        
        _logger.LogInformation(JsonSerializer.Serialize(result));
        
        return result;
    }
}