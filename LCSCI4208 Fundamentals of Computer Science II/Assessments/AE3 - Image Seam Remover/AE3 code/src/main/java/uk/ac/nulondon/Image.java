// ===== STARTING CODE (provided by the module) =====
package uk.ac.nulondon;

import java.awt.Color;
import java.awt.image.BufferedImage;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import java.util.function.Function;

public class Image {
    private final List<Pixel> rows;

    private int width;
    private int height;


    public Image(BufferedImage img) {
        width = img.getWidth();
        height = img.getHeight();
        rows = new ArrayList<>();
        Pixel current = null;
        for (int row = 0; row < height; row++) {
            for (int col = 0; col < width; col++) {
                Pixel pixel = new Pixel(img.getRGB(col, row));
                if (col == 0) {
                    rows.add(pixel);
                } else {
                    current.right = pixel;
                    pixel.left = current;
                }
                current = pixel;
            }
        }
    }

    public BufferedImage toBufferedImage() {
        BufferedImage image = new BufferedImage(width, height, BufferedImage.TYPE_INT_RGB);
        for (int row = 0; row < height; row++) {
            Pixel pixel = rows.get(row);
            int col = 0;
            while (pixel != null) {
                image.setRGB(col++, row, pixel.color.getRGB());
                pixel = pixel.right;
            }
        }
        return image;
    }

    public int getWidth() {
        return width;
    }

    public int getHeight() {
        return height;
    }
    // ===== END OF STARTING CODE =====

    //calculates individual pixel energy
    double energy(Pixel above, Pixel current, Pixel below) {  // <- starting code
        //check if pixel is missing a required pixel relation if so the brightness is its energy
        if(above == null
                || current.right == null
                || below == null
                || current.left == null
                || below.right == null
                || below.left == null
                || above.right == null
                || above.left == null
        ){
            return current.brightness();
        }

        //create all the needed brightness variables with their relation to the original pixel
        double brA = above.left.brightness();
        double brG = below.left.brightness();
        double brC = above.right.brightness();
        double brI = below.right.brightness();
        //multiplied by 2 as shown in the formula given in the brief
        double brF = current.right.brightness() * 2;
        double brB = above.brightness() * 2;
        double brD = current.left.brightness() * 2;
        double brH = below.brightness() * 2;
        //create new helper variables through doing the equations as shown in the brief
        double horizEnergy = (brB + brD + brG) - (brC + brF + brI);
        double vertEnergy = (brA + brB + brC) - (brG + brH + brI);
        //final equation to get the total energy of the pixel
        double totalEnergy = Math.sqrt((horizEnergy * horizEnergy) + (vertEnergy * vertEnergy));

        return totalEnergy;
    }

    double bordercase(Pixel curr){
        return curr.brightness();
    }

    public void calculateEnergy() {  // <- starting code
        //iterate through the image calculating the energies and storing that information in the energy double within Pixel
        for(int row = 0; row < height; row++) {
            Pixel curr = rows.get(row);
            for(int col = 0; col < width; col++) {
                //gets current pixel
                Pixel pixel = curr;
                //checks if an edge case
                if(row > 0 && row < height - 1) {
                    double energyVals = energy(rows.get(row - 1), pixel, rows.get(row + 1));
                    pixel.energy = energyVals;
                    curr = curr.right;
                }
                //if there is an edge case use helper function
                else {
                    pixel.energy = bordercase(pixel);
                }
            }
        }
    }

    //highlights the selected seam
    public List<Pixel> highlightSeam(List<Pixel> seam, Color color) {
        //create a copy of the original seam in order to return the original values
        List<Pixel> seamCopy = new ArrayList<>(seam);
        // Iterate through seam
        for (Pixel pixel : seam) {
            // Create a new Pixel with the desired colour as we Pixel is final and thus immutable
            Pixel highlightedPixel = new Pixel(color);
            //if there is no left edge case(no pixel to the left) update location of highlighted pixel
            if (pixel.left != null) {
                highlightedPixel.left = pixel.left;
                pixel.left.right = highlightedPixel;
            }
            //if there is no right edge case(no pixel to the right) update location of highlighted pixel
            if (pixel.right != null) {
                highlightedPixel.right = pixel.right;
                pixel.right.left = highlightedPixel;
            }
        }
        //return original list
        return seamCopy;
    }

