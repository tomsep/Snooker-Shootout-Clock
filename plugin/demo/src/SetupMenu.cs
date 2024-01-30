using Godot;
using System;

public partial class SetupMenu : MarginContainer
{
	[Export] private Button _startButton;
    [Export] private GameTimeSelector _gameTimeSelector;
    [Export] private ShotTimeSelector _shotTimeSelector;
    private CommandService _commandService;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        _commandService = GetNode<CommandService>("/root/CommandService");
        _commandService.VolumeControlEnabled = false;

        int maxVolume = _commandService.GetMaxVolume();
        if (_commandService.GetVolume() == maxVolume)
            _commandService.SetVolume(maxVolume);

        _startButton.Pressed += StartButton_Pressed;
	}

    private void StartButton_Pressed()
    {
        var clockMenu = ResourceLoader.Load<PackedScene>("res://src/ClockMenu.tscn").Instantiate<ClockMenu>();
        var settings = new ClockSettings()
        {
            FrameLength = _gameTimeSelector.Time,
            ShotLength_Long = _shotTimeSelector.LongTime,
            ShotLength_Short = _shotTimeSelector.ShortTime,
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
