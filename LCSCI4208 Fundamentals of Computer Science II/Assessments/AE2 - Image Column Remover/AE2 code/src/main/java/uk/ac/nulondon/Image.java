// ===== STARTING CODE (provided by the module) =====
package uk.ac.nulondon;

import java.awt.Color;
import java.awt.image.BufferedImage;
import java.util.ArrayList;
import java.util.List;
// ===== END OF STARTING CODE =====

//todo
// add comments on every method (even given ones) explaining:
// - what it does
// - parameters
// - how it works

// ===== STARTING CODE (provided by the module) =====
public final class Image {
    private List<List<Color>> columns;
    private int width;
    private int height;
    // ===== END OF STARTING CODE =====

    // Constructor: Converts BufferedImage to a list of columns
    // ===== STARTING CODE (provided by the module) =====
    public Image(BufferedImage img) {
        width = img.getWidth();
        height = img.getHeight();
        columns = new ArrayList<>();
        for (int i = 0; i < width; i++) {
            List<Color> column = new ArrayList<>();
            columns.add(column);
            for (int j = 0; j < height; j++) {
                column.add(new Color(img.getRGB(i, j)));
            }
        }
    }
    // ===== END OF STARTING CODE =====

    // Copy Constructor: Creates a deep copy of the Image
    public Image(Image other) {
        this.width = other.width;
        this.height = other.height;
        this.columns = new ArrayList<>();
        for (List<Color> column : other.columns) {
            this.columns.add(new ArrayList<>(column)); // Deep copy
        }
    }

    // Converts the stored data back to a BufferedImage
    // ===== STARTING CODE (provided by the module) =====
    public BufferedImage toBufferedImage() {
        BufferedImage image = new BufferedImage(width, height, BufferedImage.TYPE_INT_RGB);
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                Color pixel = columns.get(i).get(j);
                image.setRGB(i, j, pixel.getRGB());
            }
        }
        return image;
    }
    // ===== END OF STARTING CODE =====

    //gets the width of the image in pixels
    // ===== STARTING CODE (provided by the module) =====
    public int getWidth() {
        return width;
    }
    // ===== END OF STARTING CODE =====

    //gets the height of the image in pixels
    // ===== STARTING CODE (provided by the module) =====
    public int getHeight() {
        return height;
    }
    // ===== END OF STARTING CODE =====

    //Function: Turns every pixel in the column red
    //Parameter(s): column index
    //How it works: takes the index and loops through the array at that location, turning each pixel red

    public List<Color> highlightColumn(int i) {
        if (i < width) {
            //iterates through pixels in column and changes each to red
            List<Color> column = new ArrayList<>(columns.get(i)); // Copy the original column
            for (int j = 0; j < height; j++) {
                columns.get(i).set(j, Color.RED); // Apply highlight
            }
            return column;
        }
        else {
            System.out.println("Index out of bounds, try again.");
        }
        return null;
    }

    //Function: removes column of given index
    //Passed in: int for index
    //How it works: takes index of column to be removed then removes it from the array so the resaved image won't have it.
    public List<Color> removeColumn(int i) {  // <- starting code
        if (width == 0) {
            System.out.println("Image is empty");
        } else {
            columns.remove(i);
            width--;
        }
        return null;
    }

    //Function: adds a new given column at given index
    //Passed in: index integer and the pixels of the column to be added
    //How it works: takes in a column, increases width and inserts it to the columns array so the rebuilt image includes the new column
    public void addColumn(int index, List<Color> column) {  // <- starting code
        if (index < 0 || index > width) {
            System.out.println("Invalid index.");
        } else {
            columns.add(index, column);
            width++;
        }
    }
    //Function: finds the column with the most green pixels
    //How it works: iterates over each column and pixel, checking for which pixel is green to work out which column has the most green. it returns the column index.
    public int getGreenest() {  // <- starting code
        int greenestColumn = 0;
        int maxGreenValue = 0;
        //iterates over every column then pixel to check for green pixels
        for (int i = 0; i < columns.size(); i++) {
            int columnGreenTotal = 0;
            for (Color pixel : columns.get(i)) {
                columnGreenTotal += pixel.getGreen();
            }
            if (columnGreenTotal > maxGreenValue) {
                maxGreenValue = columnGreenTotal;
                greenestColumn = i;
            }
        }
        return greenestColumn;
    }
}
