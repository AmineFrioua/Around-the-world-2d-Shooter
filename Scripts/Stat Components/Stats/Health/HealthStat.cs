using System;
using AroundTheWorldShooter.Scripts.Stat_Components.Interfaces;
using Godot;
using Valossy.Loggers;

namespace AroundTheWorldShooter.Scripts.Stat_Components.Stats.Health;

[GlobalClass]
public partial class HealthStat : Node, IStat
{
	[Signal]
	public delegate void DeathEventHandler();

	[Export()]
	public Control StatControl
	{
		get { return (Control)statView; }
		set
		{
			if (value is IStatView statView)
			{
				this.statView = statView;
			}
			else
			{
				Logger.Error($"{nameof(IStatView)} does not have proper interface");
			}
		}
	}

	public IStatView StatView
	{
		get => this.statView;
	}

	public string StatName
	{
		get => nameof(HealthStat);
	}

	private IStatView statView;

	public void AddValue(int value)
	{
		StatView.Value = Math.Clamp(StatView.Value + value, StatView.MinValue, StatView.MaxValue);

		if (StatView.Value <= StatView.MinValue)
		{
			this.EmitSignal(nameof(Death));
		}
	}
}
