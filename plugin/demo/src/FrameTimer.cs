using Godot;
using System;

public partial class FrameTimer : Timer
{
    [Export] private AudioStreamPlayer _endSound;
    [Export] private AudioStreamPlayer _countdownSound;

    public override void _Ready()
    {
        Timeout += Timer_Timeout;
    }

    private void Timer_Timeout()
    {
        _endSound.Play();
    }

    public override void _Process(double delta)
    {
        if (WasTimestampPassedThisFrame(3, TimeLeft, delta))
            _countdownSound.Play();
        else if (WasTimestampPassedThisFrame(2, TimeLeft, delta))
            _countdownSound.Play();
        else if (WasTimestampPassedThisFrame(1, TimeLeft, delta))
            _countdownSound.Play();
    }

    private bool WasTimestampPassedThisFrame(double timestamp, double currentTime, double delta)
    {
        return
            currentTime <= timestamp &&
            currentTime + delta > timestamp;
    }

}
