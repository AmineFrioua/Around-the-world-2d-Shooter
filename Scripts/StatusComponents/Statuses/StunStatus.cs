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
        get { return (Control)statusView; }
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

    private Timer timerDuration;
    private Timer timerImmunity;

    private bool immune;

    public override void _Ready()
    {
        timerDuration = AddTimer();

        timerImmunity = AddTimer();
    }

    public void Start(float duration, float strength)
    {
        if (immune == true) return;

        timerDuration.WaitTime = duration;

        timerDuration.Start();
    }

    private Timer AddTimer()
    {
        Timer timer = new Timer();

        timer.OneShot = false;

        AddChild(timer);

        return timer;
    }
}