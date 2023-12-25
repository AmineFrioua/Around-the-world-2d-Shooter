
namespace AroundTheWorldShooter.Scripts.Stat_Components;

public interface IStat
{
    public IStatView StatView { get;}
    public int Minimum { get; set; }
    public int Maximum { get; set; }
    public int Value { get;}
    public void AddValue(int value);
}