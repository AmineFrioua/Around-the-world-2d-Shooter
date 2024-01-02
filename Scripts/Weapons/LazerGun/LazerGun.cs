using Godot;
using System;

public partial class LazerGun: Sprite2D {
	
  private GunBehavior gunBehavior;
  private PackedScene bulletScene = ResourceLoader.Load<PackedScene>("res://Nodes/Weapons/LazerGun/LazerGunBullet.tscn");
  private bool isShooting = false;
  private float timeSinceLastShot = 0f;
 [Export]
  public float ShootRate = 0.2f; // bullet per second
	
private Vector2 lastMousePosition = Vector2.Zero;
  public override void _Ready() {
	GD.Print(bulletScene);
	gunBehavior= new GunBehavior(bulletScene, ShootRate) ;
}

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(double delta) {
	Vector2 mousePos = GetGlobalMousePosition();
	Vector2 dirr = (mousePos - GlobalPosition).Normalized();
	float orbitAngle = dirr.Angle();

	// 18 and 12 are the original positions of the gun sprite
	Position = new Vector2(
	  18 * (float) Math.Cos(orbitAngle),
	  12 * (float) Math.Sin(orbitAngle)
	);
	LookAt(GetGlobalMousePosition());
	
	if (isShooting) {
		 GD.Print("Shoot");
		 gunBehavior.ProcessShoot(delta, GlobalPosition, GetGlobalMousePosition(), this);
	}
  }

 public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Left  && mouseEvent.Pressed)
			{
				isShooting= true;
				lastMousePosition = GetGlobalMousePosition();
			}
			else if( !mouseEvent.Pressed)
			{
				isShooting= false;
			}
		}
	}
}
