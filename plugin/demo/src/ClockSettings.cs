using Godot;

[Tool]
[GlobalClass]
public partial class ClockSettings : Resource
{
    [Export] public int FrameLength { get; set; }
    [Export] public int ShotLength_Long { get; set; }
    [Export] public int ShotLength_Short { get; set; }
}
