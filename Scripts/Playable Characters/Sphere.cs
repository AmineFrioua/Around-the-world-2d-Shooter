using Godot;
using System;

public partial class Sphere: CharacterBody2D {
	
 [Export]
  public float MovementSpeed = 300.0f;
  private Sprite2D lazerGun = null;

  public override void _Ready() {
	lazerGun = GetNodeOrNull < Sprite2D > ("Sprites/LazerGun");
  }

  public override void _PhysicsProcess(double delta) {

	Vector2 velocity = Velocity;
	Vector2 direction = Input.GetVector("Left", "Right", "Up", "Down");
	if (direction != Vector2.Zero) {
	  velocity.X = direction.X * MovementSpeed;
	  velocity.Y = direction.Y * MovementSpeed;
	} else {
	  velocity.X = Mathf.MoveToward(Velocity.X, 0, MovementSpeed);
	  velocity.Y = Mathf.MoveToward(Velocity.Y, 0, MovementSpeed);

	}
	Velocity = velocity;
	MoveAndSlide();
  }
}
