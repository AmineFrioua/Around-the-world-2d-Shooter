using Godot;
using System;

public partial class Sphere: CharacterBody2D {
	
 [Export]
  public float Speed = 300.0f;

  private Sprite2D lazerGun = null;

  public override void _Ready() {
	lazerGun = GetNodeOrNull < Sprite2D > ("Sprites/LazerGun");
  }

  public override void _PhysicsProcess(double delta) {

	Vector2 velocity = Velocity;
	Vector2 direction = Input.GetVector("Left", "Right", "Up", "Down");
	if (direction != Vector2.Zero) {
	  velocity.X = direction.X * Speed;
	  velocity.Y = direction.Y * Speed;
	} else {
	  velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
	  velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);

	}
	Velocity = velocity;
	MoveAndSlide();
  }
}
