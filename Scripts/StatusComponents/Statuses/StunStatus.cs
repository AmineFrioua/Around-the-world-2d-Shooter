using AroundTheWorldShooter.Scripts.StatusComponents.Interfaces;
using Godot;
using Valossy.Loggers;

namespace AroundTheWorldShooter.Scripts.StatusComponents.Statuses;

public partial class StunStatus : Node, IStatus
{
    [Signal]
    public delegate void DeathEventHandler();

    [Export()]
    public Control StatusControl
    {
        get
        {
            return (Control)statusView;
        }
        set
        {
            if (value is IStatusView statusView)
            {
                this.statusView = statusView;
            }
            else
            {
                Logger.Error($"{nameof(IStatusView)} does not have proper interface");
            }
        }
    }

    public IStatusView StatusView
    {
        get => this.statusView;
    }

    public string StatusName
    {
        get => nameof(StunStatus);
    }

    private IStatusView statusView;

    public void Start(float duration, float strength)
    {
    }
}