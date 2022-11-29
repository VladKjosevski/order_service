namespace OrderService.Data;

public static class DaysInAdvanceToOrder
{
    public static int NormalProducts { get; } = 2;
    public static int ExternalProducts { get; } = 5;
    public static int TemporaryProducts { get; } = 1;
}
