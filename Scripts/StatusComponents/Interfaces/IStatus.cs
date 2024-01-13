namespace AroundTheWorldShooter.Scripts.StatusComponents.Interfaces;

public interface IStatus
{
    public IStatusView StatusView { get; }
    public string StatusName { get; }
    public void Start(float duration, params float[] parameters);
}