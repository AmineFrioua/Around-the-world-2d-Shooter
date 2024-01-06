using Godot;
using AroundTheWorldShooter.Scripts.Stat_Components;
using AroundTheWorldShooter.Scripts.Stat_Components.Stats;

public partial class Chachia : CharacterBody2D
{
	[Export] public float Speed = 100.0f;
	private Vector2 targetPosition; // The position of the target
	private bool hasTarget = false; // Whether a target is detected
	[Export()] public StatList Stats { get; set; }

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Vector2.Zero;
		if (hasTarget)
		{
			// Calculate the direction to the target
			Vector2 direction = (targetPosition - GlobalPosition).Normalized();

			// Move in the direction of the target
			velocity = direction * Speed;
		}
		else
		{
			// Slow down to a stop if no target is present
			// velocity = (float) Velocity.Lerp(Vector2.Zero, delta * Speed);
		}

		// Move the character
		Velocity = velocity;

		MoveAndSlide();
	}

	// Method to update target position and detection status
	public void SetTargetPosition(Vector2 position)
	{
		targetPosition = position;
		hasTarget = true;
	}

	// Method to call when the target is lost or leaves the detection area
	public void ClearTarget()
	{
		hasTarget = false;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Sphere) // Assuming 'Sphere' is your target type
		{
			SetTargetPosition(body.GlobalPosition);
		}
	}

	public void OnDamage()
	{
		Stats[nameof(HealthStat)].AddValue(-1);
	}

	public void OnDeath()
	{
		 QueueFree();
	}

	public void OnBodyExited(Node2D body)
	{
		if (body is Sphere)
		{
			ClearTarget();
		}
	}
}
