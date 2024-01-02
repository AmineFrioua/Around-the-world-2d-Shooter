using Godot;
using System;

public partial class Bullet : Area2D
{	
	public Vector2 Velocity;
	[Export]
	public float Speed= 100;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		 LookAt(GetGlobalMousePosition());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		 Position += Velocity * (float)delta;
	  	
	}
}
