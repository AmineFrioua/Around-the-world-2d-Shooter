using Godot;
using System;

public partial class LazerGun: Sprite2D {
  private PackedScene bulletScene = ResourceLoader.Load<PackedScene>("res://Nodes/Weapons/LazerGun/LazerGunBullet.tscn");
  private bool isShooting = false;
  private float timeSinceLastShot = 0f;
 [Export]
  public float ShootRate = 0.2f; // bullet per second
	
private Vector2 lastMousePosition = Vector2.Zero;
  public override void _Ready() {}

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
		timeSinceLastShot += (float)delta ;
		if (timeSinceLastShot >= ShootRate)
		{
			ShootBullet(GetGlobalMousePosition());
			timeSinceLastShot = 0f;
		}
		
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

	private void ShootBullet(Vector2 targetPos)
	{
		// Instance a new bullet
		var bulletInstance = (Bullet)bulletScene.Instantiate();
		Vector2 bulletInitialPosition =new Vector2(GlobalPosition.X + Position.X +18  , GlobalPosition.Y +  Position.Y + 12);  
		Vector2 direction = (targetPos - GlobalPosition).Normalized();

		// Set bullet's position and velocity
		bulletInstance.GlobalPosition= bulletInitialPosition ; // or a specific gun's position
		bulletInstance.Velocity = direction *bulletInstance.Speed; // Assume Speed is defined in LaserBullet

		// Add the bullet to the scene
		GetTree().Root.AddChild(bulletInstance);
	}
}
