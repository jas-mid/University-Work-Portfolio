// ===== STARTING CODE (provided by the module) =====
package uk.ac.nulondon;

import javax.imageio.ImageIO;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.util.ArrayDeque;
import java.util.Deque;
import java.util.List;

/*APPLICATION SERVICE LAYER*/
public class ImageEditor {

    private Image image;
    private List<Pixel> highlightedSeam = null;
    // ===== END OF STARTING CODE =====
    public Deque<Command> commandHistory = new ArrayDeque<>();

    // ===== STARTING CODE (provided by the module) =====
    public void load(String filePath) throws IOException {
        File originalFile = new File(filePath);
        BufferedImage img = ImageIO.read(originalFile);
        image = new Image(img);
    }

    public void save(String filePath) throws IOException {
        BufferedImage img = image.toBufferedImage();
        ImageIO.write(img, "png", new File(filePath));
    }

    public void highlightGreenest() throws IOException {
    // ===== END OF STARTING CODE =====
        highlightedSeam = image.getGreenestSeam();
        image.highlightSeam(highlightedSeam, Color.red);
        save("target/highlightGreenest.png");
    }

    public void highlightLowestEnergySeam() throws IOException {
        highlightedSeam = image.getLowestEnergySeam();
        image.highlightSeam(highlightedSeam, Color.red);
        save("target/highlightLowestEnergy.png");
    }

    public void removeHighlighted() throws IOException {  // <- starting code
        if (highlightedSeam != null) {
            Command cmd = new uk.ac.nulondon.commands.RemoveHighlightedCommand(this);
            cmd.execute();
            commandHistory.push(cmd);
        }
    }

    public void undo() throws IOException {  // <- starting code
        if (!commandHistory.isEmpty()) {
            Command cmd = commandHistory.pop();
            cmd.undo();
        }
    }

    // helper methods for Command classes
    public Image getImage() {
        return image;
    }

    public List<Pixel> getHighlightedSeam() {
        return highlightedSeam;
    }

    public void setHighlightedSeam(List<Pixel> seam) {
        this.highlightedSeam = seam;
    }
}
