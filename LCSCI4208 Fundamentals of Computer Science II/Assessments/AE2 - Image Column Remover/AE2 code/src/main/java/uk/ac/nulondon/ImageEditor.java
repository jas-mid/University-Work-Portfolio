// ===== STARTING CODE (provided by the module) =====
package uk.ac.nulondon;

import javax.imageio.ImageIO;
// ===== END OF STARTING CODE =====
import java.awt.*;
// ===== STARTING CODE (provided by the module) =====
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.util.ArrayDeque;
// ===== END OF STARTING CODE =====

//todo
// add comments on every method (even given ones) explaining:
// - what it does
// - parameters
// - how it works

public final class ImageEditor {  // <- starting code
    private Image image;
    private final ArrayDeque<Image> imageStack = new ArrayDeque<>();

    //Function: loads a PNG file at location given
    //Passed in: the file path
    //How it works: assigns the file at the path to a temporary variable then assigned to an instance of image so it can be edited
    // ===== STARTING CODE (provided by the module) =====
    public void load(String filePath) throws IOException {
        File originalFile = new File(filePath);
        BufferedImage img = ImageIO.read(originalFile);
        image = new Image(img);
        // ===== END OF STARTING CODE =====
        //added so the bottom of the stack is there before any changes made
        imageStack.push(new Image(image));
    }

    //Function: saves current image as png
    //Passed in: file path as string
    //How it works: takes file path given and saves the current instance of the image to a PNG file
    // ===== STARTING CODE (provided by the module) =====
    public void save(String filePath) throws IOException {
        BufferedImage img = image.toBufferedImage();
        ImageIO.write(img, "png", new File(filePath));
    }
    // ===== END OF STARTING CODE =====

    //CODE FOR STACK TO UNDO--------------------------------------------------------------------------------------------

    //Function: Pushes the newest image to the top of the stack
    //How it works: uses the push function to add the current value stored in image to the stack
    public void pushStack() {
        imageStack.push(new Image(image)); // Store deep copy
    }

    //Function: undoes the last change made
    //How it works: pops the stack and calls the image that is now at the top to be the current image
    public void undoStack() {
        if (imageStack.size() < 2) {
            System.out.println("No changes to undo.");
        } else {
            imageStack.pop();
            image = new Image(imageStack.peek());
            System.out.println("Undo successful.");
        }
    }

    //Function: makes top image in stack the current image
    //How it works: Calls image at the top of the stack (so long as it isn't empty) to be the current image so that it isn't a highlighted image
    public void changeUnconfirmed() {
        if (!imageStack.isEmpty()) {
            image = new Image(imageStack.peek());
        }
    }

    //CODE TO ALLOW USE OF IMAGE FUNCTIONS IN MAIN----------------------------------------------------------------------
    // all of these functions just allow functions in image to be used in the main class by calling them and passing in whatever they are given.
    public void highlightColumn(int index) {
        image.highlightColumn(index);
    }

    public void removeColumn(int index) {
        image.removeColumn(index);
    }

    public void addColumn(int index, java.util.List<Color> column) {
        image.addColumn(index, column);
    }

    public int getGreenest() {
        return image.getGreenest();
    }

    // ===== STARTING CODE (provided by the module) =====
    public int getWidth() {
        return image.getWidth();
    }
}
// ===== END OF STARTING CODE =====
