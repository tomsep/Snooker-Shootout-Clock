using Godot;
using System;

public partial class GameTimeSelector : Label
{
	[Export] private Button _decrementTimeButton;
    [Export] private Button _incrementTimeButton;

    public int Time { get; private set; } = 10 * 60;
    public int Step { get; set; } = 60;
    public int Min { get; set; } = 60;
    public int Max { get; set; } = 59 * 60;

    public override void _Ready()
	{
        UpdateDisplayText();
        _decrementTimeButton.Pressed += DecrementTimeButton_Pressed;
        _incrementTimeButton.Pressed += IncrementTimeButton_Pressed;
	}

    private void IncrementTimeButton_Pressed()
    {
        Time = Math.Min(Time + Step, Max);
        UpdateDisplayText();
    }

    private void DecrementTimeButton_Pressed()
    {
        Time = Math.Max(Time - Step, Min);
        UpdateDisplayText();
    }

    private void UpdateDisplayText()
    {
        var span = TimeSpan.FromSeconds(Time);
        Text = $"{span.Minutes:D2}:{span.Seconds:D2}";
    }
}
