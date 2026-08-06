shader_type canvas_item;

// This creates a slider in your Inspector to control how thick the fade is!
uniform float edge_softness : hint_range(0.0, 0.5) = 0.1;

void fragment() {
    // Calculate how close the pixel is to the edges
    float dist_x = min(UV.x, 1.0 - UV.x);
    float dist_y = min(UV.y, 1.0 - UV.y);
    
    // Smoothly fade the alpha channel based on the distance
    float alpha_x = smoothstep(0.0, edge_softness, dist_x);
    float alpha_y = smoothstep(0.0, edge_softness, dist_y);
    
    // Apply the fade to the ColorRect's color
    COLOR.a *= alpha_x * alpha_y;
}