using System;
using Godot;
using Valossy.Loggers;

namespace AroundTheWorldShooter.Scripts.Stat_Components.Stats;

public partial class HealthStat : Node, IStat
{
    public Control StatControl
    {
        get { return statControl; }
        set
        {
            if (StatView is IStatView statView)
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
        get => statView;
    }

    public int Minimum { get; set; }
    public int Maximum { get; set; }
    public int Value { get; private set; }

    private IStatView statView;

    private Control statControl;

    public override void _Ready()
    {
    }

    public void AddValue(int value)
    {
        Value = Math.Clamp(Value + value, Minimum, Maximum);
    }
}