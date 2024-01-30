using Godot;
using System;

public partial class ShotTimeSelector : Label
{
	[Export] private Button _decrementTimeButton;
    [Export] private Button _incrementTimeButton;

    public int LongTime { get; private set; } = 15;
    public int ShortTime => LongTime - 5;
    public int Step { get; set; } = 5;
    public int Min { get; set; } = 10;
    public int Max { get; set; } = 600;

    public override void _Ready()
	{
        UpdateDisplayText();
        _decrementTimeButton.Pressed += DecrementTimeButton_Pressed;
        _incrementTimeButton.Pressed += IncrementTimeButton_Pressed;
	}

    private void IncrementTimeButton_Pressed()
    {
        LongTime = Math.Min(LongTime + Step, Max);
        UpdateDisplayText();
    }

    private void DecrementTimeButton_Pressed()
    {
        LongTime = Math.Max(LongTime - Step, Min);
        UpdateDisplayText();
    }

    private void UpdateDisplayText()
    {
        var longSpan = TimeSpan.FromSeconds(LongTime);
        var shortSpan = TimeSpan.FromSeconds(ShortTime);
        Text = $"{Math.Round(longSpan.TotalSeconds, 0)} / {Math.Round(shortSpan.TotalSeconds, 0)}";
    }
}
