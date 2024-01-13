using Godot;
using System;

public partial class LazerGun : Sprite2D
{

	private GunBehavior gunBehavior;
	private PackedScene bulletScene = ResourceLoader.Load<PackedScene>("res://Nodes/Weapons/LazerGun/LazerGunBullet.tscn");
	private Timer primaryTimer;
	private Timer secondaryTimer;
	private Timer chargeTimer;
	private Timer chargeDuration;
	private bool isPrimary = true;
	private bool isSecondary = true;
	private bool isCharge = true;
	private bool isCharging= false;

	private Vector2 lastMousePosition = Vector2.Zero;
	public override void _Ready()
	{
		gunBehavior = new GunBehavior(bulletScene);
		primaryTimer = GetNodeOrNull<Timer>("Primary Weapon Timer");
		secondaryTimer = GetNodeOrNull<Timer>("Secondary Weapon Timer");
		chargeTimer = GetNodeOrNull<Timer>("Charge Weapon Timer");
		chargeDuration=GetNodeOrNull<Timer>("Charge Weapon Duration");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 mousePos = GetGlobalMousePosition();
		Vector2 dirr = (mousePos - GlobalPosition).Normalized();
		float orbitAngle = dirr.Angle();

		// 18 and 12 are the original positions of the gun sprite
		Position = new Vector2(
		  18 * (float)Math.Cos(orbitAngle),
		  12 * (float)Math.Sin(orbitAngle)
		);
		LookAt(GetGlobalMousePosition());

		if (Input.IsActionPressed("Action"))
		{
			if (Input.IsActionPressed("Charge") && isCharge)
			{
				gunBehavior.ProcessShoot(Bullet.BulletTypesLists.charge, GlobalPosition, GetGlobalMousePosition(), this);
				chargeDuration.Start();
			}
			else if (isPrimary)
			{
				gunBehavior.ProcessShoot(Bullet.BulletTypesLists.primary, GlobalPosition, GetGlobalMousePosition(), this);
				isPrimary = false;
				primaryTimer.Start();
			}
		}
		else if (Input.IsActionPressed("Secondary Action") && isSecondary)
		{
			gunBehavior.ProcessShoot(Bullet.BulletTypesLists.secondary, GlobalPosition, GetGlobalMousePosition(), this);
			isSecondary = false;
			secondaryTimer.Start();
		}
	}
	public void onChargeTimeOut()
	{
		isCharge = true;
	}


	public void onPrimaryTimeOut()
	{
		isPrimary = true;
	}


	public void onSecondaryTimeOut()
	{
		isSecondary = true;
	}
	
	public void onChargeDuration()
	{
		isCharge = false;
		chargeTimer.Start();
	}

}



