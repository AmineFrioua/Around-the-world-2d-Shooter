namespace AroundTheWorldShooter.Scripts.Stat_Components;

public interface IStatView
{
    public double MinValue { get; set; }
    public double Value { get; set; }
    public double MaxValue { get; set; }
}