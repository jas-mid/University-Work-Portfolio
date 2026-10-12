package uk.ac.nulondon.commands;

import uk.ac.nulondon.Command;
import uk.ac.nulondon.ImageEditor;
import uk.ac.nulondon.Pixel;

import java.awt.*;
import java.io.IOException;
import java.util.List;

public class HighlightGreenestCommand implements Command {
    
    //create an instance of ImageEditor
    private final ImageEditor imageEditor;

    //constructor to ensure the input is assigned to the class's instance of ImageEditor
    public HighlightGreenestCommand(ImageEditor imageEditor) {
        this.imageEditor = imageEditor;
    }

    //ensure that execute used in this context runs the highlightGreenest function
    @Override
    public void execute() {
        //try-catch used in case of any incorrect uses
        try {
            imageEditor.highlightGreenest();
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }

    @Override
    public void undo() throws IOException {
        imageEditor.removeHighlighted();
        imageEditor.undo();
    }


}
