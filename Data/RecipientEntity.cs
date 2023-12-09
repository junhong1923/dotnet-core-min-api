namespace dotnet_core_min_api.Models;

/// <summary>
/// 收件人資訊
/// </summary>
public class RecipientEntity
{
    /// <summary>
    /// recipient_id
    /// </summary>
    public int id { get; set; }

    /// <summary>
    /// 收件人名字
    /// </summary>
    public string name { get; set; }

    /// <summary>
    /// 收件人地址
    /// </summary>
    public string address { get; set; }

    /// <summary>
    /// 收件人電話
    /// </summary>
    public string phone { get; set; }
}