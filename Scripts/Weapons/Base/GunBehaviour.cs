using Godot;
using System;

public class GunBehavior
{
	private PackedScene bulletScene;
	private float timeSinceLastShot = 0f;

	public float ShootRate { get; set; } = 0.2f; // bullets per second

	public GunBehavior(PackedScene bulletScene, float shootRate)
	{
		this.bulletScene = bulletScene;
		ShootRate=shootRate ;
		GD.Print(this.bulletScene);
	}

	public void ProcessShoot(double delta, Vector2 position, Vector2 targetPos, Node parent)
	{
		timeSinceLastShot += (float)delta;
		if (timeSinceLastShot >= ShootRate)
		{
			ShootBullet(position, targetPos, parent);
			timeSinceLastShot = 0f;
		}
	}

	private void ShootBullet(Vector2 gunPosition, Vector2 targetPos, Node parent)
	{

		var bulletInstance = (Bullet)bulletScene.Instantiate();
		Vector2 direction = (targetPos - gunPosition).Normalized();
		bulletInstance.GlobalPosition = gunPosition;
		bulletInstance.Velocity = direction * bulletInstance.Speed; 
		parent.GetTree().Root.AddChild(bulletInstance);
	}
}
