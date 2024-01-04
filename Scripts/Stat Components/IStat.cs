namespace AroundTheWorldShooter.Scripts.Stat_Components;

public interface IStat
{
    public IStatView StatView { get; }
    public string StatName { get; }
    public void AddValue(int value);
}