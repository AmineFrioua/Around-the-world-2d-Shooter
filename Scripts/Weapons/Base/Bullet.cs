using Godot;
using System;

public partial class Bullet: Area2D {
  public AnimatedSprite2D BulletAnimation;


  public Vector2 Velocity;
  [Export]
  public float Speed = 10;

  // Called when the node enters the scene tree for the first time.
  public override void _Ready() {
	LookAt(GetGlobalMousePosition());
	BulletAnimation = GetNode < AnimatedSprite2D >("Bullet");
	BulletAnimation.AnimationFinished += () => OnAnimationFinished() ;
	
	BulletAnimation.Play("default");
  }

  // Called every frame. 'delta' is the elapsed time since                                                                                                   the previous frame.
  public override void _Process(double delta) {
	Position += Velocity * Speed * (float) delta;
  }

  public void _on_body_entered(Node2D body) {
	BulletAnimation.AnimationFinished += OnAnimationFinished ;
	

	BulletAnimation.Play("collision");
	
	Chachia enemy = body as Chachia; 
	GD.Print(body);
	GD.Print($"enemy {enemy}");
	if (enemy!= null)
	{
		enemy.OnDamage();
	}
	

  }

	public void OnAnimationFinished() {
			if (BulletAnimation.Animation == "collision"){
			Speed = 0 ;
	  		QueueFree();
		}
  	}
}

