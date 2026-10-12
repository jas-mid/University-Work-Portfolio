package uk.ac.nulondon.commands;

import uk.ac.nulondon.Command;
import uk.ac.nulondon.ImageEditor;
import uk.ac.nulondon.Pixel;

import java.io.IOException;
import java.util.List;

public class RemoveHighlightedCommand implements Command {

    //create an instance of ImageEditor to work with
    private final ImageEditor imageEditor;
    private List<Pixel> removedSeam;

    //constructor assigning the input to the class's instance of ImageEditor
    public RemoveHighlightedCommand(ImageEditor imageEditor) {
        this.imageEditor = imageEditor;
    }

    //override to ensure the command runs in the correct way
    @Override
    public void execute() throws IOException {
        removedSeam = imageEditor.getHighlightedSeam();
        imageEditor.getImage().removeSeam(removedSeam);
        imageEditor.setHighlightedSeam(null);
        imageEditor.save("target/afterRemoval.png");
    }

    @Override
    public void undo() throws IOException {
        imageEditor.getImage().addSeam(removedSeam);
        imageEditor.save("target/undo.png");
    }
}
