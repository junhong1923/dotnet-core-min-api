namespace dotnet_core_min_api.Models;

/// <summary>
/// API 回傳 Entity
/// </summary>
public class ApiResponseEntity
{
    /// <summary>
    /// API 請求狀態 (success or error)
    /// </summary>
    public string Status { get; set; } = null!;
    
    /// <summary>
    /// API 回傳資料
    /// </summary>
    public object? Data { get; set; }

    /// <summary>
    /// API 錯誤訊息
    /// </summary>
    public object? Error { get; set; }
}