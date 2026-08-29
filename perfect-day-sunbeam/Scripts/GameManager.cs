using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node
{
	[Export]
	public float ChunkSpeed = 50.0f;

	[Export]
	public PackedScene ChunkScene;

	public static GameManager Instance;

	public Chunk CurrentChunk;

	private List<Chunk> AllChunks = new List<Chunk>();

	public void FreeCurrentChunk()
	{
		AllChunks.Remove(CurrentChunk);
		CurrentChunk.QueueFree();
		CurrentChunk = AllChunks.Count > 0 ? AllChunks[0] : null; //CurrentChunks should only have two chunks at any time
		
		AddNewChunk();
	}

	public void AddNewChunk(Vector2 position = default)
	{
		Chunk newChunk = ChunkScene.Instantiate<Chunk>();
		AllChunks.Add(newChunk);

		newChunk.GlobalPosition = position == default ? new Vector2(1152, 0) : position;
		newChunk.Modulate = new Color(GD.RandRange(0, 255), GD.RandRange(0, 255), GD.RandRange(0, 255), 255);
		GetTree().CurrentScene.AddChild(newChunk);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;

		GD.Randomize();

		//Two default chunks
		AddNewChunk(new Vector2(0, 0));
		AddNewChunk();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
