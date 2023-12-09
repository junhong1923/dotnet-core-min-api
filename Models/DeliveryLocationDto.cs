namespace dotnet_core_min_api.Models;

public class DeliveryLocationDto
{
    public int location_id { get; set; }

    public string title { get; set; } = null!;

    public string city { get; set; } = null!;

    public string address { get; set; } = null!;
}