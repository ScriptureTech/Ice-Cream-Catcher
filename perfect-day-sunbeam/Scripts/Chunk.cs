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
		// Calculate where the ground begins
		float chunkGroundLevel = ChunkSprite.GlobalPosition.Y - ((ChunkSprite.Texture.GetSize().Y * ChunkSprite.GlobalScale.Y) / 2);
		for(int i = 0; i < GD.RandRange(3, 5); i++)
		{
			IceCream iceCream = GameManager.Instance.IceCreamScene.Instantiate<IceCream>();
			GD.Print(GlobalPosition.X + GameManager.Instance.ChunkSize);

			//Subtraction is to keep them within the chunk
			iceCream.GlobalPosition = new Vector2((float)GD.RandRange(GlobalPosition.X, GlobalPosition.X + GameManager.Instance.ChunkSize) - GlobalPosition.X, (float)GD.RandRange(chunkGroundLevel, MaxIceCreamHeight));
			AddChild(iceCream);
		}
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
