using Godot;
using System;

public partial class PauseButton : Button
{
	[Export] private TextureRect _highlightRect;

    private ShaderMaterial _material;

	public override void _Ready()
	{
        _material = _highlightRect.Material as ShaderMaterial;
        _material?.SetShaderParameter("isEnabled", false);
        Pressed += Button_Pressed;
	}

    private void Button_Pressed()
    {
        _material?.SetShaderParameter("isEnabled", ButtonPressed);
    }
}
