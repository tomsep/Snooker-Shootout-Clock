using Godot;
using Prism.Events;
using System;

public partial class CommandService : Node
{
    private int _referenceVolume = 0;
    private DateTime _lastFrameTimestamp;
    private int _count = 0;
    private Node _volumeService;
    private IEventAggregator _events;

    public override void _Ready()
    {
        _events = EventAggregatorService.EventAggregator();
        _volumeService = GetNode("/root/AndroidVolumeService");
        _referenceVolume = GetVolume();
        _lastFrameTimestamp = DateTime.UtcNow;
    }

    public override void _Process(double delta)
    {
        // Publish event if volume was changed outside this application
        // i.e. remote controller changed or hardware volume button.

        // BUT only if app wasn't paused when the volume changed
        var timeSinceLastFrame = (DateTime.UtcNow - _lastFrameTimestamp).TotalSeconds;
        int volume = GetVolume();
        if (volume != _referenceVolume && timeSinceLastFrame <= 1)
        {
            SetVolume(_referenceVolume);
            _events.GetEvent<ShortResetEvent>().Publish();
        }
        else if (timeSinceLastFrame > 1)
        {
            // App was likely paused, so set new reference volume
            SetVolume(GetVolume());
            _events.GetEvent<ReferenceVolumeChangedEvent>().Publish();
            GD.Print("Set new reference volume");
        }

        _lastFrameTimestamp = DateTime.UtcNow;
    }

    public int GetVolume()
    {
        return _volumeService.Call("get_volume").AsInt32();
    }

    public int GetMaxVolume()
    {
        return _volumeService.Call("get_max_volume").AsInt32();
    }

    public void SetVolume(int volume)
    {
        volume = Math.Min(volume, GetMaxVolume());

        _referenceVolume = volume;
        _volumeService.Call("set_volume", volume);
    }

    public override void _UnhandledKeyInput(InputEvent @event)
    {
        if (@event.IsActionPressed(Inputs.Reset))
        {
            _events.GetEvent<ShortResetEvent>().Publish();
        }
    }
}

public class ShortResetEvent : PubSubEvent { }

public class ReferenceVolumeChangedEvent : PubSubEvent { }