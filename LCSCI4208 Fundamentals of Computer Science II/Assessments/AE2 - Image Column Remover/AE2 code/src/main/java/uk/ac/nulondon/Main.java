// ===== STARTING CODE (provided by the module) =====
package uk.ac.nulondon;

import java.io.IOException;
import java.util.Scanner;
// ===== END OF STARTING CODE =====

//todo
// add comments on every method (even given ones) explaining:
// - what it does
// - parameters
// - how it works

// ===== STARTING CODE (provided by the module) =====
public final class Main {
    private final ImageEditor editor = new ImageEditor();
    // ===== END OF STARTING CODE =====

    //menu options print function
    // ===== STARTING CODE (provided by the module) =====
    private static void printMenu() {
        System.out.println("Please enter a command");
        System.out.println("g - Remove the greenest column");
        System.out.println("r[number] - Remove a specified column");
        System.out.println("u - Undo previous edit");
        System.out.println("q - Quit");
    }
    // ===== END OF STARTING CODE =====

    ////Function:to undo the last change and call the image before to
    //Passed in: user input as scanner
    //How it works: takes user input to undo the last change made by calling the method from the image class to pop stack
    private void undo(Scanner scan) throws IOException {
        System.out.println("Undo previous change? (d to confirm)");
        String confirm = scan.next();

        //confirmation
        if (confirm.equalsIgnoreCase("d")) {
            editor.undoStack();
            editor.save("target/editedImg.png");
        } else {
            System.out.println("No changes made.");

        }
    }

    //Function: removes a column given by user input
    //Passed in: user input and the choice of column to be removed
    //How it works: takes user input and splits to get the column they wish to remove. calls removeColumn to remove column and update image on confirmation
    private void removeSpecific(Scanner scan, String choice) throws IOException {  // <- starting code
        int index = Integer.parseInt(choice.substring(1));

        if (index >= editor.getWidth() || index < 0) {
            System.out.println("Invalid column index.");
            return;
        }

        //confirmation
        editor.highlightColumn(index);
        System.out.println("Remove column " + index + "? (d to confirm)");
        String confirm = scan.next();

        if (confirm.equalsIgnoreCase("d")) {
            editor.removeColumn(index);
            editor.pushStack();
            editor.save("target/editedImg.png");
        } else {
            System.out.println("No changes made.");
            editor.changeUnconfirmed();
        }
    }

    //Function: removes the column with the most green pixels
    //Passed in: input
    //How it works: highlights the greenest column then asks for deletion before using deleteColumn to remove it
    private void removeGreenest(Scanner scan) throws IOException {  // <- starting code
        int greenest = editor.getGreenest();
        editor.highlightColumn(greenest);
        //confirmation
        System.out.println("Remove greenest column? (d to confirm)");
        String confirm = scan.next();

        if (confirm.equalsIgnoreCase("d")) {
            editor.removeColumn(greenest);
            editor.pushStack();
            editor.save("target/editedImg.png");
        } else {
            System.out.println("No changes made.");
            editor.changeUnconfirmed();
        }
    }

    //Function: runs program
    //How it works: activates all written functions
    // ===== STARTING CODE (provided by the module) =====
    private void run() throws IOException {
        try (Scanner scan = new Scanner(System.in)) {
            System.out.println("Welcome! Enter file path");
            String filePath = scan.next();
            editor.load(filePath);
            System.out.printf("The image contains %d columns%n", editor.getWidth());

            String choice = "";
            while (!"q".equalsIgnoreCase(choice)) {
                printMenu();
                choice = scan.next();
                String command = choice.isEmpty() ? "" : choice.substring(0, 1);
                switch (command.toLowerCase()) {
                    case "g" -> removeGreenest(scan);
                    case "r" -> removeSpecific(scan, choice);
                    case "u" -> undo(scan);
                    // ===== END OF STARTING CODE =====
                    case "q" -> System.out.println("Thanks for using the editor!");
                    default -> System.out.println("Invalid option.");
                }
            }
            editor.save("target/finalImage.png");
        }
    }

    // ===== STARTING CODE (provided by the module) =====
    public static void main(String[] args) {
        try {
            new Main().run();
        } catch (IOException e) {
            System.out.println(e.getMessage());
        }
    }
}
// ===== END OF STARTING CODE =====
