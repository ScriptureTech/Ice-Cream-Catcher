using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node
{
	[Export]
	public float ChunkSpeed = 50.0f;

	[Export]
	public float ChunkSize = 1152.0f;

	[Export]
	public PackedScene ChunkScene;

	[Export]
	public PackedScene IceCreamScene;

	public static GameManager Instance;

	public int Score = 0;

	public Chunk CurrentChunk;

	private List<Chunk> AllChunks = new List<Chunk>();

	public void FreeCurrentChunk()
	{
		AllChunks.Remove(CurrentChunk);
		CurrentChunk.QueueFree();
		CurrentChunk = AllChunks.Count > 0 ? AllChunks[0] : null; //CurrentChunks should only have two chunks at any time
		
		AddNewChunk(new Vector2(ChunkSize, 0));
	}

	public void AddNewChunk(Vector2 position, bool setCurrentChunk = false)
	{
		Chunk newChunk = ChunkScene.Instantiate<Chunk>();

		newChunk.GlobalPosition = position;
		newChunk.ChunkSprite.Modulate = new Color(GD.Randf(), GD.Randf(), GD.Randf(), 255);

		GetTree().CurrentScene.AddChild(newChunk);

		AllChunks.Add(newChunk);

		if (setCurrentChunk)
		{
			CurrentChunk = newChunk;
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;

		GD.Randomize();

		//Two default chunks
		AddNewChunk(new Vector2(ChunkSize, 0));
		AddNewChunk(new Vector2(0, 0), true);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
