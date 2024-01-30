using Godot;
using Prism.Events;
using System;

public partial class VolumeSlider : HSlider
{
    private CommandService _commandService;
    private IEventAggregator _events;

    public override void _Ready()
	{
        _commandService = GetNode<CommandService>("/root/CommandService");
        _events = EventAggregatorService.EventAggregator();
        _events.GetEvent<ReferenceVolumeChangedEvent>().Subscribe(
            UpdateSliderWithoutNotify, ThreadOption.PublisherThread, false);
        MaxValue = _commandService.GetMaxVolume() - 1;
        Value = _commandService.GetVolume();
        ValueChanged += VolumeSlider_ValueChanged;
    }


    private void VolumeSlider_ValueChanged(double value)
    {
        _commandService.SetVolume((int)value);
    }

    private void UpdateSliderWithoutNotify()
    {
        SetValueNoSignal(_commandService.GetVolume());
    }
}
