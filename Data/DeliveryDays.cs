namespace OrderService.Data;

public static class DeliveryDays
{
    public static List<string> NormalProducts { get; } = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
    public static List<string> ExternalProducts { get; } = new List<string> { "Monday", "Wednesday", "Friday" };
    public static List<string> TemporaryProducts { get; } = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
}
