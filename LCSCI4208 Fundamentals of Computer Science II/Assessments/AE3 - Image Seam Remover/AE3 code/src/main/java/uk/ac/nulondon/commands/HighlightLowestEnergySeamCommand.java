package uk.ac.nulondon.commands;

import uk.ac.nulondon.Command;
import uk.ac.nulondon.ImageEditor;

import java.io.IOException;

abstract class HighlightLowestEnergySeamCommand implements Command {

    //create an instance of ImageEditor
    private final ImageEditor imageEditor;
    
    //constructor to assign the input to the class's instance of ImageEditor
    public HighlightLowestEnergySeamCommand(ImageEditor imageEditor) {
        this.imageEditor = imageEditor;
    }
    
    
    //override to make execute run this function when called correctly
    @Override
    public void execute(){
        //try-catch in case of incorrect use
        try {
            imageEditor.highlightLowestEnergySeam();
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }
}
