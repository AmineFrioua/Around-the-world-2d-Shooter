using AroundTheWorldShooter.Scripts.Stat_Components.Interfaces;
namespace AroundTheWorldShooter.Scripts.Stat_Components.Interfaces;

public interface IStat
{
	public IStatView StatView { get; }
	public string StatName { get; }
	public void AddValue(int value);
}
