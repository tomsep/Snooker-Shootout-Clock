using Godot;
using System;

public partial class ShotButton : TextureButton
{
	private ShaderMaterial _material;

	public new void SetPressedNoSignal(bool pressed)
	{
		base.SetPressedNoSignal(pressed);
		_material?.SetShaderParameter("isEnabled", pressed);
	}

	public override void _Ready()
	{
		_material = Material as ShaderMaterial;
        _material?.SetShaderParameter("isEnabled", false);
    }
}
