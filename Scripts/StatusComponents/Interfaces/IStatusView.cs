namespace AroundTheWorldShooter.Scripts.StatusComponents.Interfaces;

public interface IStatusView
{
    void Activate();
    void Deactivate();
    void Update(float duration, params float[] parameters);
}