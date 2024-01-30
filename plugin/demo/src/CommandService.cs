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

    private bool _volumeControlEnabled;
    public bool VolumeControlEnabled
    {
        get => _volumeControlEnabled;
        set
        {
            if (_volumeControlEnabled != value)
            {
                SetVolume(GetVolume());
            }
            _volumeControlEnabled = value;
        }
    }

    public override void _Ready()
    {
        _events = EventAggregatorService.EventAggregator();
        _volumeService = GetNode("/root/AndroidVolumeService");
        _referenceVolume = GetVolume();
        _lastFrameTimestamp = DateTime.UtcNow;
    }

    public override void _Process(double delta)
    {
        bool appWasPaused = AppWasPaused();
        if (appWasPaused)
        {
            // Set new reference volume
            SetVolume(GetVolume());
            GD.Print("Set new reference volume after application pause");
        }
        else
        {
            // Publish event if volume was changed outside this application
            // i.e. remote controller changed or hardware volume button.
            if (GetVolume() != _referenceVolume && VolumeControlEnabled)
            {
                SetVolume(_referenceVolume);
                _events.GetEvent<ShotResetEvent>().Publish();
            }
            else if (GetVolume() != _referenceVolume)
            {
                SetVolume(GetVolume());
            }
        }

        _lastFrameTimestamp = DateTime.UtcNow;
    }

    private bool AppWasPaused()
    {
        var timeSinceLastFrame = (DateTime.UtcNow - _lastFrameTimestamp).TotalSeconds;
        return timeSinceLastFrame > 0.5;
    }

    public int GetVolume()
    {
        return _volumeService.Call("get_volume").AsInt32();
    }

    public int GetMaxVolume()
    {
        return _volumeService.Call("get_max_volume").AsInt32() - 1;
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
            _events.GetEvent<ShotResetEvent>().Publish();
        }
    }
}

public class ShotResetEvent : PubSubEvent { }

public class ReferenceVolumeChangedEvent : PubSubEvent { }