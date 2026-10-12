// ===== STARTING CODE (provided by the module) =====
package uk.ac.nulondon;

import java.awt.*;

public class Pixel {
    Pixel left;
    Pixel right;

    double energy;

    final Color color;

    public Pixel(int rgb) {
        this.color = new Color(rgb);
    }

    public Pixel(Color color) {
        this.color = color;
    }

    public double brightness() {
    // ===== END OF STARTING CODE =====
        return (color.getRed() + color.getGreen() + color.getBlue()) / 3;
    }

    // ===== STARTING CODE (provided by the module) =====
    public double getGreen() {
        return color.getGreen();
    }
    // ===== END OF STARTING CODE =====

}
