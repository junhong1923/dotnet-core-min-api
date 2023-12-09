namespace dotnet_core_min_api.Data;

public class DeliveryEntity
{
    public int id { get; set; }

    public string sno { get; set; }

    public string tracking_status { get; set; }

    public DateOnly estimated_date { get; set; }

    public int recipient_id { get; set; }
}