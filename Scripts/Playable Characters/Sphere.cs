using Godot;
using AroundTheWorldShooter.Scripts.Stat_Components;
using AroundTheWorldShooter.Scripts.Stat_Components.Stats;

public partial class Sphere : CharacterBody2D
{
	[Export] public float Speed { get; set; } = 300.0f;

	[Export()] public StatList Stats { get; set; }

	private Sprite2D lazerGun = null;

	public override void _Ready()
	{
		lazerGun = GetNodeOrNull<Sprite2D>("Sprites/LazerGun");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		Vector2 direction = Input.GetVector("Left", "Right", "Up", "Down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
	
	public void OnDamage() {
		//Stats[HealthStat.StatName].AddValue(-1);
	}

	public void OnDeath()
	{
		//play animation
		
		QueueFree();// You dont really need this, what you should do here is show Death screen
	}
}

