using Godot;
using System;
using System.Numerics;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

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
}
