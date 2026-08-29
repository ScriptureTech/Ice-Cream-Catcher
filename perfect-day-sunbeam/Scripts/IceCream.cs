using Godot;
using System;

public partial class IceCream : Area2D
{
	[Export]
	public int PointValue = 10;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void On_Body_Entered(Node2D body)
	{
		if(body is Player)
		{
			GameManager.Instance.Score += PointValue;
			QueueFree();
		}
	}
}
