# uk.ac.nulondon:project2

**Project Description:**

Image Editing Software Project: 
This project's goal is to create a code compilation that can be used to edit images that have been buffered into pixels.
It has an integrated command structure for the main class to communicate with the ImageEditor class as well as encapsulated commands to ensure modularity.

The project can locate specific 'seams' with certain characteristics and perform operations on them, including highlighting, removing and undoing changes. The characteristics specifically used and calculated in this program are based around the energy (a near arbitrary measure of brightness via average RGB value) or colour.


**Description of key functions:**

Remove Greenest Seam ("g") - Finds the seam with the most green in it and removes it from the image completely.

Remove Lowest Energy ("e") - Locates the seam with the lowest sum of pixel energy and removes it from the image.

Undo Previous Edit ("u") - Undoes the previous action done by the user. Makes no changes if no actions have been carried out.

Quit ("q") - Ends the program.


Java version 21

Generated at 2024-02-14 10:04:00
