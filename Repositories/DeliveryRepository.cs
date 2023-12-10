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
    /// 新增假物流資料
    /// </summary>
    /// <returns>物流編號</returns>
    public async Task<string> InsertFakeDeliveryData(int num)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        var recipients = await this.GetRecipients();
        var locations = await this.GetLocations();
        
        // 隨機recipient_id、location_id (從1開始)
        var randomRecipientId = new Random().Next(1, recipients.Count() + 1);
        var randomLocationId = new Random().Next(1, locations.Count() + 1);
        var sno = $"{DateTime.Now.ToString("yyyy.MM.dd-HH:mm")}_FakeSno{num+1}";

        var delivery = new DeliveryEntity
        {
            sno = sno,
            tracking_status = this.RandomTrackingStatus(),
            estimated_date = DateOnly.FromDateTime(this.RandomTime()),
            recipient_id = randomRecipientId
        };
        
        // insert delivery
        await _dbContext.delivery.AddAsync(delivery);

        // details 隨機塞1~3筆
        var randInt = new Random().Next(1, 4);
        for (var i = 1; i <= randInt; i++)
        {
            var randomTime = this.RandomTime();
            var history = new HistoryEntity
            {
                date = DateOnly.FromDateTime(randomTime),
                time = TimeOnly.FromDateTime(randomTime),
                status = this.RandomTrackingStatus(),
                location_id = randomLocationId,
                delivery_sno = sno
            };

            await _dbContext.history.AddAsync(history);
        }
        
        await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();
        
        return delivery.sno;
    }

    /// <summary>
    /// 依DeliveryId取得出貨狀態
    /// </summary>
    /// <param name="ids">DeliveryIds</param>
    /// <returns>出貨狀態</returns>
    public async Task<DeliveryDto> GetDeliveryDataById(List<int> ids)
    {
        return new DeliveryDto();
    }

    /// <summary>
    /// 取得目前所有的收件人資訊
    /// </summary>
    /// <returns>目前所有的收件人資訊</returns>
    public async Task<IEnumerable<RecipientEntity>> GetRecipients()
    {
        var query = from recipient in _dbContext.recipient
            select recipient;

        return await query.ToListAsync();
    }

    /// <summary>
    /// 取得目前所有的Locations
    /// </summary>
    /// <returns>目前所有的Locations</returns>
    public async Task<IEnumerable<LocationEntity>> GetLocations()
    {
        var query = from location in _dbContext.location
            select location;

        return await query.ToListAsync();
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
    
    /// <summary>
    /// 隨機生產時間
    /// </summary>
    /// <returns>隨機時間</returns>
    private DateTime RandomTime()
    {
        DateTime start = new DateTime(2018, 1, 1, 1, 1, 1);
        Random gen = new Random();
        int range = (DateTime.Today - start).Days;
        
        return start.AddDays(gen.Next(range));
    }

    /// <summary>
    /// 隨機生成TrackingStatus
    /// </summary>
    /// <returns>TrackingStatus</returns>
    private string RandomTrackingStatus()
    {
        var statusList = new List<string>
        {
            "Created", // 物流公司建⽴了⼀張物流單，但尚未收到包裹
            "Package Received", // 物流公司已經接收了包裹，準備進⾏後續的處理和運輸
            "In Transit", // 包裹正在運送途中。這是⼀個通⽤狀態，表⽰包裹已離開發送地並在前往⽬的地的路上 
            "Out for Delivery", // 包裹已經到達最終的配送中⼼，並正在派送到收件⼈地址
            "Delivery Attempted", // 指嘗試將包裹遞送給收件⼈，但由於某些原因（如收件⼈不在家）⽽未能成功
            "Delivered", // 包裹已成功遞送給收件⼈
            "Returned to Sender", // 如果包裹無法投遞給收件⼈，則可能被退回給發件⼈
            "Exception" // 表⽰包裹運輸過程中遇到了異常情況，如運輸途中損壞、遺失或延誤等
        };

        var randomIndex = new Random().Next(0, statusList.Count);

        return statusList.ElementAt(randomIndex);
    }
}