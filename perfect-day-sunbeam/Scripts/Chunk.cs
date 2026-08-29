using Godot;
using System;

public partial class Chunk : Node2D
{
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GlobalPosition += new Vector2(-GameManager.Instance.ChunkSpeed * (float)delta, 0);
	}

	public void On_Hitbox_Entered(Area2D collider)
	{
		GD.Print("Hitbox entered!");
		if(collider.Name == "DestroyBox")
		{
			GameManager.Instance.FreeCurrentChunk();
		}
	}
}
