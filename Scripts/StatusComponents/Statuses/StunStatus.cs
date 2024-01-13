using AroundTheWorldShooter.Scripts.StatusComponents.Interfaces;
using Godot;
using Valossy.Loggers;

namespace AroundTheWorldShooter.Scripts.StatusComponents.Statuses;

[Tool]
public partial class StunStatus : Node, IStatus
{
    [Signal]
    public delegate void StunnedEventHandler();
    
    [Signal]
    public delegate void StunRecoveredEventHandler();

    /// <summary>
    /// Stun Immunity after Stun passes, default 5 sec so there is no permanent stun
    /// </summary>
    [Export()]
    public float ImmunityDuration { get; set; } = 5f;

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

        timerDuration.Timeout += TimerDurationOnTimeout;

        timerImmunity = AddTimer();

        timerImmunity.Timeout += TimerImmunityOnTimeout;
    }

    public void TimerImmunityOnTimeout()
    {
        immune = false;
    }

    public void TimerDurationOnTimeout()
    {
        timerImmunity.Start(ImmunityDuration);
        
        EmitSignal(nameof(StunRecovered));
    }

    public void Start(float duration, params float[] parameters)
    {
        if (immune == true) return;

        immune = true;

        timerDuration.Start(duration);

        EmitSignal(nameof(Stunned));
    }

    private Timer AddTimer()
    {
        Timer timer = new Timer();

        timer.OneShot = true;

        AddChild(timer);

        return timer;
    }
}