    //removes seam from the image
    public void removeSeam(List<Pixel> seam) {  // <- starting code
        //start at the bottom row
        int row = height - 1;
        //iterate through removing the seam
        for (Pixel pixel : seam) {
            if (pixel.right != null) {
                //if pixel to the right is not null it gets 'linked' to the pixel on the left of the current pixel effectively removing it
                pixel.right.left = pixel.left;
            } else {
                rows.set(row, pixel.left);
            }
            if (pixel.left != null){
                //if pixel to the left is not null it gets 'linked' to the pixel on the right of the current pixel effectively removing it
                pixel.left.right = pixel.right;
            }
            //repeat up the rows
            row--;
        }
        //decrease width to reflect removal of seam
        width--;
    }

    //Adds seam back to the image
    public void addSeam(List<Pixel> seam) {  // <- starting code
        //start at the bottom
        int row = height - 1;
        //iterate through the seam adding the pixel where it should be
        for (Pixel pixel : seam) {
            //if not an edge case simply add back in
            if (pixel.left != null) {
                pixel.left.right = pixel;
            } else {
                rows.set(row, pixel);
            }
            //if not an edge case simply add back in
            if (pixel.right != null){
                pixel.right.left = pixel;
            }
            //repeat up the rows
            row--;
        }
        //increase width to reflect changes
        width++;
    }

    //helper function to use within getSeamMaximisng to add a pixel to a list of Pixels
    public List<Pixel> concat(Pixel currPixel, List<Pixel> prevSeam){
        List<Pixel> newSeam = new ArrayList<>();
        newSeam.add(currPixel);
        newSeam.addAll(prevSeam);
        return newSeam;
    }

    //finds the seam with maximum value requested within the image
    private List<Pixel> getSeamMaximizing(Function<Pixel, Double> valueGetter) {
        //create variables to store information about seams and pixel values
        double[] prevVals = new double[width];
        double[] current = new double[width];
        List<List<Pixel>> prevSeams = new ArrayList<>();
        List<List<Pixel>> currSeams = new ArrayList<>();
        Pixel currentPixel = rows.getFirst();
        //allows us to store the information about the current column
        int col = 0;

        while(currentPixel != null) {
            prevVals[col] = valueGetter.apply(currentPixel);
            prevSeams.add(List.of(currentPixel));
            currentPixel = currentPixel.right;
            col++;
        }

        //filling up paths array
        for(int row = 1; row < height; row++) {
            currentPixel = rows.get(row);
            col = 0;
            while(currentPixel != null) {
                double max = prevVals[col];
                int ref = col;
                if(col > 0 && prevVals[col - 1] > max){
                    max = prevVals[col - 1];
                    ref = col - 1;
                }
                if(col < width - 1 && prevVals[col + 1] > max){
                    max = prevVals[col + 1];
                    ref = col + 1;
                }

                current[col] = max + valueGetter.apply(currentPixel);
                //check out concat, used in lecture don't know if he coded as a helper or imported it
                currSeams.add(concat(currentPixel, prevSeams.get(ref)));
                col++;
                currentPixel = currentPixel.right;
            }
            prevVals = current;
            current = new double[width];
            prevSeams = currSeams;
            currSeams = new ArrayList<>();
        }

        //finding the seam with the maximum sum
        double maxVal = prevVals[0];
        int maxValIndex = 0;
        for(int i = 0; i < width; i++){
            if(prevVals[i] > maxVal){
                maxVal = prevVals[i];
                maxValIndex = i;
            }
        }

        return prevSeams.get(maxValIndex);

    }

    //uses getSeamMaximising to find the seam with the greatest green value
    public List<Pixel> getGreenestSeam() {
        return getSeamMaximizing(Pixel::getGreen);
    }

    //uses getSeamMaximising to find the seam with the lowest energy
    // ===== STARTING CODE (provided by the module) =====
    public List<Pixel> getLowestEnergySeam() {
        calculateEnergy();
        // ===== END OF STARTING CODE =====
        //finding the max of -energy is the same as finding the minimum
        return getSeamMaximizing(pixel -> -pixel.energy);  // <- starting code
    }
}

