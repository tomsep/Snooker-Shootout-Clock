using Godot;
using System;

public partial class VolumeSlider : HSlider
{
    private CommandService _commandService;
    public override void _Ready()
	{
        _commandService = GetNode<CommandService>("/root/CommandService");
        MaxValue = _commandService.GetMaxVolume() - 1;
        Value = _commandService.GetVolume();
        ValueChanged += VolumeSlider_ValueChanged;
    }

    private void VolumeSlider_ValueChanged(double value)
    {
        _commandService.SetVolume((int)value);
    }
}
