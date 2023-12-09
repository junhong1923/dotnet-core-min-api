using dotnet_core_min_api.Data;
using dotnet_core_min_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_core_min_api.Repositories;

public class DeliveryRepository : IDeliveryRepository
{
    /// <summary>
    /// ApiDbContext
    /// </summary>
    private readonly ApiDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeliveryRepository" /> class.
    /// </summary>
    /// <param name="dbContext">ApiDbContext</param>
    public DeliveryRepository(ApiDbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    
    /// <summary>
    /// 透過物流編號查詢出貨狀態
    /// </summary>
    /// <param name="sno">物流編號</param>
    /// <returns>出貨狀態</returns>
    public async Task<DeliveryDto?> GetDeliveryData(string sno)
    {
        var query1 =
            from history in _dbContext.history
            join location in _dbContext.location
                on history.location_id equals location.id
            where history.delivery_sno == sno
            select new
            {
                history = new HistoryEntity
                {
                    id = history.id,
                    date = history.date,
                    time = history.time,
                    status = history.status,
                    location_id = history.location_id,
                },
                location = new LocationEntity
                {
                    id = location.id,
                    title = location.title,
                    city = location.city,
                    address = location.address
                }
            };
        
        var tmpList = await query1.ToListAsync();
        if (tmpList.Any() == false) return null;
        
        var currentLocation = tmpList.OrderByDescending((item) => item.history.id).FirstOrDefault()?.location;
        var detailList = tmpList.Select(i =>
        {
            var history = i.history;

            return new DeliveryDetailDto
            {
                id = history.id,
                date = history.date,
                time = history.time,
                status = history.status,
                location_id = history.location_id,
                location_title = i.location.title
            };
        }).ToList();
        
        var query2 =
            from delivery in _dbContext.delivery
            join recipient in _dbContext.recipient
                on delivery.recipient_id equals recipient.id
            where delivery.sno == sno
            select new DeliveryDto
            {
                sno = delivery.sno,
                tracking_status = delivery.tracking_status,
                estimated_delivery = delivery.estimated_date,
                recipient = recipient,
                details = detailList,
                current_location = new DeliveryLocationDto
                {
                    location_id = currentLocation.id,
                    title = currentLocation.title,
                    city = currentLocation.city,
                    address = currentLocation.address
                }
            };

        return await query2.FirstOrDefaultAsync();
    }

    /// <summary>
    /// 依tracking_status產生物流狀態彙總報表
    /// </summary>
    /// <returns>物流狀態彙總報表</returns>
    public async Task<IEnumerable<DeliveryEntity>> Report()
    {
        var query = from delivery in _dbContext.delivery
            select delivery;

        return await query.ToListAsync();
    }
}