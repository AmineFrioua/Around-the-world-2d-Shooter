using AroundTheWorldShooter.Scripts.StatusComponents.Interfaces;
using Godot;

namespace AroundTheWorldShooter.Scripts.StatusComponents.Statuses;

[Tool]
public partial class StunStatusView : Control, IStatusView
{
    public void Activate()
    {
        Show();
    }

    public void Deactivate()
    {
        Hide();
    }

    public void Update(float duration, params float[] parameters)
    {
        
    }

    public void Custom(params object[] parameters)
    {

    }
}