using Godot;
using System;

public partial class Bullet: Area2D {
  public AnimatedSprite2D BulletAnimation;
  public Vector2 Velocity;
  [Export]
  public float Speed = 10;
  private bool collision = false;

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

  private void _on_body_entered(Node2D body) {
	
	BulletAnimation.Play("collision");

	collision = true;
  }

	private void OnAnimationFinished() {
	if (collision){
			Speed = 0 ;
	  		QueueFree();
		}
  	}
}

