using Godot;
using System;

public partial class Chunk : Node2D
{
	[Export]
	public Sprite2D ChunkSprite;

	[Export]
	public float MaxIceCreamHeight = 100.0f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		float chunkGroundLevel = GetChunkGroundLevel();
		for(int i = 0; i < GD.RandRange(3, 5); i++)
		{
			IceCream iceCream = GameManager.Instance.IceCreamScene.Instantiate<IceCream>();
			GD.Print(GlobalPosition.X + GameManager.Instance.ChunkSize);

			//Subtraction is to keep them within the chunk
			iceCream.GlobalPosition = new Vector2(GetRandomXPositionWithinChunk(), (float)GD.RandRange(chunkGroundLevel, MaxIceCreamHeight));
			AddChild(iceCream);
		}

		for(int i = 0; i < GD.RandRange(1, 3); i++)
		{
			ObstacleBase obstacle = GameManager.Instance.ValidObstalces[GD.RandRange(0, GameManager.Instance.ValidObstalces.Count - 1)].Instantiate<ObstacleBase>();
			obstacle.GlobalPosition = new Vector2(GetRandomXPositionWithinChunk(), chunkGroundLevel - (obstacle.ObstacleSprite.Texture.GetSize().Y * obstacle.ObstacleSprite.GlobalScale.Y) / 2);
			AddChild(obstacle);
		}
	}

	public float GetRandomXPositionWithinChunk()
	{
		return (float)GD.RandRange(GlobalPosition.X, GlobalPosition.X + GameManager.Instance.ChunkSize) - GlobalPosition.X;
	}

	public float GetChunkGroundLevel()
	{
		return ChunkSprite.GlobalPosition.Y - ((ChunkSprite.Texture.GetSize().Y * ChunkSprite.GlobalScale.Y) / 2);
	}

	public override void _Draw()
	{
		DrawLine(ToLocal(new Vector2(GlobalPosition.X, MaxIceCreamHeight)), ToLocal(new Vector2(GlobalPosition.X + GameManager.Instance.ChunkSize, MaxIceCreamHeight)), ChunkSprite.Modulate, 2);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GlobalPosition += new Vector2(-GameManager.Instance.ChunkSpeed * (float)delta, 0);

		if(GlobalPosition.X < -GameManager.Instance.ChunkSize)
		{
			GameManager.Instance.FreeCurrentChunk();
		}

		QueueRedraw();
	}
}
