using Godot;
using System;
using System.Numerics;

public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed = 300.0f;
	[Export]
	public float JumpVelocity = -400.0f;

	public override void _PhysicsProcess(double delta)
	{
		GetInputAndMove(delta);
		MoveAndSlide();
	}

	public void GetInputAndMove(double delta){
		if (!IsOnFloor())
		{
			Velocity += GetGravity() * (float)delta;
		}

		if (Input.IsActionJustPressed("Jump") && IsOnFloor())
		{
			Velocity = new Godot.Vector2(Velocity.X, JumpVelocity);
		}
	}

	public void On_Hitbox_Entered(Area2D area)
	{
		if(area is ObstacleBase obstacle)
		{
			GameManager.Instance.Score -= obstacle.ScoreLossValue;
		}
	}
}
