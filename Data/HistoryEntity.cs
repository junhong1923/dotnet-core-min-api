namespace dotnet_core_min_api.Data;

public class HistoryEntity
{
    public int id { get; set; }

    public DateOnly date { get; set; }

    public TimeOnly time { get; set; }

    public string status { get; set; }
    
    public int location_id { get; set; }

    public string delivery_sno { get; set; }
}