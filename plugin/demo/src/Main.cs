using Godot;
using System;

public partial class Main : MarginContainer
{
	public override void _Ready()
	{
		GetTree().ChangeSceneToFile("res://src/SetupMenu.tscn");
	}
}
