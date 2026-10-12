package uk.ac.nulondon.commands;

import uk.ac.nulondon.Command;
import uk.ac.nulondon.ImageEditor;

import java.io.IOException;

public class UndoCommand implements Command {

    //create an instance of ImageEditor
    private final ImageEditor imageEditor;

    //constructor to assign the input to the instance of ImageEditor
    public UndoCommand(ImageEditor imageEditor) {
        this.imageEditor = imageEditor;
    }
    @Override
    public void execute() throws IOException {
        imageEditor.undo();
    }

    @Override
    public void undo() {
    }
}
