shader_type canvas_item;

uniform bool isEnabled = false;
uniform float pulsation_rate : hint_range(0, 10) = 1.0;
uniform float aura_size : hint_range(0.0, 0.1) = 0.05; // Size of the aura

void fragment() {
	if (!isEnabled) {
		COLOR = texture(TEXTURE, UV);
	}
	else {
	    // Pulsation effect based on time
	    float pulsation = (sin(TIME * pulsation_rate) + 1.0) / 2.0;

	    // Sample the original texture color
	    vec4 original_color = texture(TEXTURE, UV);

	    // Calculate aura color
	    vec4 aura_color = vec4(0.0);
	    int samples = 8; // Number of samples for the blur effect
	    float step = aura_size / float(samples);
	    for (int i = -samples; i <= samples; ++i) {
	        for (int j = -samples; j <= samples; ++j) {
	            vec2 offset = vec2(float(i) * step, float(j) * step);
	            aura_color += texture(TEXTURE, UV + offset);
	        }
	    }
	    aura_color /= float((samples * 2 + 1) * (samples * 2 + 1));

	    // Combine original color with aura color based on pulsation
	    COLOR = mix(original_color, aura_color, pulsation * original_color.a);
	}
}
