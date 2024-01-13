using Godot;
using System;

public partial class LazerGun: Sprite2D {

  private GunBehavior gunBehavior;
  private PackedScene bulletScene = ResourceLoader.Load<PackedScene>("res://Nodes/Weapons/LazerGun/LazerGunBullet.tscn");
private Timer primaryTimer ;
private Timer secondaryTimer ;
private Timer chargeTimer ;




private Vector2 lastMousePosition = Vector2.Zero;
  public override void _Ready() {
	gunBehavior= new GunBehavior(bulletScene) ;
	primaryTimer= GetNodeOrNull<Timer>("Primary Timer");
	secondaryTimer= GetNodeOrNull<Timer>("Secondary Timer");
	chargeTimer= GetNodeOrNull<Timer>("Charge Timer");

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

	if (Input.IsActionPressed("Action")) {
		if(Input.IsActionPressed("Charge")){
			GD.Print("Charge");
			chargeTimer.Start();
		}
		else
		{
			gunBehavior.ProcessShoot(Bullet.BulletTypesLists.primary, GlobalPosition, GetGlobalMousePosition(), this);
			primaryTimer.Start();
		}
	}
	else if (Input.IsActionPressed("Secondary Action"))
	{
		gunBehavior.ProcessShoot(Bullet.BulletTypesLists.secondary, GlobalPosition, GetGlobalMousePosition(), this);
		secondaryTimer.Start();
	}
  }

}
