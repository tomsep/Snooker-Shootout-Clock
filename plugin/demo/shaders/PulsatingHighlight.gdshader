shader_type canvas_item;

uniform bool isEnabled = false;

// Uniform variable to control pulsation rate
uniform float pulsation_rate : hint_range(0, 10) = 1.0;

void fragment() {
	
	if (!isEnabled) {
		COLOR = vec4(0, 0, 0, 0);
	}
	else {
	    // Pulsation effect based on time
	    float pulsation = (sin(TIME * pulsation_rate) + 1.0) / 2.0;

	    // Sample the original texture color
		float orig_alpha = texture(TEXTURE, UV).a;
	    vec4 base_color = vec4(1, 1, 1, orig_alpha * 0.3);

	    // Apply pulsation only to non-transparent areas
	    // The alpha channel of the original color is preserved
	    COLOR = vec4(base_color.rgb, base_color.a * pulsation);
	}
}