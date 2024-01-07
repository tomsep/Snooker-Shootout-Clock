using Godot;
using System;

public partial class SetupMenu : MarginContainer
{
	[Export] private Button _startButton;
    [Export] private GameTimeSelector _gameTimeSelector;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _startButton.Pressed += StartButton_Pressed;
	}

    private void StartButton_Pressed()
    {
        var clockMenu = ResourceLoader.Load<PackedScene>("res://src/ClockMenu.tscn").Instantiate<ClockMenu>();
        var settings = new ClockSettings()
        {
            FrameLength = _gameTimeSelector.Time,
            ShotLength_Long = 15,
            ShotLength_Short = 10,
        };
        clockMenu.Initialize(settings);
        GetTree().Root.AddChild(clockMenu);
        GetTree().CurrentScene.QueueFree();
        GetTree().CurrentScene = clockMenu;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
