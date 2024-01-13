using Godot;
using System;

public class GunBehavior
{
	private PackedScene bulletScene;

	public GunBehavior(PackedScene bulletScene)
	{
		this.bulletScene = bulletScene;
	}

	public void ProcessShoot(Bullet.BulletTypesLists type, Vector2 position, Vector2 targetPos, Node parent)
	{
		var bulletInstance = (Bullet)bulletScene.Instantiate();
		
		bulletInstance.BulletType= type;
		
		Vector2 direction = (targetPos - position).Normalized();
		
		bulletInstance.GlobalPosition = position;
		
		bulletInstance.Velocity = direction * bulletInstance.Speed;
		
		parent.GetTree().Root.AddChild(bulletInstance);
	}

}
