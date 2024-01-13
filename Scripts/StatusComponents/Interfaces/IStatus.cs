namespace AroundTheWorldShooter.Scripts.StatusComponents.Interfaces;

public interface IStatus
{
    public IStatusView StatusView { get; }
    public string StatName { get; }
}