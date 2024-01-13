using Godot;
using System;

public partial class Bullet : Area2D
{
	public AnimatedSprite2D BulletAnimation;
	public enum BulletTypesLists {
		charge,
		primary,
		secondary
	}
	private BulletTypesLists bulletType;
	public BulletTypesLists BulletType {get {return bulletType;}  set
	{
		bulletType= value;

	}}

	public Vector2 Velocity;
	[Export] public float Speed = 10;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LookAt(GetGlobalMousePosition());

		BulletAnimation = GetNode<AnimatedSprite2D>("Bullet");

		switch (BulletType)
			{
				case BulletTypesLists.charge:
					BulletAnimation.Play("charge");
					break;
				case BulletTypesLists.secondary:
					BulletAnimation.Play("secondary");
					break;
				case BulletTypesLists.primary:
					BulletAnimation.Play("primary");
					break;
			}
	}

	// Called every frame. 'delta' is the elapsed time since                                                                                                   the previous frame.
	public override void _Process(double delta)
	{
		Position += Velocity * Speed * (float)delta;
	}

	public void _on_body_entered(Node2D body)
	{
		BulletAnimation.AnimationFinished += OnAnimationFinished;

		Chachia enemy = body as Chachia;

		if (enemy != null)
		{
			switch (BulletType)
			{
				case BulletTypesLists.charge:
					enemy.OnDamage();
					break;
				case BulletTypesLists.secondary:
					//do something
					break;
				case BulletTypesLists.primary:
					enemy.OnDamage();
					break;
			}

		}

		BulletAnimation.Play("collision");
	}

	public void OnAnimationFinished()
	{
		if (BulletAnimation.Animation == "collision")
		{
			Speed = 0;
			QueueFree();
		}
	}
}
