using Godot;
using Prism.Events;
using System;

public partial class ClockMenu : Control
{
    [Export] private Timer _frameTimer;
    [Export] private Timer _frameHalfTimeTimer;
    [Export] private Label _frameTimeLabel;

    [Export] private Timer _shotTimer;
    [Export] private Label _shotTimeLabel;

    [Export] private Timer _frameLastMinuteTimer;
    [Export] private Timer _frameLastHalfMinuteTimer;
    [Export] private Timer _frameLast15SecondsTimer;
    [Export] private AudioStreamPlayer _frameLastMinute;
    [Export] private AudioStreamPlayer _frameLastHalfMinute;
    [Export] private AudioStreamPlayer _frameLast15Seconds;

    [Export] private Button _playPauseButton;
    [Export] private ShotButton _shotButton;
    [Export] private Button _exitButton;

    [Export] private AudioStreamPlayer _shotStartSound;
    [Export] private AudioStreamPlayer _shotEndSound;
    [Export] private AudioStreamPlayer _frameEndSound;
    [Export] private AudioStreamPlayer _frameHalfTimeSound;

    [Export] public ClockSettings Settings { get; private set; }

    private IEventAggregator _events;

    private bool _isFrameStarted;
    private bool _isPaused;
    private bool _isFrameFinished;
    private bool _shotTimerFinished;
    private bool _isHalfTimePassed;

    public void Initialize(ClockSettings settings)
    {
        Settings = settings;
    }

    public override void _Ready()
    {
        _events = EventAggregatorService.EventAggregator();
        _events.GetEvent<ShortResetEvent>().Subscribe(ShotReset);

        _frameTimer.Timeout += FrameTimer_Timeout;
        _frameHalfTimeTimer.Timeout += FrameHalfTimeTimer_Timeout;
        _shotTimer.Timeout += ShotTimer_Timeout;
        _frameLastMinuteTimer.Timeout += LastMinute_Timeout;
        _frameLastHalfMinuteTimer.Timeout += LastHalfMinute_Timeout;
        _frameLast15SecondsTimer.Timeout += FrameLast15SecondsTimer_Timeout;
        _playPauseButton.ButtonDown += PlayPauseButton_ButtonDown;
        _exitButton.Pressed += ExitButton_Pressed;
        _shotButton.Pressed += ShotButton_Pressed;

        UpdateShotTimerDisplay(TimeSpan.FromSeconds(GetShotLength()));
        UpdateFrameTimerDisplay(TimeSpan.FromSeconds(Settings.FrameLength));

        StartFrame();
    }

    private void FrameLast15SecondsTimer_Timeout()
    {
        _frameLast15Seconds.Play();
    }

    private void FrameHalfTimeTimer_Timeout()
    {
        _isHalfTimePassed = true;
        _frameHalfTimeSound.Play();
    }

    private void ShotButton_Pressed()
    {
        ShotReset();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        _events.GetEvent<ShortResetEvent>().Unsubscribe(ShotReset);
    }

    private void ExitButton_Pressed()
    {
        GetTree().ChangeSceneToFile("res://src/SetupMenu.tscn");
    }

    public void StartFrame()
    {
        _frameTimer.Start(Settings.FrameLength);
        _frameHalfTimeTimer.Start(Settings.FrameLength / 2);
        _frameLastMinuteTimer.Start(Settings.FrameLength - 60);
        _frameLastHalfMinuteTimer.Start(Settings.FrameLength - 30);
        _frameLast15SecondsTimer.Start(Settings.FrameLength - 15);

        GD.Print(nameof(StartFrame));
    }

    private void ShotReset()
    {
        if (_isPaused || _isFrameFinished)
            return;

        if (_shotTimer.IsStopped() && !_shotTimerFinished)
        {
            _shotTimer.Start(GetShotLength());
            _shotStartSound.Play();
            _shotButton.SetPressedNoSignal(true);
            GD.Print(nameof(ShotReset) + " Started");
            
        }
        else
        {
            _shotTimer.Stop();
            _shotTimer.WaitTime = GetShotLength();
            UpdateShotTimerDisplay(TimeSpan.FromSeconds(_shotTimer.WaitTime));
            _shotButton.SetPressedNoSignal(false);
            _shotTimerFinished = false;
            GD.Print(nameof(ShotReset) + " Stopped");
        }
    }

    private void FrameTimer_Timeout()
    {
        _frameTimeLabel.Text = $"{0:D2}:{0:D2}";
        _frameEndSound.Play();
        _frameLastHalfMinute.Stop();
        _shotTimer.Stop();
        _isFrameFinished = true;
        GD.Print(nameof(FrameTimer_Timeout));
    }

    private void ShotTimer_Timeout()
    {
        _shotTimeLabel.Text = $"{0:D2}:{0:D2}";
        _shotEndSound.Play();
        _shotTimerFinished = true;
        GD.Print(nameof(ShotTimer_Timeout));
    }

    private void LastMinute_Timeout()
    {
        _frameLastMinute.Play();
        GD.Print(nameof(LastMinute_Timeout));
    }

    private void LastHalfMinute_Timeout()
    {
        _frameLastHalfMinute.Play();
        _frameLastMinute.Stop();
        GD.Print(nameof(LastHalfMinute_Timeout));
    }

    private void PlayPauseButton_ButtonDown()
    {
        if (_isFrameFinished)
            return;

        if (_isPaused)
            Pause(false);
        else
            Pause(true);
    }

    public void Pause(bool pause)
    {
        _shotTimer.Paused = pause;
        _frameTimer.Paused = pause;
        _frameHalfTimeTimer.Paused = pause;
        _frameLastHalfMinuteTimer.Paused = pause;
        _frameLastMinuteTimer.Paused = pause;
        _frameLast15SecondsTimer.Paused = pause;
        _isPaused = pause;
        GD.Print(nameof(Pause) + $" {pause}");
    }

    public override void _Process(double delta)
    {
        if (!_frameTimer.IsStopped())
            UpdateFrameTimerDisplay(TimeSpan.FromSeconds(_frameTimer.TimeLeft));

        if (!_shotTimer.IsStopped())
            UpdateShotTimerDisplay(TimeSpan.FromSeconds(_shotTimer.TimeLeft));
    }

    private int ConvertToDisplaySeconds(int seconds, int max)
    {
        return Math.Min(seconds + 1, max);
    }

    private void UpdateFrameTimerDisplay(TimeSpan time)
    {
        _frameTimeLabel.Text =
            $"{time.Minutes:D2}:{ConvertToDisplaySeconds(time.Seconds, Settings.FrameLength):D2}";
    }

    private void UpdateShotTimerDisplay(TimeSpan time)
    {
        _shotTimeLabel.Text =
            $"{time.Minutes:D2}:{ConvertToDisplaySeconds(time.Seconds, GetShotLength()):D2}";
    }

    private int GetShotLength()
    {
        if (_isHalfTimePassed)
            return Settings.ShotLength_Short;
        else
            return Settings.ShotLength_Long;
    }
}
